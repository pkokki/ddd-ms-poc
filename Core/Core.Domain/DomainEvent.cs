using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    public abstract class DomainEvent : IDomainEvent
    {
        protected DomainEvent(int version = 1, DateTime? occuredOn = null)
        {
            EventVersion = version;
            OccurredOn = occuredOn ?? DateTime.UtcNow;
        }

        public int EventVersion { get; private set; }
        public DateTime OccurredOn { get; private set; }
    }
}
