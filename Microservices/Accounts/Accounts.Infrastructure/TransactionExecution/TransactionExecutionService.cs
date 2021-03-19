using Accounts.Application;
using Accounts.Domain.Entities;
using ExtAccountService.Client;
using ExtAccountService.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Accounts.Infrastructure.TransactionExecution
{
    public class TransactionExecutionService : ITransactionExecutionService
    {
        public IExtAccountClientService ExtAccountClientService { get; }

        public TransactionExecutionService(IExtAccountClientService extAccountClientService)
        {
            ExtAccountClientService = extAccountClientService ?? throw new ArgumentNullException(nameof(extAccountClientService));
        }

        public Task<string> DepositAsync(AccountTransaction transaction, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public Task<string> WithdrawAsync(AccountTransaction transaction, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public async Task<AccountTransactionResult> TransferAsync(AccountTransaction transaction, CancellationToken cancellationToken = default)
        {
            var request = new TransferTransactionRequest()
            {
                Amount = transaction.Amount
            };
            var response = await ExtAccountClientService.TransferAsync(request);
            return new AccountTransactionResult()
            {
                
            };
        }
    }
}
