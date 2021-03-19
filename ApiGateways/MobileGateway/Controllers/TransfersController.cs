using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Accounts.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MobileGateway.Models;

namespace MobileGateway.Controllers
{
    [ApiController]
    [Route("retail/rest-api/[controller]")]
    public class TransfersController : ControllerBase
    {
        private readonly ILogger<TransfersController> logger;
        private readonly IAccountService accountService;

        public TransfersController(IAccountService accountService, ILogger<TransfersController> logger)
        {
            this.logger = logger;
            this.accountService = accountService;
        }

        [HttpGet]
        [Route("")]
        public string Get()
        {
            return $"{nameof(TransfersController)} is running.";
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(TransferResponse), 201)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> NewTransfer(
            [FromBody] TransferRequest request,
            [FromHeader(Name = "request-id")] string requestId,
            [FromHeader(Name = "device-identifier")] string deviceId,
            [FromHeader(Name = "services-version")] string version
            )
        {
            var command = request.ToCommand(requestId, deviceId, version, User);
            var commandResult = await accountService.Transfer(command);
            var response = commandResult.ToResponse();
            return StatusCode(201, response);
        }
    }
}
