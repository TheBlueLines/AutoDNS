namespace TTMC.AutoDNS
{
	internal class Program
	{
		private static Config config = new();
		private static HttpClient client = new();
		private static string? catcheIP = null;
		private static string? catcheID = null;
		static void Main(string[] args)
		{
			config.Initialize();
			Netlify netlify = new(config.accessToken);
			LoadDefault(netlify);
			EndlessLoop(netlify);
		}
		private static void LoadDefault(Netlify netlify)
		{
			List<Dns> dnsList = netlify.getDnsRecords(config.dnsZone);
			Dns? search = dnsList.Where(x => x.hostname == config.hostname).FirstOrDefault();
			if (search != default && !string.IsNullOrEmpty(search.id) && !string.IsNullOrEmpty(search.value))
			{
				catcheID = search.id;
				catcheIP = search.value;
			}
		}
		private static void EndlessLoop(Netlify netlify)
		{
			while (true)
			{
				string publicIP = client.GetStringAsync("https://api.ipify.org").Result;
				if (publicIP != catcheIP)
				{
					catcheID = publicIP;
					if (!string.IsNullOrEmpty(catcheID))
					{
						netlify.deleteDnsRecord(config.dnsZone, catcheID);
					}
					CreateDns createDNS = new()
					{
						type = "A",
						hostname = config.hostname,
						value = publicIP
					};
					Dns dns = netlify.createDnsZone(config.dnsZone, createDNS);
					catcheID = dns.id;
					Thread.Sleep(config.wait);
				}
			}
		}
	}
}