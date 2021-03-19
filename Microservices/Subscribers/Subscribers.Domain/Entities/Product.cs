using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subscribers.Domain.Entities
{
    public class Product : Entity
    {
        protected Product()
        {
        }

        public string Number { get; private set; }
        public ProductType Type { get; private set; }
        public string Name { get; private set; }
        public Currency Currency { get; private set; }
        public ProductStatus Status { get; private set; }
    }
}
