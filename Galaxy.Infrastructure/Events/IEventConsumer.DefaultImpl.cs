using System;
using System.Collections.Generic;
using System.Threading;
using Galaxy.Infrastructure.Messaging;

namespace Galaxy.Infrastructure.Events
{
    //internal sealed class EventConsumer : DisposableObject, IEventConsumer
    //{
    //    internal bool disposed;

    //    private readonly IMessageConsumerFactory consumerFactory;

    //    public IMessageConsumer MessageConsumer { get; private set; }

    //    public EventConsumer(string groupId, IMessageConsumerFactory consumerFactory)
    //    {
    //        this.consumerFactory = consumerFactory;
    //        this.MessageConsumer = consumerFactory.Create(groupId);
    //        this.MessageConsumer.OnMessageReceived += ConsumerClient_OnMessage;
    //    }

    //    public event EventHandler<MessageEventArgs> OnMessageReceived;

    //    public void Subscribe(IEnumerable<string> topics)
    //    {
    //        this.MessageConsumer.Subscribe(topics);
    //    }

    //    public void Listening(TimeSpan timeout, CancellationToken cancellationToken)
    //    {
    //        MessageConsumer.Listening(timeout, cancellationToken);
    //    }

    //    public void Commit()
    //    {
    //        MessageConsumer.Commit();
    //    }

    //    public void Reject()
    //    {
    //        MessageConsumer.Reject();
    //    }

    //    void ConsumerClient_OnMessage(object sender, MessageEventArgs e)
    //    {
    //        OnMessageReceived?.Invoke(sender, e);
    //    }

    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //        {
    //            if (!disposed)
    //            {
    //                this.MessageConsumer.Dispose();
    //                disposed = true;
    //            }
    //        }
    //    }
    //}
}
