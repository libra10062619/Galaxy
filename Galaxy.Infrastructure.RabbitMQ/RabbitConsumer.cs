using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Galaxy.Infrastructure.Messaging;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Galaxy.Infrastructure.RabbitMQ
{
    internal class RabbitConsumer : DisposableObject, IMessageConsumer
    {
        readonly string _queueName;
        readonly RabbitMQOptions _options;
        readonly IConnectionChannelPool _connectionChannelPool;
        readonly Lazy<IConnection> _connection;
        readonly Lazy<IModel> _channel;

        ulong _deliveryTag;

        public event EventHandler<MessageEventArgs> OnMessageReceived;

        public RabbitConsumer(string queueName, 
                              RabbitMQOptions options, 
                              IConnectionChannelPool connectionChannelPool,
                              ILoggerFactory loggerFactory)
        {
            _queueName = queueName;
            _options = options;
            _connectionChannelPool = connectionChannelPool;

            _connection = new Lazy<IConnection>(() =>
            {
                return _connectionChannelPool.Connection();
            });

            _channel = new Lazy<IModel>(() =>
            {
                var channel = _connection.Value.CreateModel();
                channel.ExchangeDeclare(_options.ExchangeName, _options.ExchangeType, true);

                var arguments = new Dictionary<string, object>
                {
                        {"x-message-ttl", _options.MessageExpriesInMill}
                };
                channel.QueueDeclare(_queueName, true, false, false, arguments);
                return channel;
            });
        }

        public void Commit()
        {
            _channel.Value.BasicAck(_deliveryTag, false);
        }


        public void Listening(TimeSpan timeout, CancellationToken cancellationToken)
        {
            var consumer = new EventingBasicConsumer(_channel.Value);
            consumer.Received += OnConsumerReceived;
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.Value.BasicConsume(_queueName, false, consumer);

            while (!cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
                cancellationToken.WaitHandle.WaitOne(timeout);
            }
        }

        public void Reject()
        {
            _channel.Value.BasicReject(_deliveryTag, true);
        }

        public void Subscribe(IEnumerable<string> topics)
        {
            if (topics == null)
            {
                throw new ArgumentNullException(nameof(topics));
            }

            foreach (var topic in topics)
            {
                _channel.Value.QueueBind(_queueName, _options.ExchangeName, topic);
            }
        }

        protected override void Disposing()
        {
            _channel.Value.Close();
            _channel.Value.Dispose();
            _connection.Value.Close();
            _connection.Value.Dispose();
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)
        {
            ;
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
            ;
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
            ;
        }

        private void OnConsumerReceived(object sender, BasicDeliverEventArgs e)
        {
            _deliveryTag = e.DeliveryTag;
            var message = new MessageEventArgs(e.RoutingKey, Encoding.UTF8.GetString(e.Body));
            OnMessageReceived?.Invoke(sender, message);
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
            //var args = new LogMessageEventArgs
            //{
            //    LogType = MqLogType.ConsumerShutdown,
            //    Reason = e.ReplyText
            //};
            //OnLog?.Invoke(sender, args);
        }
    }
}