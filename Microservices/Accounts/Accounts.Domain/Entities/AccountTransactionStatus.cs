using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Domain.Entities
{
    public enum AccountTransactionStatus
    {
        PENDING = 1,
        COMPLETED = 2,
        FAILED = 3
    }
}
