using MediatR;
using Subscribers.Application.Commands;
using Subscribers.OpenHost;
using Subscribers.OpenHost.Models;
using System;
using System.Threading.Tasks;

namespace Subscribers.Application
{
    public class SubscriberService : ISubscriberService
    {
        private readonly IMediator mediator;

        public SubscriberService(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<SubscriberHasPermissionResponse> Execute(SubscriberHasPermissionRequest request)
        {
            var command = new SubscriberHasPermissionCommand()
            {
                UserId = request.UserId,
                ProductNumber = request.ProductNumber
            };
            var result = await mediator.Send(command);
            var response = new SubscriberHasPermissionResponse()
            {
                Granted = result.Granted
            };
            return response;
        }
    }
}
