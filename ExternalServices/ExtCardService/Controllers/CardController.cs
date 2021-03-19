using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExtAccountService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        private static readonly object theLock = new object();
        private static bool isStopped;
        private static readonly Dictionary<string, List<Transaction>> store = new Dictionary<string, List<Transaction>>();
        static CardController()
        {
            store.Add("C1", new List<Transaction>());
            store.Add("C2", new List<Transaction>());
            store.Add("C3", new List<Transaction>());
        }

        private readonly ILogger<CardController> logger;

        public CardController(ILogger<CardController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Home()
        {
            return Ok(isStopped ? "API stopped" : "API running");
        }

        [HttpPost]
        [Route("Fill")]
        public IActionResult Fill(Request request)
        {
            if (isStopped)
                return StatusCode(500);
            lock (theLock)
            {
                if (store.TryGetValue(request.ExtCardNumber, out List<Transaction> entries))
                {
                    var tx = new Transaction(request.ExtAmount);
                    entries.Add(tx);
                    return Ok(tx);
                }
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Pay")]
        public IActionResult Pay(Request request)
        {
            if (isStopped)
                return StatusCode(500);
            lock (theLock)
            {
                if (store.TryGetValue(request.ExtCardNumber, out List<Transaction> entries))
                {
                    if (entries.Sum(o => o.Amount) > request.ExtAmount)
                        return BadRequest();
                    var tx = new Transaction(-request.ExtAmount);
                    entries.Add(tx);
                    return Ok(tx);
                }
                return NotFound();
            }
        }

        [HttpGet]
        [Route("Start")]
        public IActionResult Start()
        {
            logger.LogInformation("Service is RUNNING.");
            isStopped = false;
            return Ok("RUNNING");
        }

        [HttpGet]
        [Route("Stop")]
        public IActionResult Stop()
        {
            logger.LogInformation("Service is STOPPED.");
            isStopped = true;
            return Ok("STOPPED");
        }

        [HttpGet]
        [Route("Toggle")]
        public IActionResult Toggle()
        {
            return isStopped ? Start() : Stop();
        }
    }

    public class Transaction
    {
        public Transaction(decimal amount)
        {
            Id = Guid.NewGuid().ToString();
            Amount = amount;
        }
        public string Id { get; }

        [JsonIgnore]
        public decimal Amount { get; }
    }
    public class Request
    {
        public string ExtCardNumber { get; set; }
        public decimal ExtAmount { get; set; }
    }

    public class Response
    {
        public string ExtId { get; set; }
    }
}
