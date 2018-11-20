using System;
using System.Threading.Tasks;
namespace Galaxy.Infrastructure
{
    /// <summary>
    /// Handler.
    /// </summary>
    public interface IHandler {}

    /// <summary>
    /// Handler.
    /// </summary>
    public interface IHandler<TMessage> : IHandler
    {
        /// <summary>
        /// Handles the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="message">Message.</param>
        Task HandleAsync(TMessage message);
    }
}
