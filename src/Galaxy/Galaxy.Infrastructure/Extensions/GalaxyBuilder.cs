using System;
using Galaxy.Infrastructure.Commands;
using Galaxy.Infrastructure.Helper;
using Galaxy.Infrastructure.Messaging;
using Galaxy.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Galaxy.Infrastructure.Extensions
{
    public sealed class GalaxyBuilder
    {
        public GalaxyOptions _options;

        public IServiceCollection Services { get; }

        public GalaxyBuilder(IServiceCollection services, Action<GalaxyOptions> setupAction)
        {
            if (null == setupAction) throw new ArgumentNullException(nameof(setupAction));

            Services = services;

            var options = new GalaxyOptions();
            setupAction(options);
            _options = options;
            Services.AddSingleton(options);

            AddCommonServices();

            AddExtensionServices();
        }

        GalaxyBuilder AddCommonServices()
        {
            Services.AddSingleton<IIdGenerator, Snowflake>();
            Services.AddSingleton<ITypeFinder, TypeFinder>();
            Services.AddSingleton<ILoggerFactory, LoggerFactory>();
            Services.AddSingleton<ICommandHandlerFactory, CommandHandlerFactory>();
            Services.AddSingleton<ICommandBus, CommandBus>();
            Services.AddSingleton<IEventPublisher, EventPublisher>();
            Services.AddSingleton(typeof(IDomainRepository<>), typeof(DomainRepository<>));
            Services.AddSingleton<IBootstrapper, Bootstrapper>();
            return this;
        }

        GalaxyBuilder AddExtensionServices()
        {
            foreach (var extension in _options.ExtensionRegistrations)
            {
                extension.Register(Services);
            }

            return this;
        }
    }
}
