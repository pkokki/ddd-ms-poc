using Accounts.Application;
using System;

namespace Accounts.Infrastructure.TransactionPermissions
{
    public class TransactionPermissionService : ITransactionPermissionService
    {
        public TransactionPermissionService(SubscriberPermissionsAdapter adapter)
        {
            Adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
        }

        private SubscriberPermissionsAdapter Adapter { get; }

        public bool CanDeposit(string userId, string accountNumber)
        {
            return Adapter.HasPermission(userId, "Deposit", accountNumber);
        }

        public bool CanTrasferFromAccount(string userId, string accountNumber)
        {
            return Adapter.HasPermission(userId, "Withdraw", accountNumber);
        }

        public bool CanWithdraw(string userId, string accountNumber)
        {
            return Adapter.HasPermission(userId, "Withdraw", accountNumber);
        }
    }
}
