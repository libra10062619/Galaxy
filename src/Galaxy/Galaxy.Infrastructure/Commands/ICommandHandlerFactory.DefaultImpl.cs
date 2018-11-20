using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace Galaxy.Infrastructure.Commands
{
    /// <summary>
    /// Command handler factory.
    /// </summary>
    internal sealed class CommandHandlerFactory : ICommandHandlerFactory
    {
        /// <summary>
        /// The provider.
        /// </summary>
        readonly IEnumerable<ICommandHandler> _commandHandlers;
        readonly ConcurrentDictionary<string, string> _handlerMatcher;
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galaxy.Infrastructure.Commands.CommandHandlerFactory"/> class.
        /// </summary>
        /// <param name="provider">Provider.</param>
        public CommandHandlerFactory(IEnumerable<ICommandHandler> commandHandlers,
                                     ILoggerFactory loggerFactory)
        {
            //this.provider = provider;
            _commandHandlers = commandHandlers;
            _handlerMatcher = InitHandlerMatcher();
        }

        /// <summary>
        /// Gets the handler.
        /// </summary>
        /// <returns>The handler.</returns>
        /// <typeparam name="TCommand">The 1st type parameter.</typeparam>
        public ICommandHandler<TCommand> GetHandler<TCommand>() where TCommand : Command
        {
            return _handlerMatcher.TryGetValue(typeof(TCommand).Name, out var handlerName)
                ? (ICommandHandler<TCommand>)_commandHandlers.FirstOrDefault(p => p.GetType().Name.Equals(handlerName))
                : null;
        }

        /// <summary>
        /// Inits the handler matcher.
        /// </summary>
        /// <returns>The handler matcher.</returns>
        ConcurrentDictionary<string, string> InitHandlerMatcher()
        {
            var dic = new ConcurrentDictionary<string, string>();
            foreach (var handler in _commandHandlers)
            {
                var handlerName = handler.GetType().Name;
                var commandNames = handler.GetType().GetInterfaces()
                                          .Where(p => p.IsGenericType);
                Parallel.ForEach(commandNames, p =>
                {
                    dic.TryAdd(p.GenericTypeArguments[0].Name, handlerName);
                });
            }
            return dic;
        }
    }
}
