using System;
using Galaxy.Infrastructure;
using Galaxy.Infrastructure.RabbitMQ;
namespace Microsoft.Extensions.DependencyInjection
{
    public static class RabbitMQOptionsExtensions
    {
        public static GalaxyOptions AddRabbitMQ(this GalaxyOptions options, Action<RabbitMQOptions> steupAction)
        {
            options.RegisterServiceExtensions(new RabbitMQServicesRegistration(steupAction));
            return options;
        }
    }
}
