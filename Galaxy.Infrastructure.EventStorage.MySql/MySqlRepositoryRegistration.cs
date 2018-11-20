using System;
using Galaxy.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Galaxy.Infrastructure.Exceptions;
using Galaxy.Infrastructure.Repository;
using Microsoft.Extensions.Logging;

namespace Galaxy.Infrastructure.EventStorage.MySql
{
    /// <summary>
    /// My sql repository registration.
    /// </summary>
    internal class MySqlRepositoryRegistration : GalaxyRegistration<MySqlRepositoryOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galaxy.Infrastructure.MySql.MySqlRepositoryRegistration"/> class.
        /// </summary>
        /// <param name="setupAction">Setup action.</param>
        public MySqlRepositoryRegistration(Action<MySqlRepositoryOptions> setupAction) : base(setupAction)
        {
        }

        /// <summary>
        /// Checks the requirement.
        /// </summary>
        /// <param name="provider">Provider.</param>
        protected override void CheckRequirementServices(IServiceProvider provider)
        {
            if (string.IsNullOrEmpty(provider.GetRequiredService<MySqlRepositoryOptions>()?.ConnectionString))
                throw new GalaxyException("You should set ConnectionString");
        }

        /// <summary>
        /// Adds the services.
        /// </summary>
        /// <param name="services">Services.</param>
        protected override void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IConnectionFactory, MySqlConnectionFactory>(MySqlConnectionFactoryActivetor);
            services.AddSingleton(typeof(IEventStorage<>), typeof(EventStorage<>));
        }

        protected override void UnregisterServices(IServiceProvider provider)
        {
            ;
        }

        readonly Func<IServiceProvider, MySqlConnectionFactory> MySqlConnectionFactoryActivetor = (provider) =>
        {
            var option = provider.GetRequiredService<MySqlRepositoryOptions>();
            var loggerFactory = provider.GetService<ILoggerFactory>();

            return new MySqlConnectionFactory(option.ConnectionString, loggerFactory);
        };
    }
}
