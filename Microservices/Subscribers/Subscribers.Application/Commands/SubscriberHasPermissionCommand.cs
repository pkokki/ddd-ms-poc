using Core.Application.Commands;
using System;

namespace Subscribers.Application.Commands
{
    public class SubscriberHasPermissionCommand : CommandBase<SubscriberHasPermissionCommandResponse>
    {
        public string ProductNumber { get; set; }
    }

    public class SubscriberHasPermissionCommandResponse : CommandResponseBase
    {
        public bool Granted { get; set; }
    }
}
