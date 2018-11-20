using System;
using Galaxy.Infrastructure.Messaging;
using Microsoft.Extensions.Logging;

namespace Galaxy.Infrastructure.RabbitMQ
{
    class RabbitMQConsumerFactory : IMessageConsumerFactory
    {
        readonly RabbitMQOptions _options;
        readonly ILoggerFactory _loggerFactory;
        readonly IConnectionChannelPool _connectionChannelPool;
        public RabbitMQConsumerFactory(RabbitMQOptions options, 
                                       IConnectionChannelPool connectionChannelPool,
                                       ILoggerFactory loggerFactory)
        {
            _options = options;
            _connectionChannelPool = connectionChannelPool;
            _loggerFactory = loggerFactory;
        }

        public IMessageConsumer Create(string groupId)
        {
            return new RabbitConsumer(groupId, _options, _connectionChannelPool, _loggerFactory);
        }
    }
}
