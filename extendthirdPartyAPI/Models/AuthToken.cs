using System;
namespace extendthirdPartyAPI.Models
{
    public class AuthToken
    {
        public String Token { get; set; }
        public String RefreshToken { get; set; }

        public AuthToken()
        {
        }
    }
}

