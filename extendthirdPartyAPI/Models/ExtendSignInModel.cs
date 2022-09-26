using System;
using System.Text.Json.Serialization;

namespace extendthirdPartyAPI.Models
{
    public class ExtendSignInModel
    {
        [JsonPropertyName("id")]
        public String Id { get; set; }

        [JsonPropertyName("email")]
        public String Email { get; set; }

        [JsonPropertyName("token")]
        public String Token { get; set; }

        [JsonPropertyName("refreshToken")]
        public String RefreshToken { get; set; }


        public ExtendSignInModel()
        {
          

        }
    }
}

