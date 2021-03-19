using Core.Adapters.HttpClients;
using ExtAccountService.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ExtAccountService.Client
{
    public class ExtAccountClientService : RestClient, IExtAccountClientService
    {
        private static readonly HttpClient INSTANCE;

        static ExtAccountClientService()
        {
            INSTANCE = new HttpClient
            {
                BaseAddress = new Uri("http://localhost"),
                Timeout = new TimeSpan(0, 0, 10),
                MaxResponseContentBufferSize = 1024 * 1024,
                DefaultRequestVersion = new Version(0, 1, 1)
            };
            INSTANCE.DefaultRequestHeaders.Add("x-xxx", "1");
        }

        public ExtAccountClientService(ILogger<ExtAccountClientService> logger) : base(INSTANCE, logger)
        {
        }

        public async Task<TransactionResponse> DepositAsync(TransactionRequest request, CancellationToken cancellationToken = default)
        {
            var response = await PostJsonAsync<TransactionResponse>("Account/Deposit", request, null, cancellationToken);
            return response;
        }

        public async Task<TransferTransactionResponse> TransferAsync(TransferTransactionRequest request, CancellationToken cancellationToken = default)
        {
            var response = await PostJsonAsync<TransferTransactionResponse>("Account/Transfer", request, null, cancellationToken);
            return response;
        }

        public async Task<TransactionResponse> WithdrawAsync(TransactionRequest request, CancellationToken cancellationToken = default)
        {
            var response = await PostJsonAsync<TransactionResponse>("Account/Withdraw", request, null, cancellationToken);
            return response;
        }
    }
}
