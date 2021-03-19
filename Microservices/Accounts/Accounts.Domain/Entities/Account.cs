using Accounts.Domain.Events;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Domain.Entities
{
    public class Account : ConcurrencySafeEntity, IAggregateRoot
    {
        private readonly ICollection<AccountTransaction> transactions;
        private string accountNumber;

        protected Account()
        {
            transactions = new List<AccountTransaction>();
        }

        public Account(
            string accountNumber
            )
        {
            AccountNumber = accountNumber;
            ValidFrom = DateTime.UtcNow;

            AddDomainEvent(
                new AccountCreated(
                    AccountNumber
                ));
        }

        protected string AccountNumber
        {
            get { return accountNumber; }
            set
            {
                accountNumber = value ?? throw new ArgumentNullException(nameof(AccountNumber));
            }
        }

        public AccountTransaction CreatePendingDepositTransaction(string requestId, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException($"The amount to deposit must be greater than 0.");

            var transaction = new AccountTransaction(
                requestId,
                AccountTransactionType.DEPOSIT,
                AccountTransactionStatus.PENDING,
                amount
                );
            transactions.Add(transaction);
            return transaction;
        }

        public void Withdraw(string requestId, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException($"The amount to withdraw must be greater than 0.");

            var transaction = new AccountTransaction(
                requestId,
                AccountTransactionType.DEPOSIT,
                AccountTransactionStatus.PENDING,
                amount
                );
            transactions.Add(transaction);
        }

        public AccountTransaction CreatePendingTransferTransaction(string commandId, object activityDateTime, object creditAccountNumber, object amount, object debitReason, object creditReason, object message)
        {
            throw new NotImplementedException();
        }

        public void MarkExternalTransactionComplete(string requestId, string externalId)
        {
            if (string.IsNullOrEmpty(requestId))
                throw new ArgumentException($"{nameof(Account)}.{nameof(MarkExternalTransactionComplete)}: {nameof(requestId)} cannot be null or empty.");
            if (string.IsNullOrEmpty(externalId))
                throw new ArgumentException($"{nameof(Account)}.{nameof(MarkExternalTransactionComplete)}: {nameof(externalId)} cannot be null or empty.");

            var transaction = transactions.FirstOrDefault(o => o.RequestId == requestId);
            if (transaction == null)
                throw new Exception($"{nameof(Account)}.{nameof(MarkExternalTransactionComplete)}: Transaction with {nameof(requestId)} '{requestId}' not found.");
            transaction.Complete();
        }

        public object MarkExternalTransactionComplete(string commandId, object externalId)
        {
            throw new NotImplementedException();
        }
    }
}
