using System;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Infrastructure.Messaging;
using RabbitMQ.Client;
using Microsoft.Extensions.Logging;

namespace Galaxy.Infrastructure.RabbitMQ
{
    internal class RabbitMQMessagePublisher : DisposableObject, IMessagePublisher
    {
        readonly RabbitMQOptions _options;
        private readonly IConnectionChannelPool _connectionChannelPool;


        public RabbitMQMessagePublisher(RabbitMQOptions options,
                                        IConnectionChannelPool connectionChannelPool,
                                        ILoggerFactory loggerFactory)
        {
            _options = options;
            _connectionChannelPool = connectionChannelPool;
        }

        public async Task<Result> PublishAsync(string topic, string message)
        {
            var channel = _connectionChannelPool.Rent();

            try
            {
                var body = Encoding.UTF8.GetBytes(message);
                channel.ExchangeDeclare(_options.ExchangeName, _options.ExchangeType, true);
                channel.BasicPublish(_options.ExchangeName, topic, null, body);

                //_logger.LogDebug($"RabbitMQ topic message [{topic}] has been published.");
                await Task.CompletedTask;
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failed(ex.Message);
            }
            finally
            {
                var returned = _connectionChannelPool.Return(channel);
                if (!returned)
                {
                    channel.Dispose();
                }
            }
        }

        protected override void Disposing()
        {
            ;
        }
    }
}
