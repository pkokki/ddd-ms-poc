using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ExtAccountService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExtAccountService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private static readonly object theLock = new object();
        private static bool isStopped;
        private static readonly Dictionary<string, List<Transaction>> store = new Dictionary<string, List<Transaction>>();
        static AccountController()
        {
            store.Add("A1", new List<Transaction>());
            store.Add("A2", new List<Transaction>());
            store.Add("A3", new List<Transaction>());
        }

        private readonly ILogger<AccountController> logger;

        public AccountController(ILogger<AccountController> logger)
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
        [Route("Transfer")]
        public IActionResult Transfer(TransactionRequest request)
        {
            if (isStopped)
                return StatusCode(500);
            lock (theLock)
            {
                if (store.TryGetValue(request.AccountNumber, out List<Transaction> entries))
                {
                    if (entries.Sum(o => o.Amount) < request.Amount)
                        return BadRequest();
                    var tx = new Transaction(-request.Amount);
                    entries.Add(tx);
                    return Ok(new TransactionResponse { TransactionId = tx.Id });
                }
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Deposit")]
        public IActionResult Deposit(TransactionRequest request)
        {
            if (isStopped)
                return StatusCode(500);
            return InnerDeposit(request.AccountNumber, request.Amount);
        }

        //[HttpPost]
        //[Route("Deposit")]
        //public IActionResult Deposit(TransactionRequest request)
        //{
        //    if (isStopped)
        //        return StatusCode(500);
        //    if (string.IsNullOrEmpty(request.CustomerNumber))
        //        return Unauthorized();
        //    return InnerDeposit(request.AccountNumber, request.Amount);
        //}

        private IActionResult InnerDeposit(string accountNumber, decimal amount)
        {
            lock (theLock)
            {
                if (store.TryGetValue(accountNumber, out List<Transaction> entries))
                {
                    var tx = new Transaction(amount);
                    entries.Add(tx);
                    return Ok(new TransactionResponse { TransactionId = tx.Id });
                }
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Withdraw")]
        public IActionResult Withdraw(TransactionRequest request)
        {
            if (isStopped)
                return StatusCode(500);
            lock (theLock)
            {
                if (store.TryGetValue(request.AccountNumber, out List<Transaction> entries))
                {
                    if (entries.Sum(o => o.Amount) < request.Amount)
                        return BadRequest();
                    var tx = new Transaction(-request.Amount);
                    entries.Add(tx);
                    return Ok(new TransactionResponse { TransactionId = tx.Id });
                }
                return NotFound();
            }
        }

        [HttpPost]
        [Route("Transfer")]
        public IActionResult Transfer(TransferTransactionRequest request)
        {
            if (isStopped)
                return StatusCode(500);
            lock (theLock)
            {
                if (store.TryGetValue(request.AccountNumber, out List<Transaction> entries))
                {
                    if (entries.Sum(o => o.Amount) < request.Amount)
                        return BadRequest();
                    var tx = new Transaction(-request.Amount);
                    entries.Add(tx);
                    return Ok(new TransferTransactionResponse 
                    {
                        TransactionUN = tx.Id,
                        TransactionAUN = Guid.NewGuid().ToString(),
                        ProcessDate = DateTime.Today
                    });
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

        public decimal Amount { get; }
    }
    
}
