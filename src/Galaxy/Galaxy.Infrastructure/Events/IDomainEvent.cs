using System;
namespace Galaxy.Infrastructure.Events
{
    public interface IDomainEvent : IHasVersion
    {
        string Id { get; }

        object AggregateRootId { get; }

        string EventName { get; }

        DateTime Timestamp { get; }
    }
}