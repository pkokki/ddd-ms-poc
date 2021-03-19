using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MobileGateway.IntegrationTests
{
    public class TestFixture : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        protected TestFixture(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        protected HttpClient CreateClient()
        {
            return factory.CreateClient();
        }
    }
}
