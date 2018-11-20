using System;
using Microsoft.Extensions.DependencyInjection;
namespace Galaxy.Infrastructure.Extensions
{
    public interface IGalaxyRegistration
    {
        void Register(IServiceCollection services);

        void CheckRequirement(IServiceProvider provider);

        void Unregister(IServiceProvider provider);
    }
}
