using Accounts.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Accounts.Application
{
    public interface ITransactionExecutionService
    {
        Task<string> DepositAsync(AccountTransaction transaction, CancellationToken cancellationToken = default);
        Task<string> WithdrawAsync(AccountTransaction transaction, CancellationToken cancellationToken = default);
        Task<AccountTransactionResult> TransferAsync(AccountTransaction transaction, CancellationToken cancellationToken = default);
    }
}
