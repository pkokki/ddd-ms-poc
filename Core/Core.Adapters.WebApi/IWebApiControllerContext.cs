using Core.ApplicationServices.Mediator;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Adapters.WebApi
{
    public interface IWebApiControllerContext<TCategoryName>
    {
        ILogger Logger { get; }
        IMediator Mediator { get; }
    }
}
