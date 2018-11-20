using System;
namespace Galaxy.Infrastructure.Events
{
    public interface IDomainEventHandler{}

    public interface IDomainEventHandler<TEvent> : IHandler<TEvent>, IDomainEventHandler where TEvent : class, IDomainEvent
    {

    }
}
