using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    /// <summary>
    /// A Layer Supertype that provides a basic surrogate primary key that is hidden from
    /// the view of clients.
    /// </summary>
    public abstract class IdentifiedDomainObject
    {
        protected int Uuid { get; set; }
    }
}
