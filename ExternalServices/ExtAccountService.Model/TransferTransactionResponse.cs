using System;
using System.Collections.Generic;
using System.Text;

namespace ExtAccountService.Model
{
    public class TransferTransactionResponse
    {
        public string TransactionUN { get; set; }
        public string TransactionAUN { get; set; }
        public DateTime ProcessDate { get; set; }
    }
}
