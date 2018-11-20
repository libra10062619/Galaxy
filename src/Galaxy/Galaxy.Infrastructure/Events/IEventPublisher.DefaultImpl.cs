using System;
using System.Threading.Tasks;
using Galaxy.Infrastructure.Events;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Galaxy.Infrastructure.Messaging
{
    internal sealed class EventPublisher : DisposableObject, IEventPublisher
    {
        readonly IMessagePublisher _messagePublisher;

        public EventPublisher(IMessagePublisher messagePublisher,
                              ILoggerFactory loggerFactory)
        {
            _messagePublisher = messagePublisher;
        }

        public async Task<Result> PublishAsync<TEvent>(TEvent @event)
            where TEvent : DomainEvent
        {
            var content = JsonConvert.SerializeObject(@event);
            return await _messagePublisher.PublishAsync(@event.EventName, content);
        }

        protected override void Disposing()
        {
            _messagePublisher.Dispose();
        }
    }
}
