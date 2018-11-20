using System;
namespace Galaxy.Infrastructure.Commands
{
    /// <summary>
    /// Command handler.
    /// </summary>
    public interface ICommandHandler
    {
    }

    /// <summary>
    /// Command handler.
    /// </summary>
    public interface ICommandHandler<TCommand> : IHandler<TCommand>, ICommandHandler
    {
    }
}
