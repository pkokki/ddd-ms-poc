using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subscribers.Domain.Entities
{
    public class ProductType : Entity
    {
        public string Name { get; private set; }
    }
}
