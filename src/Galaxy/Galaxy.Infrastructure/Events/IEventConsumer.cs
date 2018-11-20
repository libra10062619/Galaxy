using System;
using System.Collections.Generic;
using Galaxy.Infrastructure.Messaging;

namespace Galaxy.Infrastructure.Events
{
    public interface IEventConsumer : IMessageConsumer
    {
        //void Subscribe(IEnumerable<string> topics);

        //event EventHandler<MessageEventArgs> OnMessageReceived;
    }
}


