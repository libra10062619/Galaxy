using System;

namespace Galaxy.Infrastructure.RabbitMQ
{
    public sealed class RabbitMQOptions
    {
        public string Host { get; set; } = "localhost";

        public string VirtualHost { get; set; } = "/";

        public int Port { get; set; } = -1;

        public string Username { get; set; } = "guest";

        public string Password { get; set; } = "guest";

        public string ExchangeType { get; set; } = "topic";

        public string ExchangeName { get; set; } = "galaxy.default.router";

        public int RequestedConnectionTimeout { get; set; } = 60 * 1000;

        public int SocketReadTimeout { get; set; } = 60 * 1000;

        public int SocketWriteTimeout { get; set; } = 60 * 1000;

        public int MessageExpriesInMill { get; set; } = 15 * 24 * 3600 * 1000;
    }
}
