using System;
using System.Text.Json.Serialization;

namespace extendthirdPartyAPI.Models
{
    public class TransactionDetails
    {
        [JsonPropertyName("id")]
        public String Id { get; set; }

        [JsonPropertyName("cardholderId")]
        public String CardholderId { get; set; }

        [JsonPropertyName("cardholderName")]
        public String CardholderName { get; set; }

        [JsonPropertyName("recipientName")]
        public String RecipientName { get; set; }

        [JsonPropertyName("virtualCardId")]
        public String VirtualCardId { get; set; }

        [JsonPropertyName("authBillingAmountCents")]
        public long AuthBillingAmountCents { get; set; }

        [JsonPropertyName("authMerchantCurrency")]
        public String AuthMerchantCurrency { get; set; }

        [JsonPropertyName("authedAt")]
        public String AuthedAt { get; set; }


        public TransactionDetails()
        {
        }
    }
}

