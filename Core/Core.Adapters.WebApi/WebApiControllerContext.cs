using Core.ApplicationServices.Mediator;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Adapters.WebApi
{
    public class WebApiControllerContext<TController> : IWebApiControllerContext<TController>
    {
        public WebApiControllerContext(IMediator mediator, ILogger<TController> logger)
        {
            Mediator = mediator;
            Logger = logger ?? new NullLogger<TController>();
        }

        public ILogger Logger { get; }
        public IMediator Mediator { get; }
    }
}
