namespace TTMC.AutoDNS
{
	public class Config
	{
		public string accessToken = "TOKEN_HERE";
		public string type = "A";
		public string hostname = "nzx.hu";
		public string dnsZone = "nzx_hu";
		public int wait = 60000;
		public void SaveConfig(string path = "config.cfg")
		{
			string[] lines = { "# AutoDNS Config File", "# Created on: " + DateTime.Now + "\n", $"accessToken = \"{accessToken}\"", $"type = \"{type}\"", $"hostname = \"{hostname}\"", $"dnsZone = \"{dnsZone}\"", $"wait = {wait}" };
			File.WriteAllLines(path, lines);
		}
		public void LoadConfig(string path = "config.cfg")
		{
			foreach (string line in File.ReadLines(path))
			{
				string text = Crimp(line);
				if (!text.StartsWith("#") && text.Contains('='))
				{
					string[] temp = text.Split('=');
					switch (temp[0])
					{
						case "accessToken":
							if (temp[1].StartsWith('"') && temp[1].EndsWith('"'))
							{
								accessToken = temp[1][1..][..^1];
							}
							break;
						case "type":
							if (temp[1].StartsWith('"') && temp[1].EndsWith('"'))
							{
								type = temp[1][1..][..^1];
							}
							break;
						case "hostname":
							if (temp[1].StartsWith('"') && temp[1].EndsWith('"'))
							{
								hostname = temp[1][1..][..^1];
							}
							break;
						case "dnsZone":
							if (temp[1].StartsWith('"') && temp[1].EndsWith('"'))
							{
								dnsZone = temp[1][1..][..^1];
							}
							break;
						case "wait":
							int.TryParse(temp[1], out wait);
							break;
					}
				}
			}
		}
		public void Initialize(string path = "config.cfg")
		{
			if (!File.Exists(path))
			{
				SaveConfig();
			}
			else
			{
				LoadConfig();
			}
		}
		private string Crimp(string text)
		{
			string resp = string.Empty;
			bool god = false;
			foreach (char c in text)
			{
				if (c != ' ' || god)
				{
					if (c == '"')
					{
						god = !god;
					}
					resp += c;
				}
			}
			return resp;
		}
	}
}