using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Application
{
    public interface ITransactionPermissionService
    {
        bool CanDeposit(string userId, string accountNumber);
        bool CanWithdraw(string userId, string accountNumber);
        bool CanTrasferFromAccount(string userId, string accountNumber);
    }
}
