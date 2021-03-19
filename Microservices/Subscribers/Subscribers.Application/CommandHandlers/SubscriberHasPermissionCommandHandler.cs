using Core.Application.Commands;
using Subscribers.Application.Commands;
using Subscribers.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Subscribers.Application.CommandHandlers
{
    public class SubscriberHasPermissionCommandHandler : CommandHandlerBase<SubscriberHasPermissionCommand, SubscriberHasPermissionCommandResponse>
    {
        private readonly ISubscriberRepository subscriberRepository;

        public SubscriberHasPermissionCommandHandler(ISubscriberRepository subscriberRepository)
        {
            this.subscriberRepository = subscriberRepository ?? throw new ArgumentNullException(nameof(subscriberRepository));
        }

        public override async Task<SubscriberHasPermissionCommandResponse> Handle(SubscriberHasPermissionCommand request, CancellationToken cancellationToken)
        {
            var subscription = await subscriberRepository.GetSubscriptionByUserId(request.UserId);
            if (subscription != null)
            {
                var granted = subscription.HasPermission(request.ProductNumber);

                return new SubscriberHasPermissionCommandResponse()
                {
                    CommandId = request.CommandId,
                    Granted = granted
                };
            }
            else
            {
                throw new Exception($"{nameof(SubscriberHasPermissionCommandHandler)}: Subscription for user '{request.UserId}' not found.");
            }
        }
    }
}
