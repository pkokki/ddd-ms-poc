using Subscribers.OpenHost.Models;
using System;
using System.Threading.Tasks;

namespace Subscribers.OpenHost
{
    public interface ISubscriberService
    {
        Task<SubscriberHasPermissionResponse> Execute(SubscriberHasPermissionRequest request);
    }
}
