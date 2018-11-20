using System;
namespace Galaxy.Infrastructure.Messaging
{
    public interface IMessageConsumerFactory
    {
        IMessageConsumer Create(string groupId);
    }
}
