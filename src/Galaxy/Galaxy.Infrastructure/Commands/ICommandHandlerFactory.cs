using System;
using System.Collections.Generic;

namespace Galaxy.Infrastructure.Commands
{
    /// <summary>
    /// Command handler factory.
    /// </summary>
    public interface ICommandHandlerFactory
    {
        /// <summary>
        /// Gets the handler.
        /// </summary>
        /// <returns>The handler.</returns>
        /// <typeparam name="TCommand">The 1st type parameter.</typeparam>
        ICommandHandler<TCommand> GetHandler<TCommand>() where TCommand : Command;
    }
}
