using System;
using System.Text.Json.Serialization;

namespace extendthirdPartyAPI.Models
{

    public class Paginations
    {
        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; set; }

        [JsonPropertyName("virtualCards")]
        public List<VirtualCreditCard>? VirtualCards { get; set; }


        public Paginations()
        {

        }
    }

    public class Pagination
    {
        [JsonPropertyName("page")]
        public int page { get; set; }

        [JsonPropertyName("pageItemCount")]
        public int PageItemCount { get; set; }

        [JsonPropertyName("totalItems")]
        public int TotalItems { get; set; }

        [JsonPropertyName("numberOfPages")]
        public int NumberOfPages { get; set; }



        public Pagination()
        {
        }
    }
}

