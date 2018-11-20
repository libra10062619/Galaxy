using System;
using System.Collections.Generic;
using Galaxy.Infrastructure.Commands;
using Galaxy.Infrastructure.Events;
using Microsoft.Extensions.DependencyInjection;
using Galaxy.Infrastructure.Extensions;

namespace Galaxy.Infrastructure.Services
{
    public abstract class MicroserviceRegistration<T> : GalaxyRegistration<T> where T :class, new()
    {
        protected virtual IEnumerable<Func<IServiceProvider, ICommandHandler>> CommandHandlersInitializer => null;

        protected virtual IEnumerable<Func<IServiceProvider, IDomainEventHandler>> EventHandlersInitializer => null;

        protected virtual IEnumerable<Func<IServiceProvider, IMicroservice>> MicroserviceInitializer => null;

        protected MicroserviceRegistration(Action<T> setupAction) : base (setupAction)
        {
        }

        protected override void UnregisterServices(IServiceProvider provider)
        {
        }

        protected override void CheckRequirementServices(IServiceProvider provider)
        {
            //var handlers = provider.GetServices<ICommandHandler>();// provider.GetRequiredService<ICommandHandler>();
        }

        protected override void RegisterServices(IServiceCollection services)
        {
            //RegisterEventHandlers(services);
            //RegisterCommandHandlers(services);

            RegisterMicroservices(services);
            RegisterMicroservice(services);
        }

        protected abstract void RegisterMicroservice(IServiceCollection services);

        /// <summary>
        /// Registers the command handlers.
        /// </summary>
        /// <param name="services">Services.</param>
        void RegisterCommandHandlers(IServiceCollection services)
        {
            if (CommandHandlersInitializer != null)
            {
                foreach (var init in CommandHandlersInitializer)
                {
                    services.AddScoped(typeof(ICommandHandler), provider => init(provider));
                    //services.AddScoped((IServiceProvider provider) => init(provider));
                }
            }
        }

        /// <summary>
        /// Registers the event handlers.
        /// </summary>
        /// <param name="services">Services.</param>
        void RegisterEventHandlers(IServiceCollection services)
        {
            if (EventHandlersInitializer != null)
            {
                foreach (var init in EventHandlersInitializer)
                {
                    services.AddScoped((IServiceProvider provider) => init(provider));
                }
            }
        }

        /// <summary>
        /// Registers the microservices.
        /// </summary>
        /// <param name="services">Services.</param>
        void RegisterMicroservices(IServiceCollection services)
        {
            if(MicroserviceInitializer != null)
            {
                foreach(var init in MicroserviceInitializer)
                {
                    services.AddSingleton((IServiceProvider provider) => init(provider));
                }
            }
        }


    }
}
