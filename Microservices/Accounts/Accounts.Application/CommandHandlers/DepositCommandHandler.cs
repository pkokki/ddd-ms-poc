using Accounts.Application.Commands;
using Accounts.Domain;
using Core.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Accounts.Application.CommandHandlers
{
    public class DepositCommandHandler : CommandHandlerBase<DepositCommandV1, DepositCommandResponseV1>
    {
        private readonly IAccountRepository accountRepository;
        private readonly ITransactionExecutionService transactionExecutionService;
        private readonly ITransactionPermissionService transactionPermissionService;

        public DepositCommandHandler(
            IAccountRepository accountRepository,
            ITransactionExecutionService transactionExecutionService,
            ITransactionPermissionService transactionPermissionService
            )
        {
            this.accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            this.transactionExecutionService = transactionExecutionService ?? throw new ArgumentNullException(nameof(transactionExecutionService));
            this.transactionPermissionService = transactionPermissionService ?? throw new ArgumentNullException(nameof(transactionPermissionService));
        }

        public override async Task<DepositCommandResponseV1> Handle(DepositCommandV1 request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.GetAccountWithNumber(request.AccountNumber);
            if (account != null)
            {
                // Check permissions
                if (!transactionPermissionService.CanDeposit(request.UserId, request.AccountNumber))
                    throw new Exception($"{nameof(DepositCommandHandler)}: User '{request.UserId}' can not deposit in '{request.AccountNumber}'.");

                // Append to local storage
                var transaction = account.CreatePendingDepositTransaction(request.CommandId, request.Amount);
                await accountRepository.UnitOfWork.SaveEntitiesAsync();

                // Call external service
                var externalTransactionId = await transactionExecutionService.DepositAsync(transaction);

                // Update local storage
                account.MarkExternalTransactionComplete(request.CommandId, externalTransactionId);
                await accountRepository.UnitOfWork.SaveEntitiesAsync();

                return new DepositCommandResponseV1()
                {
                    CommandId = request.CommandId,
                    TransactionId = externalTransactionId
                };
            }
            else
            {
                throw new Exception($"{nameof(DepositCommandHandler)}: Account '{request.AccountNumber}' not found.");
            }
        }
    }
}
