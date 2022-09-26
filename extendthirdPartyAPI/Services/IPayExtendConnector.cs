using System;
using extendthirdPartyAPI.Models;

namespace extendthirdPartyAPI.Services
{
    public interface IPayExtendConnector
    {
        public Task<Paginations> GetVirtualCards(String queryString, String token);

        public Task<Transactions> GetCardTransactions(String cardId, String queryString, String token);

        public Task<TransactionDetails> GetTransactionDetails(String id, String token);
    }
}

