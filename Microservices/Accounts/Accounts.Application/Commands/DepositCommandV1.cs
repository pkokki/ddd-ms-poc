using Core.Application.Commands;
using System;

namespace Accounts.Application.Commands
{
    public class DepositCommandV1 : CommandBase<DepositCommandResponseV1>
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }

    public class DepositCommandResponseV1 : CommandResponseBase
    {
        public string TransactionId { get; set; }
    }
}
