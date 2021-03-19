using Core.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Application.Commands
{
    public class WithdrawCommandV1 : CommandBase<WithdrawCommandResponseV1>
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }

    public class WithdrawCommandResponseV1 : CommandResponseBase
    {
        public decimal TransactionId { get; set; }
    }
}
