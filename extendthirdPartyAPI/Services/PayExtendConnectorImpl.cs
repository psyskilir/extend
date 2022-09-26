using System;
using System.Text;
using System.Text.Json;
using extendthirdPartyAPI.Models;
using System.Text.Json.Serialization;

namespace extendthirdPartyAPI.Services
{
    public class PayExtendConnectorImpl : IPayExtendConnector
    {
        private static readonly string USERID = "u_49D6Wkt0RjH99TdHEDsjIe";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _options;
        private readonly ILogger<PayExtendConnectorImpl> _logger;
        private readonly ITokenManager _tokenManager;
        private static readonly String URI = "https://api.paywithextend.com/";

        public PayExtendConnectorImpl(IHttpClientFactory httpClientFactory, ILogger<PayExtendConnectorImpl> logger, ITokenManager tokenManager)
        {
            _httpClientFactory = httpClientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _logger = logger;
            _tokenManager = tokenManager;
        }

        public async Task<Paginations> GetVirtualCards(String queryString)
        { 
            AuthToken token = await GetAuthToken();

            String url = URI + "virtualcards";

            if (queryString != null)
            {
                _logger.LogInformation("Query : " + queryString);
                url += queryString;
            }
            var httpClient = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("Accept", "application/vnd.paywithextend.v2021-03-12+json");
          
            request.Headers.Add("Authorization", "Bearer " + token.Token);
            _logger.LogInformation("Auth bearer: " + token.Token);
            using (var response = await httpClient.SendAsync(request))
            {
                String resp = await response.Content.ReadAsStringAsync();
                
                _logger.LogInformation("Resp : " + resp);

               Paginations pages = JsonSerializer.Deserialize<Paginations>(resp);
                return pages;
            }

           
        }

        private async Task SignIn(UserCrential credential, String id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, URI + "signin");
            var jsonCred = JsonSerializer.Serialize(credential);

       
            request.Content = new StringContent(jsonCred, Encoding.UTF8, "application/json");
            request.Headers.Add("Accept", "application/vnd.paywithextend.v2021-03-12+json");

            using (var response = await httpClient.SendAsync(request))
            {
                String resp = await response.Content.ReadAsStringAsync();
                
                ExtendSignInModel model = JsonSerializer.Deserialize<ExtendSignInModel>(resp);

                _logger.LogInformation("Token : " + JsonSerializer.Serialize(model));

                AuthToken token = new AuthToken { Token = model.Token, RefreshToken = model.RefreshToken };
                _tokenManager.AddUserAuthToken(id, token);
            }
        }

        public async Task<Transactions> GetCardTransactions(string cardId, string queryString)
        {
            AuthToken token = await GetAuthToken();

            String url = URI + "virtualcards/" + cardId + "/transactions";

            if (queryString != null)
            {
                _logger.LogInformation("Query : " + queryString);
                url += queryString;
            }

            var httpClient = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("Accept", "application/vnd.paywithextend.v2021-03-12+json");

            request.Headers.Add("Authorization", "Bearer " + token.Token);

            using (var response = await httpClient.SendAsync(request))
            {
                String resp = await response.Content.ReadAsStringAsync();

                _logger.LogInformation("Resp : " + resp);

                Transactions transactions = JsonSerializer.Deserialize<Transactions>(resp);
                return transactions;
            }
        }

        private async Task<AuthToken> GetAuthToken()
        {
            AuthToken token = _tokenManager.GetUserAuthToken(USERID);


            if (token == null)
            {
                var cred = new UserCrential { Email = "psy00@yahoo.com", Password = "Pass$word4" };
                await SignIn(cred, USERID);
            }

            token = _tokenManager.GetUserAuthToken(USERID);

            return token;

        }
    }
}

