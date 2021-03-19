using Accounts.Application.Commands;
using Accounts.Domain;
using Core.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Accounts.Application.CommandHandlers
{
    public class TransferCommandHandler : CommandHandlerBase<TransferCommand, TransferCommandResponse>
    {
        private readonly IAccountRepository accountRepository;
        private readonly ITransactionExecutionService transactionExecutionService;
        private readonly ITransactionPermissionService transactionPermissionService;

        public TransferCommandHandler(
            IAccountRepository accountRepository,
            ITransactionExecutionService transactionExecutionService,
            ITransactionPermissionService transactionPermissionService
            )
        {
            this.accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            this.transactionExecutionService = transactionExecutionService ?? throw new ArgumentNullException(nameof(transactionExecutionService));
            this.transactionPermissionService = transactionPermissionService ?? throw new ArgumentNullException(nameof(transactionPermissionService));
        }

        public override async Task<TransferCommandResponse> Handle(TransferCommand command, CancellationToken cancellationToken)
        {
            var account = await accountRepository.GetAccountWithNumber(command.DebitAccountNumber);
            if (account != null)
            {
                // Check permissions
                if (!transactionPermissionService.CanTrasferFromAccount(command.UserId, command.DebitAccountNumber))
                    throw new Exception($"{nameof(TransferCommandHandler)}: User '{command.UserId}' can not traansfer from '{command.DebitAccountNumber}'.");

                // Append to local storage
                var transaction = account.CreatePendingTransferTransaction(
                    command.CommandId, 
                    command.ActivityDateTime, 
                    command.CreditAccountNumber,
                    command.Amount,
                    command.DebitReason,
                    command.CreditReason,
                    command.Message
                    );
                await accountRepository.UnitOfWork.SaveEntitiesAsync();

                // Call external service
                var externalTransaction = await transactionExecutionService.TransferAsync(transaction);

                // Update local storage
                var accountTransaction = account.MarkExternalTransactionComplete(
                    command.CommandId, 
                    externalTransaction.ExternalId);
                await accountRepository.UnitOfWork.SaveEntitiesAsync();

                return new TransferCommandResponse()
                {
                    CommandId = command.CommandId,
                    TransactionAUN = accountTransaction.TransactionAUN,
                    TransactionUN = accountTransaction.TransactionUN,
                    ProcessDate = accountTransaction.ProcessDate,
                    SubmittedTimeStamp = accountTransaction.TransactionDate,
                };
            }
            else
            {
                throw new Exception($"{nameof(TransferCommandHandler)}: Account '{command.DebitAccountNumber}' not found.");
            }
        }
    }
}
