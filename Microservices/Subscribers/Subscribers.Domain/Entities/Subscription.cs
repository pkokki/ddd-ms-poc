using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Subscribers.Domain.Entities
{
    public class Subscription : ConcurrencySafeEntity, IAggregateRoot
    {
        private readonly List<Product> products;

        protected Subscription()
        {
            products = new List<Product>();
        }
        public string SubType { get; private set; }
        public bool IsActive { get; private set; }
        public IEnumerable<Product> Products => products.AsReadOnly();

        public bool HasPermission(string productNumber)
        {
            return IsActive && products.Any(p => p.Number == productNumber && p.ValidTo <= DateTime.UtcNow);
        }
    }
}
