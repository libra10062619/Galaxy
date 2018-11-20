using System;
using Microsoft.Extensions.DependencyInjection;

namespace Galaxy.Infrastructure.Extensions
{
    public abstract class GalaxyRegistration<T> : IGalaxyRegistration where T : class, new()
    {
        readonly Action<T> _setupAction;

        protected GalaxyRegistration(Action<T> setupAction)
        {
            _setupAction = setupAction;
        }

        public void Register(IServiceCollection services)
        {
            var options = new T();
            _setupAction?.Invoke(options);
            services.AddSingleton(options);

            RegisterServices(services);
        }

        public void CheckRequirement(IServiceProvider provider)
        {
            CheckRequirementServices(provider);
        }

        public void Unregister(IServiceProvider provider)
        {
            UnregisterServices(provider);
        }

        protected abstract void RegisterServices(IServiceCollection services);
        protected abstract void CheckRequirementServices(IServiceProvider provider);
        protected abstract void UnregisterServices(IServiceProvider provider);
    }
}
