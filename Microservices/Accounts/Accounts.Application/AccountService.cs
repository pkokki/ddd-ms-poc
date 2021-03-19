using Accounts.Application;
using Accounts.Application.Commands;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Accounts.Application
{
    /// <summary>
    /// <see cref="AccountService"/> is an <b>Application Service (14)</b> in the Accounts Microservice. 
    /// </summary>
    /// <remarks>
    /// <b>Application Services (14)</b> are different from <b>Domain Services(7)</b> and are thus devoid of 
    /// domain logic. They may control persistence transactions and security. They may also be in charge of 
    /// sending Event-based notifications to other systems and/or for composing e-mail messages to be sent to users.
    /// 
    /// The Application Services in this layer are the direct clients of the domain model, though 
    /// themselves possessing no business logic. They remain very lightweight, coordinating operations performed
    /// against domain objects, such as <b>Aggregates (10)</b>. They are the primary means of expressing use cases 
    /// or user stories on the model. Hence, a common function of an Application Service is to accept parameters 
    /// from the User Interface, use a <b>Repository (12)</b> to obtain an Aggregate instance, and then execute some
    /// command operation on it.
    /// 
    /// If our Application Services become much more complex than this, it is probably
    /// an indication that domain logic is leaking into the Application Services,
    /// and that the model is becoming anemic. So it’s a best practice to keep these
    /// model clients very thin. When a new Aggregate must be created, an Application
    /// Service would use a <b>Factory (11)</b> or the Aggregate’s constructor to instantiate
    /// it and then use the corresponding Repository to persist it. An Application
    /// Service may also use a Domain Service to fulfill some domain-specific task
    /// designed as a stateless operation. [pg 120]
    /// </remarks>
    public class AccountService : IAccountService
    {
        private readonly IMediator mediator;

        public AccountService(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<DepositCommandResponseV1> Deposit(DepositCommandV1 command)
        {
            var result = await mediator.Send(command);
            return result;
        }

        public async Task<WithdrawCommandResponseV1> Withdraw(WithdrawCommandV1 command)
        {
            var result = await mediator.Send(command);
            return result;
        }

        public async Task<TransferCommandResponse> Transfer(TransferCommand command)
        {
            var result = await mediator.Send(command);
            return result;
        }
    }
}
