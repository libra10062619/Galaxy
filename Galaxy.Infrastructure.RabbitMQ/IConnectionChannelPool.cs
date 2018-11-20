using System;
using RabbitMQ.Client;
namespace Galaxy.Infrastructure.RabbitMQ
{
    public interface IConnectionChannelPool : IDisposable
    {
        string Host { get; set; }

        string Exchange { get; set; }

        IConnection Connection();

        IModel Rent();

        bool Return(IModel channel);
    }
}
