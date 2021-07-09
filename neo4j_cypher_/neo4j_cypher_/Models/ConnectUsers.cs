using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace neo4j_cypher_.Models
{
    public class ConnectUsers
    {
		//[BsonId]
		[JsonProperty("id")]
		public string id { get; set; }

		[JsonProperty("name")]
		public string name { get; set; }

		[JsonProperty("RequesterId")]
		public string RequesterId { get; set; }

		[JsonProperty("ReceiverId")]
		public string ReceiverId { get; set; }

		//[JsonProperty("ApprovalStatus")]
		//public ApprovalStatus ApprovalStatus { get; set; }

		[JsonProperty("CreatedDate")]
		public DateTimeOffset CreatedDate { get; set; }

		[JsonProperty("IsUpdated")]
		public bool IsUpdated { get; set; }

		[JsonProperty("UpdatedDate")]
		public DateTimeOffset UpdatedDate { get; set; }
	}
}
