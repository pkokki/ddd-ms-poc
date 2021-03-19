using System;

namespace Core.Domain
{
    /// <summary>
    /// A Layer Supertype [Fowler, P of EAA] used to manage surrogate identity 
    /// and optimistic concurrency versioning, as explained in Entities(5).
    /// </summary>
    public class ConcurrencySafeEntity : Entity
    {
        private int concurrencyVersion;

        protected ConcurrencySafeEntity()
        {
            concurrencyVersion = 1;
        }

        public int ConcurrencyVersion
        {
            get { return concurrencyVersion; }
            protected set
            {
                FailWhenConcurrencyViolation(value);
                concurrencyVersion = value;
            }
        }

        public void FailWhenConcurrencyViolation(int version)
        {
            if (version != ConcurrencyVersion)
            {
                throw new InvalidOperationException(
                        "Concurrency Violation: Stale data detected. Entity was already modified.");
            }
        }
    }
}
