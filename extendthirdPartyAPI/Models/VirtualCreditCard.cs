using System;
using System.Text.Json.Serialization;

namespace extendthirdPartyAPI.Models
{
    public class VirtualCreditCard
    {
        [JsonPropertyName("id")]
        public String Id { get; set; }

        [JsonPropertyName("recipient")]
        public Recipient Recipient { get; set; }


        [JsonPropertyName("cardholder")]
        public CardHolder CardHolder { get; set; }

        [JsonPropertyName("issuer")]
        public Issuer Issuer { get; set; }

        [JsonPropertyName("address")]
        public Address Address { get; set; }

        [JsonPropertyName("status")]
        public String Status { get; set; }

        public VirtualCreditCard()
        {
        }
    }

    public class Address
    {
        [JsonPropertyName("address1")]
        public String Address1 { get; set; }

        [JsonPropertyName("address2")]
        public String Address2 { get; set; }

        [JsonPropertyName("city")]
        public String City { get; set; }

        [JsonPropertyName("province")]
        public String Province { get; set; }

        [JsonPropertyName("postal")]
        public String Postal { get; set; }

        [JsonPropertyName("country")]
        public String Country { get; set; }

        public Address()
        {

        }
    }

    public class Issuer
    {
        [JsonPropertyName("id")]
        public String Id { get; set; }

        [JsonPropertyName("name")]
        public String Name { get; set; }

        public Issuer()
        {

        }
    }

    public class CardHolder
    {
        [JsonPropertyName("id")]
        public String Id { get; set; }

        [JsonPropertyName("firstName")]
        public String FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public String LastName { get; set; }

        [JsonPropertyName("email")]
        public String Email { get; set; }

        [JsonPropertyName("phone")]
        public String PhoneNumber { get; set; }

        public CardHolder()
        {
        }
    }

    public class Recipient
    {

        [JsonPropertyName("id")]
        public String Id { get; set; }

        [JsonPropertyName("firstName")]
        public String FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public String LastName { get; set; }

        [JsonPropertyName("email")]
        public String Email { get; set; }

        [JsonPropertyName("phone")]
        public String PhoneNumber { get; set; }

        [JsonPropertyName("verified")]
        public Boolean IsVerified { get; set; }

        [JsonPropertyName("cardholderId")]
        public String CardHolderId { get; set; }

        public Recipient()
        {

        }
    }
}

