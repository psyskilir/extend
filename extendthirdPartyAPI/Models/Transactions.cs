using System;
using System.Text.Json.Serialization;

namespace extendthirdPartyAPI.Models
{
    public class Transactions
    {
        [JsonPropertyName("transactions")]
        public List<Transaction> TransactionList { get; set; }

        public Transactions()
        {
        }
    }

    public class Transaction
    {
        [JsonPropertyName("id")]
        public String Id { get; set; }

        [JsonPropertyName("cardholderId")]
        public String CardHolderId { get; set; }

        [JsonPropertyName("cardholderName")]
        public String CardHolderName { get; set; }

        [JsonPropertyName("recipientId")]
        public String RecipientId { get; set; }

        [JsonPropertyName("status")]
        public String Status { get; set; }

        [JsonPropertyName("authBillingAmountCents")]
        public long AuthBillingAmountCents { get; set; }

        [JsonPropertyName("authedAt")]
        public String AuthAt { get; set; }

        [JsonPropertyName("creditCardType")]
        public String CreditCardType { get; set; }
    }
}

