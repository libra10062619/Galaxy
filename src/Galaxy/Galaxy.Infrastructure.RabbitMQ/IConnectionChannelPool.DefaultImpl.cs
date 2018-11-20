using System;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Collections.Concurrent;
using System.Threading;
using System.Diagnostics;

namespace Galaxy.Infrastructure.RabbitMQ
{
    internal sealed class ConnectionChannelPool : DisposableObject,  IConnectionChannelPool
    {
        public string Host { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Exchange { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        readonly Func<IConnection> _connectionActivator;
        readonly ConcurrentQueue<IModel> _channlPool;
        readonly ILogger _logger;

        IConnection _connection;
        int _count;
        int _maxSize;

        public ConnectionChannelPool(RabbitMQOptions options, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ConnectionChannelPool>();
            _channlPool = new ConcurrentQueue<IModel>();

            _connectionActivator = () =>
            {
                var factory = new ConnectionFactory()
                {
                    UserName = options.Username,
                    Port = options.Port,
                    Password = options.Password,
                    VirtualHost = options.VirtualHost,
                    RequestedConnectionTimeout = options.RequestedConnectionTimeout,
                    SocketReadTimeout = options.SocketReadTimeout,
                    SocketWriteTimeout = options.SocketWriteTimeout
                };
                try
                {
                    return factory.CreateConnection(options.Host.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                }
                catch (Exception ex)
                {
                    return null;
                }

                //return options.Host.Contains(",") ? factory.CreateConnection(options.Host.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) : factory.CreateConnection(); 
            };
        }

        public IConnection Connection()
        {
            if (_connection != null && _connection.IsOpen)
            {
                return _connection;
            }

            _connection = _connectionActivator();
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            return _connection;
        }

        public IModel Rent()
        {
            if(_channlPool.TryDequeue(out IModel model))
            {
                Interlocked.Decrement(ref _count);

                Debug.Assert(_count >= 0);

                return model;
            }

            return Connection().CreateModel();
        }

        public bool Return(IModel connection)
        {
            if (Interlocked.Increment(ref _count) <= _maxSize)
            {
                _channlPool.Enqueue(connection);

                return true;
            }

            Interlocked.Decrement(ref _count);

            Debug.Assert(_maxSize == 0 || _channlPool.Count <= _maxSize);

            return false;
        }

        protected override void Disposing()
        {
            _maxSize = 0;
            while(_channlPool.TryDequeue(out var channel))
            {
                channel.Close();
                channel.Dispose();
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            _logger.LogWarning($"RabbitMQ client connection closed! --> {e.ReplyText}");
        }
    }
}
