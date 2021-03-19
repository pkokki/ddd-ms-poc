using Core.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Application.Commands
{
    public class TransferCommand : CommandBase<TransferCommandResponse>
    {
        public string DebitAccountNumber { get; set; }
        public object CreditAccountNumber { get; set; }
        public object Amount { get; set; }
        public object DebitReason { get; set; }
        public object CreditReason { get; set; }
        public object Message { get; set; }
        public object ActivityDateTime { get; set; }
    }

    public class TransferCommandResponse : CommandResponseBase
    {
        public string TransactionAUN { get; set; }
        public string TransactionUN { get; set; }
        public DateTime ProcessDate { get; set; }
        public DateTime SubmittedTimeStamp { get; set; }
    }
}
