using System;
using extendthirdPartyAPI.Models;

namespace extendthirdPartyAPI.Services
{
    public interface IPayExtendConnector
    {
        /// <summary>
        /// API to get user's virtual credit card.
        /// Note : User's identity is encapsulate inside token passed in from Authorization header
        /// </summary>
        /// <param name="queryString">Optional query string</param>
        /// <param name="token">User's identity token</param>
        /// <returns>Paginated result of user's credit cards</returns>
        public Task<Paginations> GetVirtualCards(String queryString, String token);

        /// <summary>
        /// Get a list of user's credit card transactions.
        /// </summary>
        /// <param name="cardId">Credit card ID</param>
        /// <param name="queryString">Query string for query filtering</param>
        /// <param name="token">User's identity token</param>
        /// <returns></returns>
        public Task<Transactions> GetCardTransactions(String cardId, String queryString, String token);

        /// <summary>
        /// Get transaction details of a particular transaction
        /// </summary>
        /// <param name="id">Credit card transaction ID</param>
        /// <param name="token">User's identity token</param>
        /// <returns></returns>
        public Task<TransactionDetails> GetTransactionDetails(String id, String token);
    }
}

