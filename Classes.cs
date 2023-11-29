namespace TTMC.AutoDNS
{
	public class CreateDns
	{
		public string? type { get; set; }
		public string? hostname { get; set; }
		public string? value { get; set; }
		public long? ttl { get; set; }
		public long? priority { get; set; }
		public long? weight { get; set; }
		public long? port { get; set; }
		public long? flag { get; set; }
		public string? tag { get; set; }
	}
	public class Dns
	{
		public string? id { get; set; }
		public string? hostname { get; set; }
		public string? type { get; set; }
		public string? value { get; set; }
		public long? ttl { get; set; }
		public long? priority { get; set; }
		public string? dns_zone_id { get; set; }
		public string? site_id { get; set; }
		public int? flag { get; set; }
		public long? tag { get; set; }
		public bool? managed { get; set; }
	}
	public class Response
	{
		public string? message { get; set; }
		public long? code { get; set; }
	}
}