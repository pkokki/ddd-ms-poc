using ExtAccountService.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExtAccountService.Client
{
    /// <summary>
    /// <see cref="IExtAccountClientService"/> is an <b>Application Service (14)</b> in the Accounts Microservice. 
    /// 
    /// Used as private Application Service in Accounts Microservice. Could be a standalone Microservice.
    /// </summary>
    public interface IExtAccountClientService
    {
        Task<TransactionResponse> DepositAsync(TransactionRequest request, CancellationToken cancellationToken = default);
        Task<TransactionResponse> WithdrawAsync(TransactionRequest request, CancellationToken cancellationToken = default);
        Task<TransferTransactionResponse> TransferAsync(TransferTransactionRequest request, CancellationToken cancellationToken = default);
    }
}
