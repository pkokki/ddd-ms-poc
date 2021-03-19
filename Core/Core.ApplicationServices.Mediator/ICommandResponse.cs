using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ApplicationServices.Mediator
{
    public interface ICommandResponse
    {
        string CommandId { get; set; }
    }

    public class BaseCommandResponse : ICommandResponse
    {
        public string CommandId { get; set; }
    }
}
