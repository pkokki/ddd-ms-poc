using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Infrastructure.TransactionPermissions
{
    /// <summary>
    /// The <see cref="TransactionPermissionService"/>, <see cref="SubscriberPermissionsAdapter"/>
    /// and <see cref=""/>
    /// form an <b>Anticorruption Layer (3)</b>, the means by which the Account Context will 
    /// interact with the Subscriber Context and translate the subscription permisssions
    /// representation into a Value Object for a specific kind of Account.
    /// </summary>
    public class SubscriberPermissionsAdapter
    {
        public bool HasPermission(string userId, string permission, string accountNumber)
        {
            // Call some client of Subscribers
            throw new NotImplementedException();
        }
    }
}
