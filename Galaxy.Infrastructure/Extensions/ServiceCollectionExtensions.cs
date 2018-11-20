using System;
using Galaxy.Infrastructure;
using Galaxy.Infrastructure.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{

    public static class ServiceCollectionExtensions
    {
        public static GalaxyBuilder AddGalaxy(this IServiceCollection services, Action<GalaxyOptions> setupAction)
        {
            var builder = new GalaxyBuilder(services, setupAction);

            return builder;
        }
    }
}
