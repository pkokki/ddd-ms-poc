using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace MobileGateway.IntegrationTests
{
    public static class HttpClientExtensions
    {
        public static async Task<T> PostJsonAsync<T>(
            this HttpClient client,
            string url,
            object payload,
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK,
            IDictionary<string, string> headers = null)
        {
            var requestJson = JsonSerializer.Serialize(payload);
            var message = new HttpRequestMessage(HttpMethod.Post, new Uri(url, UriKind.Relative))
            {
                Content = new StringContent(requestJson, Encoding.UTF8, "application/json")
            };

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    message.Headers.TryAddWithoutValidation(header.Key, header.Value);
                    message.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            var response = await client.SendAsync(message);
            Assert.Equal(expectedStatusCode, response.StatusCode);

            var stringContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(stringContent);
        }
    }
}
