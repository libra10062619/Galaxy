using System;
using System.Collections.Generic;
using System.Threading;

namespace Galaxy.Infrastructure.Messaging
{
	public interface IMessageConsumer : IDisposable
	{
        void Subscribe(IEnumerable<string> topics);

        void Listening(TimeSpan timeout, CancellationToken cancellationToken);

        void Commit();

        void Reject();

        event EventHandler<MessageEventArgs> OnMessageReceived;
        //IMessageSubscriber Subscriber { get; }
    }
}