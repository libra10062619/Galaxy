using System;
using Galaxy.Infrastructure.Events;
using Newtonsoft.Json;

namespace Galaxy.Infrastructure.EventStorage.MySql
{
    /// <summary>
    /// Event entity.
    /// </summary>
    internal sealed class EventEntity : IHasVersion
        //where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the aggregate root identifier.
        /// </summary>
        /// <value>The aggregate root identifier.</value>
        public string AggregateRootId { get; set; }
        /// <summary>
        /// Gets the content of the event.
        /// </summary>
        /// <value>The content of the event.</value>
        public string EventContent { get; }
        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>The version.</value>
        public int Version { get; set; }

        public EventEntity(){}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galaxy.Infrastructure.MySql.Storage.EventEntity"/> class.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="aggregateRootId">Aggregate root identifier.</param>
        /// <param name="version">Version.</param>
        /// <param name="eventContent">Event content.</param>
        private EventEntity(string id, string aggregateRootId, int version, string eventContent)
        {
            Id = id;
            AggregateRootId = aggregateRootId;
            Version = version;
            EventContent = eventContent;
        }

        /// <summary>
        /// Converts DomainEvent to db entity.
        /// </summary>
        /// <returns>The event entity.</returns>
        /// <param name="event">Event.</param>
        /// <typeparam name="TDomainEvent">The 1st type parameter.</typeparam>
        public static EventEntity ToEventEntity<TDomainEvent>(TDomainEvent @event)
            where TDomainEvent: DomainEvent
        {
            return new EventEntity(@event.Id, @event.AggregateRootId.ToString(), @event.Version, JsonConvert.SerializeObject(@event));
        }

        /// <summary>
        /// Converts db entity to DomainEvent
        /// </summary>
        /// <returns>The domain event.</returns>
        /// <param name="eventEntity">Event entity.</param>
        /// <typeparam name="TDomainEvent">The 1st type parameter.</typeparam>
        public static TDomainEvent ToDomainEvent<TDomainEvent>(EventEntity eventEntity)
        {
            return JsonConvert.DeserializeObject<TDomainEvent>(eventEntity.EventContent);
        }
    }
}
