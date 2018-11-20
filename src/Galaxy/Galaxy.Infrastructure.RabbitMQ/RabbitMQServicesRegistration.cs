using System;
using Galaxy.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Galaxy.Infrastructure.Messaging;

namespace Galaxy.Infrastructure.RabbitMQ
{
    internal sealed class RabbitMQServicesRegistration : GalaxyRegistration<RabbitMQOptions>
    {
        public RabbitMQServicesRegistration(Action<RabbitMQOptions> setupAction) : base(setupAction){}

        protected override void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IConnectionChannelPool, ConnectionChannelPool>();
            services.AddSingleton<IMessageConsumerFactory, RabbitMQConsumerFactory>();
            services.AddSingleton<IConnectionChannelPool, ConnectionChannelPool>();
            services.AddSingleton<IMessagePublisher, RabbitMQMessagePublisher>();
        }

        protected override void CheckRequirementServices(IServiceProvider provider)
        {
        }

        protected override void UnregisterServices(IServiceProvider provider)
        {
        }
    }
}
