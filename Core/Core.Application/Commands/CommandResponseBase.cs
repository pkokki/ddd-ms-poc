using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Commands
{
    public abstract class CommandResponseBase
    {
        public string CommandId { get; set; }
    }
}
