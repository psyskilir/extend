using System;
using System.Net;

namespace extendthirdPartyAPI.Services
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public String Message { get; set; }

        public ApiException() : base()
        {
        }
    }
}

