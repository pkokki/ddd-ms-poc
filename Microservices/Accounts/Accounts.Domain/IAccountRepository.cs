using Accounts.Domain.Entities;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account Add(Account account);
        void Update(Account account);

        Task<Account> GetAccountWithNumber(string accountNumber);
    }
}
