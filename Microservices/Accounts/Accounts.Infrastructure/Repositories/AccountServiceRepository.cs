using Accounts.Domain;
using Accounts.Domain.Entities;
using Core.Domain;
using System;
using System.Threading.Tasks;

namespace Accounts.Infrastructure.Repositories
{
    public class AccountServiceRepository : IAccountRepository
    {
        public AccountServiceRepository()
        {
        }

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public Account Add(Account account)
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetAccountWithNumber(string accountNumber)
        {
            throw new NotImplementedException();
        }

        public void Update(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
