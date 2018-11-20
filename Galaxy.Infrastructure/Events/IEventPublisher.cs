using System;
using System.Threading.Tasks;
using Galaxy.Infrastructure.Events;

namespace Galaxy.Infrastructure.Messaging
{
	public interface IEventPublisher 
    {
        Task<Result> PublishAsync<TEvent>(TEvent @event) where TEvent : DomainEvent;
    }
}