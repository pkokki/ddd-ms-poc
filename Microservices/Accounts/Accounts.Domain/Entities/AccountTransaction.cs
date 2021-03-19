using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Domain.Entities
{
    public class AccountTransaction : Entity
    {
        private string requestId;
        private decimal amount;

        protected AccountTransaction()
        {
        }

        internal protected AccountTransaction(
            string requestId,
            AccountTransactionType type,
            AccountTransactionStatus status,
            decimal amount,
            DateTime? transactionDate = null,
            string externalId = null)
        {
            Status = status;
            Type = type;
            RequestId = requestId;
            TransactionUN = externalId;
            Amount = amount;
            TransactionDate = transactionDate ?? DateTime.UtcNow;
            ValidFrom = TransactionDate;
        }

        public AccountTransactionStatus Status { get; protected set; }
        public AccountTransactionType Type { get; protected set; }
        public string TransactionUN { get; protected set; }
        public DateTime TransactionDate { get; protected set; }

        public string RequestId
        {
            get { return requestId; }
            protected set
            {
                requestId = value ?? throw new ArgumentNullException(nameof(RequestId));
            }
        }
        public decimal Amount
        {
            get { return amount; }
            protected set
            {
                if (amount < 0)
                    throw new ArgumentException(nameof(Amount));
                amount = value;
            }
        }

        public void Complete()
        {
            Status = AccountTransactionStatus.COMPLETED;
        }
        public void Fail()
        {
            Status = AccountTransactionStatus.FAILED;
        }
    }
}
