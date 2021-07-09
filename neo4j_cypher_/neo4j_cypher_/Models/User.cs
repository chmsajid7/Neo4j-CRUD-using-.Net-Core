using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace neo4j_cypher_.Models
{
    public class User
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("title")]
        public string title { get; set; }
    }

    public class UserConnections
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("location")]
        public string location { get; set; }
        [JsonProperty("designation")]
        public string designation { get; set; }
        [JsonProperty("photo")]
        public string photo { get; set; }
        [JsonProperty("totalConnectionsCount")]
        public string totalConnectionsCount { get; set; }
        [JsonProperty("mutualConnectionsCount")]
        public string mutualConnectionsCount { get; set; }
    }

    public class ConnectionsViewModel
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("TotalConnectionsCount")]
        public int TotalConnectionsCount { get; set; }

        [JsonProperty("MutualConnectionsCount")]
        public int MutualConnectionsCount { get; set; }

        [JsonProperty("IsConnected")]
        public bool IsConnected { get; set; }
    }

    public class UserProfile
    {
        [JsonProperty("_id")]
        public string _id { get; set; }
        [JsonProperty("UserId")]
        public string UserId { get; set; }
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        [JsonProperty("LastName")]
        public string LastName { get; set; }
        [JsonProperty("Email")]
        public string Email { get; set; }
        public string About { get; set; }
        public string AboutDescription { get; set; }
        public bool EmailConfirmed { get; set; }
        public string ProfilePicture { get; set; }
        public string CoverPicture { get; set; }
        public string PhoneNumber { get; set; }
        //public Country Country { get; set; }
        //public State State { get; set; }
        //public City City { get; set; }
        //public UserProfileStatus UserProfileStatus { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Occupation { get; set; }
        public string Gender { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public bool IsUpdated { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }

    //public class ConnectionsViewModel
    //{
    //    public string UserId { get; set; }
    //    public string Name { get; set; }
    //    public string Designation { get; set; }
    //    public string Location { get; set; }
    //    public string ProfilePicture { get; set; }
    //    public int TotalConnectionsCount { get; set; }
    //    public int MutualConnectionsCount { get; set; }
    //    public bool IsConnected { get; set; }
    //}

}
