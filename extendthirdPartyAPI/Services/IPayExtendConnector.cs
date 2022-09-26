using System;
using extendthirdPartyAPI.Models;

namespace extendthirdPartyAPI.Services
{
    public interface IPayExtendConnector
    {
        public Task<Paginations> GetVirtualCards(String queryString);

        public Task<Transactions> GetCardTransactions(String cardId, String queryString);
    }
}

