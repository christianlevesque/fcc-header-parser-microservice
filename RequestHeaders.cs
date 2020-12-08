using System.Text.Json.Serialization;

namespace header_parser
{
	public class RequestHeaders
	{
		[JsonPropertyName("ipaddress")]
		public string IpAddress { get; set; }
		
		[JsonPropertyName("software")]
		public string Software { get; set; }
		
		[JsonPropertyName("language")]
		public string Language { get; set; }
	}
}