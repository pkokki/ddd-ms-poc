using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subscribers.Domain.Entities
{
    public class Currency : ValueObject
    {
        public Currency(string code, string description)
        {
            Code = code ?? throw new ArgumentNullException(nameof(Code));
            Description = description ?? "No description";
        }
        public string Code { get; private set; }
        public string Description { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            return new object[] { Code };
        }
    }
}
