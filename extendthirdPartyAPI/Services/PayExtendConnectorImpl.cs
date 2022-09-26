using System;
using System.Text;
using System.Text.Json;
using extendthirdPartyAPI.Models;
using System.Text.Json.Serialization;
using System.Net;

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

        public async Task<Paginations> GetVirtualCards(String queryString, String token)
        {
            String url = URI + "virtualcards";

            if (queryString != null)
            {
                _logger.LogInformation("Query : " + queryString);
                url += queryString;
            }
            var httpClient = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("Accept", "application/vnd.paywithextend.v2021-03-12+json");
          
            request.Headers.Add("Authorization", "Bearer " + token);
            _logger.LogInformation("Auth bearer: " + token);
            using (var response = await httpClient.SendAsync(request))
            {
                String payload = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiException { StatusCode = response.StatusCode, Message = payload };
                }

                
                _logger.LogInformation("Resp : " + payload);

               Paginations pages = JsonSerializer.Deserialize<Paginations>(payload);
               return pages;
            }

           
        }

        public async Task<Transactions> GetCardTransactions(string cardId, string queryString, string token)
        {
            if (string.IsNullOrEmpty(cardId))
            {
                throw new ApiException { StatusCode = HttpStatusCode.BadRequest, Message = "CardId is null" };
            }

            String url = URI + "virtualcards/" + cardId + "/transactions";

            if (queryString != null)
            {
                _logger.LogInformation("Query : " + queryString);
                url += queryString;
            }

            var httpClient = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("Accept", "application/vnd.paywithextend.v2021-03-12+json");

            request.Headers.Add("Authorization", "Bearer " + token);

            using (var response = await httpClient.SendAsync(request))
            {
                String payload = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiException { StatusCode = response.StatusCode, Message = payload };
                }

                _logger.LogInformation("Resp : " + payload);

                Transactions transactions = JsonSerializer.Deserialize<Transactions>(payload);
                return transactions;
            }
        }

        public async Task<TransactionDetails> GetTransactionDetails(string id, string token)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ApiException { StatusCode = HttpStatusCode.BadRequest, Message = "Id is null" };
            }

            String url = URI + "transactions/" + id;

            var httpClient = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("Accept", "application/vnd.paywithextend.v2021-03-12+json");

            request.Headers.Add("Authorization", "Bearer " + token);

            using (var response = await httpClient.SendAsync(request))
            {
                String payload = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiException { StatusCode = response.StatusCode, Message = payload };
                }

                _logger.LogInformation("Resp : " + payload);

                TransactionDetails transactions = JsonSerializer.Deserialize<TransactionDetails>(payload);
                return transactions;
            }
        }
    }
}

