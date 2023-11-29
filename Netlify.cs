using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace TTMC.AutoDNS
{
	public class Netlify
	{
		public HttpClient client;
		public Netlify(string accessToken)
		{
			client = new()
			{
				BaseAddress = new Uri("https://api.netlify.com")
			};
			client.DefaultRequestHeaders.Authorization = new("Bearer", accessToken);
		}
		public Dns createDnsZone(string zone_id, CreateDns createDns)
		{
			JsonContent content = JsonContent.Create(createDns);
			HttpResponseMessage resp = client.PostAsync($"/api/v1/dns_zones/{zone_id}/dns_records", content).Result;
			return Deserialize<Dns>(resp.Content.ReadAsStringAsync().Result);
		}
		public List<Dns> getDnsRecords(string zone_id)
		{
			HttpResponseMessage resp = client.GetAsync($"/api/v1/dns_zones/{zone_id}/dns_records").Result;
			return Deserialize<List<Dns>>(resp.Content.ReadAsStringAsync().Result);
		}
		public bool deleteDnsRecord(string zone_id, string dns_record_id)
		{
			HttpResponseMessage resp = client.DeleteAsync($"/api/v1/dns_zones/{zone_id}/dns_records/{dns_record_id}").Result;
			return resp.StatusCode == HttpStatusCode.OK;
		}
		private T Deserialize<T>(string json)
		{
			try
			{
				if (json.StartsWith('{') && json.EndsWith('}'))
				{
					Response? error = JsonSerializer.Deserialize<Response>(json);
					if (error != null && error.message != null)
					{
						throw new(error.message);
					}
				}
				T? item = JsonSerializer.Deserialize<T>(json);
				if (item != null)
				{
					return item;
				}
			}
			catch { }
			throw new(json);
		}
	}
}