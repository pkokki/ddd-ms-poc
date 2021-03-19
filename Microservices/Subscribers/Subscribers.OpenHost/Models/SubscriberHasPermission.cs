using System;
using System.Collections.Generic;
using System.Text;

namespace Subscribers.OpenHost.Models
{
    public class SubscriberHasPermissionRequest
    {
        public string UserId { get; set; }
        public string ProductNumber { get; set; }
    }

    public class SubscriberHasPermissionResponse
    {
        public bool Granted { get; set; }
    }
}
