using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace extendthirdPartyAPI.Models
{
    public class UserCrential
    {
        [JsonPropertyName("email")]
        public String Email { get; set; }
        [JsonPropertyName("password")]
        public String Password { get; set; }

        public UserCrential()
        {
            
        }
    }
}

