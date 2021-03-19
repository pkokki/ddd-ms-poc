using Accounts.Application.Commands;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace MobileGateway.Models
{
    public class TransferRequest
    {
        public int DebitProductCurrency { get; set; }
        public string DebitProductCode { get; set; }
        public int DebitProductType { get; set; }
        public int DebitProductID { get; set; }

        public string CreditProductCode { get; set; }
        public int CreditProductType { get; set; }
        public int CreditProductID { get; set; }

        public int AmountCurrency { get; set; }
        public int TransferType { get; set; }
        public decimal Amount { get; set; }
        public string DebitReason { get; set; }
        public string CreditReason { get; set; }
        public IEnumerable<TransferDetailsItem> TransferDetails { get; set; }

        public DateTime ActivityDateTime { get; set; }
        public string UniqueIdentifier { get; set; }
        public string OTP { get; set; }

        internal TransferCommand ToCommand(string requestId, string deviceId, string version, ClaimsPrincipal user)
        {
            return new TransferCommand()
            {
                CommandId = requestId,
                UserId = user?.Identity?.Name
            };
        }
    }

    public class TransferDetailsItem
    {
        public int Name { get; set; }
        public int DataType { get; set; }
        public string Value { get; set; }
    }
}
