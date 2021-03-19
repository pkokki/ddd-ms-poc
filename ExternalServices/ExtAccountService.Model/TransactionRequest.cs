using System;
using System.Collections.Generic;
using System.Text;

namespace ExtAccountService.Model
{
    public class TransactionRequest
    {
        public string AccountNumber { get; set; }
        //public string CustomerNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
