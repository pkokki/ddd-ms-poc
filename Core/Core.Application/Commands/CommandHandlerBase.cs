using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Commands
{
    public abstract class CommandHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
