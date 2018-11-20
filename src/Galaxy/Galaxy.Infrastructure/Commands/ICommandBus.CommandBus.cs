using System;
using System.Threading.Tasks;
using Galaxy.Infrastructure.Messaging;
using Galaxy.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;

namespace Galaxy.Infrastructure.Commands
{
    /// <summary>
    /// Command bus.
    /// </summary>
    internal sealed class CommandBus : DisposableObject, ICommandBus
    {
        /// <summary>
        /// The command handler factory.
        /// </summary>
        readonly ICommandHandlerFactory commandHandlerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galaxy.Infrastructure.Commands.CommandBus"/> class.
        /// </summary>
        /// <param name="commandHandlerFactory">Command handler factory.</param>
        public CommandBus(ICommandHandlerFactory commandHandlerFactory, 
                          ILoggerFactory loggerFactory)
        {
            this.commandHandlerFactory = commandHandlerFactory;
        }

        /// <summary>
        /// Sends the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="command">Command.</param>
        /// <typeparam name="TCommand">The 1st type parameter.</typeparam>
        public async Task SendAsync<TCommand>(TCommand command) where TCommand : Command
        {
            var handler = commandHandlerFactory.GetHandler<TCommand>();

            if (null == handler)
                throw new UnregisteredHandlerException($"The handler of {typeof(TCommand).Name} is not registered");

            await handler.HandleAsync(command);
        }

        /// <summary>
        /// Dispose the specified disposing.
        /// </summary>
        /// <param name="disposing">If set to <c>true</c> disposing.</param>
        protected override void Disposing()
        {
            ;
        }
    }
}
