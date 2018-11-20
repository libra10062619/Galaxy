using System;

namespace Galaxy.Infrastructure.Events
{
    /// <summary>
    /// Domain event.
    /// </summary>
    public abstract class DomainEvent : IDomainEvent
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id => Guid.NewGuid().ToString();
        /// <summary>
        /// Gets or sets the tran identifier.
        /// </summary>
        /// <value>The tran identifier.</value>
        public string CommandId { get; set; }
        /// <summary>
        /// Gets the full name of the event.
        /// </summary>
        /// <value>The name of the event.</value>
        public string EventName => GetType().FullName;
        /// <summary>
        /// Gets or sets the aggregate root identifier.
        /// </summary>
        /// <value>The aggregate root identifier.</value>
        public object AggregateRootId { get; set; }
        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>The timestamp.</value>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public int Version { get; set; }

        #region Ctors

        protected DomainEvent() { }

        protected DomainEvent(string commandId)
        {
            CommandId = commandId;
        }

        protected DomainEvent(string commandId, object aggregateRootId)
        {
            CommandId = commandId;
            AggregateRootId = aggregateRootId;
        }

        #endregion
    }
}
