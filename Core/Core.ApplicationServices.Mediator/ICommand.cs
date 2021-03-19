using System;

namespace Core.ApplicationServices.Mediator
{
    public interface ICommand<T> where T: ICommandResponse
    {
        string CommandId { get; set; }
    }

    public class BaseCommand<T> : ICommand<T> where T : ICommandResponse
    {
        public string CommandId { get; set; }
    }
}
