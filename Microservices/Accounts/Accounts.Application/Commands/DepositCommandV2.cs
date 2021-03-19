using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Application.Commands
{
    public class DepositCommandV2 : DepositCommandV1
    {
        public string CustomerNumber { get; set; }
    }
}
