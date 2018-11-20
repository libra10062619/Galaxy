using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Galaxy.Infrastructure.Services;
using System.Linq;

namespace Galaxy.Infrastructure.Extensions
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseGalaxy(this IApplicationBuilder app)
        {
            if (null == app)
                throw new ArgumentNullException(nameof(app));

            var provider = app.ApplicationServices;

            var galaxy = provider.GetRequiredService<GalaxyOptions>();

            galaxy.UseGalaxy(provider);

            return app;
        }
    }
}
