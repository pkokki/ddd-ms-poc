using Core.ApplicationServices.Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Core.Adapters.WebApi
{
    /// <summary>
    /// In the <b>Hexagonal (4)</b> or Ports and Adapters architecture, class 
    /// <see cref="WebApiController"/> is an <b>Adapter</b> for the WebAPI 
    /// HTTP Port provided by the <see cref="ControllerBase"/> implementation.
    /// A consumer makes a request in the form
    /// 
    /// <code>POST /api/{controller}/{operation}</code>
    /// 
    /// The Adapter delegates to an <see cref="IMediator" />, a core <b>Application Service (14)</b>
    /// that provides an API at the inner hexagon. Being a direct client of the domain
    /// model, the <see cref="IMediator" /> manages the use case task and transaction. 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")] 
    public class WebApiController : ControllerBase
    {
        private readonly IWebApiControllerContext<WebApiController> context;
        public WebApiController(IWebApiControllerContext<WebApiController> context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected ILogger Logger => context.Logger;
        protected IMediator Mediator => context.Mediator;
    }
}
