using Subscribers.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Subscribers.Domain
{
    public interface ISubscriberRepository
    {
        Task<Subscription> GetSubscriptionByUserId(string userId);
    }
}
