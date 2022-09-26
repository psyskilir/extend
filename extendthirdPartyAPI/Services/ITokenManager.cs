using System;
using extendthirdPartyAPI.Models;

namespace extendthirdPartyAPI.Services
{
    public interface ITokenManager
    {
        public AuthToken GetUserAuthToken(String id);
        public void AddUserAuthToken(String id, AuthToken token);
    }
}

