using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Adapters.HttpClients
{
    public abstract class RestClient
    {
        private readonly HttpClient httpClient;

        protected ILogger Logger { get; }

        public RestClient(RestClientConfiguration config, ILogger logger)
        {
            this.httpClient = config.Client;
            this.Logger = logger;
        }
        public RestClient(HttpClient httpClient, ILogger logger)
        {
            this.httpClient = httpClient;
            this.Logger = logger;
        }

        public async Task<T> GetJsonAsync<T>(
            string requestUri,
            JsonSerializerOptions options = null,
            CancellationToken cancellationToken = default)
        {
            return await SendJsonAsync<T>(HttpMethod.Get, requestUri, null, options, cancellationToken);
        }
        public async Task<T> PostJsonAsync<T>(
            string requestUri, 
            object content,
            JsonSerializerOptions options = null,
            CancellationToken cancellationToken = default)
        {
            return await SendJsonAsync<T>(HttpMethod.Post, requestUri, content, options, cancellationToken);
        }
        public async Task<T> SendJsonAsync<T>(
            HttpMethod method,
            string requestUri,
            object content = null,
            JsonSerializerOptions options = null,
            CancellationToken cancellationToken = default)
        {
            Logger.LogInformation($"{method} {requestUri} BEGIN");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var message = new HttpRequestMessage(method, requestUri);
                if (content != null)
                {
                    var requestJson = JsonSerializer.Serialize(content, options);
                    Logger.LogInformation($"{method} {requestUri} REQUEST: {requestJson}");
                    message.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
                }

                var response = await httpClient.SendAsync(message, cancellationToken);
                response.EnsureSuccessStatusCode();

                if (typeof(T) == typeof(IgnoreResponse))
                {
                    Logger.LogInformation($"{method} {requestUri} RESPONSE IGNORED");
                    return default;
                }
                else
                {
                    var stringContent = await response.Content.ReadAsStringAsync();
                    Logger.LogInformation($"{method} {requestUri} RESPONSE: {stringContent}");
                    return JsonSerializer.Deserialize<T>(stringContent, options);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"{method} {requestUri} ERROR: {ex.Message}");
                throw;
            }
            finally
            {
                stopwatch.Stop();
                Logger.LogInformation($"{method} {requestUri} END: {stopwatch.ElapsedMilliseconds} ms");
            }
        }
    }
}
