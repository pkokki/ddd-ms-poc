using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    public abstract class Entity : IdentifiedDomainObject
    {
        private int? _requestedHashCode;
        private List<INotification> _domainEvents;

        protected Entity()
        {
            ValidFrom = DateTime.MinValue.ToUniversalTime();
            ValidTo = DateTime.MaxValue.ToUniversalTime();
        }

        public virtual DateTime ValidFrom { get; protected set; }
        public virtual DateTime ValidTo { get; protected set; }
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public bool IsTransient()
        {
            return Uuid == default;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            Entity item = (Entity)obj;

            if (item.IsTransient() || IsTransient())
                return false;
            else
                return item.Uuid == Uuid;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = Uuid.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();

        }
        public static bool operator ==(Entity left, Entity right)
        {
            if (Equals(left, null))
                return (Equals(right, null));
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
