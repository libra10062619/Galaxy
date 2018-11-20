using System;
namespace Galaxy.Infrastructure.Commands
{
    /// <summary>
    /// Command.
    /// </summary>
    public abstract class Command : ICommand
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public int Version { get; set; }
    }
}
