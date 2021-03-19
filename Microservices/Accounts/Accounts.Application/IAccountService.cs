using Accounts.Application.Commands;
using System;
using System.Threading.Tasks;

namespace Accounts.Application
{
    /// <summary>
    /// <see cref="IAccountService"/> is an <b>Open Host Service (13)</b>: It defines a protocol 
    /// that gives access to the Accounts subsystem as a set of services. All who need to integrate
    /// with Accounts can use it.
    /// </summary>
    /// <remarks>
    /// We generally think of <b>Open Host Service</b> as a remote procedure call(RPC) API, but it can be
    /// implemented using message exchange also.
    /// 
    /// The translation between the models of two Bounded Contexts requires a common language. We use 
    /// a well-documented shared language (<b>Published Language</b>) that can express the necessary 
    /// domain information as a common medium of communication, translating as necessary into and out 
    /// of that language. <b>Published Language</b> is often combined with <b>Open Host Service</b>.
    /// 
    /// A <b>Published Language</b> is also used in an <b>Event-Driven Architecture(4)</b>, where <b>Domain Events(8)</b>
    /// are delivered as messages to subscribing interested parties.
    /// </remarks>
    public interface IAccountService
    {
        Task<DepositCommandResponseV1> Deposit(DepositCommandV1 command);
        Task<WithdrawCommandResponseV1> Withdraw(WithdrawCommandV1 command);
        Task<TransferCommandResponse> Transfer(TransferCommand command);
    }
}
