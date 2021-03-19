using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Commands
{
    public abstract class CommandBase<T> : IRequest<T> where T: CommandResponseBase
    {
        public string UserId { get; set; }
        public string CommandId { get; set; }
    }
}
