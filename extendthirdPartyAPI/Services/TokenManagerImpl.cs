using System;
using System.Collections.Concurrent;
using extendthirdPartyAPI.Models;

namespace extendthirdPartyAPI.Services
{
    public class TokenManagerImpl : ITokenManager
    {
        private ConcurrentDictionary<String, AuthToken> _tokenStore;

        public TokenManagerImpl() 
        {
            _tokenStore = new ConcurrentDictionary<string, AuthToken>();
        }

        public void AddUserAuthToken(string id, AuthToken token)
        {
            if (string.IsNullOrEmpty(id) || token == null)
            {
                throw new ArgumentNullException("Invalid Id or token");
            }

            _tokenStore[id] = token;
        }

        public AuthToken GetUserAuthToken(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("Null id");

           if (!_tokenStore.ContainsKey(id))
           {
                return null;
           }

            return _tokenStore[id];
        }
    }
}

