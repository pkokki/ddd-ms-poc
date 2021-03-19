using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Core.Adapters.HttpClients
{
    public class RestClientConfiguration
    {
        public RestClientConfiguration(HttpClient httpClient)
        {
            Client = httpClient;
        }

        internal HttpClient Client { get; }
    }
}
