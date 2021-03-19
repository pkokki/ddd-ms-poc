using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Domain.Events
{
    public class AccountCreated : DomainEvent
    {
        public AccountCreated(string accountNumber)
        {
            AccountNumber = accountNumber;
        }

        public string AccountNumber { get; }
    }
}
