using System;
using System.Threading.Tasks;
using Galaxy.Infrastructure.Commands;

namespace Galaxy.Infrastructure.Messaging
{
    /// <summary>
    /// Command bus interface.
    /// </summary>
	public interface ICommandBus
    {
        /// <summary>
        /// Sends command async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="command">Command.</param>
        /// <typeparam name="TCommand">The 1st type parameter.</typeparam>
        Task SendAsync<TCommand>(TCommand command) where TCommand : Command;
    }
}
