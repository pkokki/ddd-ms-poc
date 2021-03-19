using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    public interface IDomainEvent : INotification
    {
        int EventVersion { get; }
        DateTime OccurredOn { get; }
    }
}
