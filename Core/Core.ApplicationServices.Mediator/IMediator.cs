using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.ApplicationServices.Mediator
{
    public interface IMediator
    {
        Task<T> Send<T>(ICommand<T> command, CancellationToken ct = default) where T : ICommandResponse;
    }
}
