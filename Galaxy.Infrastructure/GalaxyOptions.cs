using System;
using System.Collections.Generic;
using Galaxy.Infrastructure.Extensions;
using System.Linq;
using Galaxy.Infrastructure.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace Galaxy.Infrastructure
{
    public class GalaxyOptions
    {
        /// <summary>
        /// Gets or sets the identifier generator epoch.
        /// </summary>
        /// <value>The identifier generator epoch.</value>
        public DateTime IdGeneratorEpoch { get; set; } = new DateTime(2015, 4, 1, 0, 0, 0, DateTimeKind.Utc);
        /// <summary>
        /// Gets or sets the generator identifier.
        /// </summary>
        /// <value>The generator identifier.</value>
        public int GeneratorId { get; set; } = 0;
        /// <summary>
        /// Gets the extensions of service registration.
        /// </summary>
        /// <value>The extensions.</value>
        internal IList<IGalaxyRegistration> ExtensionRegistrations { get; } = new List<IGalaxyRegistration>();

        /// <summary>
        /// Registers an extension that will be executed when building services.
        /// </summary>
        /// <param name="extension"></param>
        public void RegisterServiceExtensions(IGalaxyRegistration extension)
        {
            if (extension == null)
                throw new ArgumentNullException(nameof(extension));

            ExtensionRegistrations.Add(extension);
        }

        public void UseGalaxy(IServiceProvider provider)
        {
            foreach(var idGenerator in provider.GetServices<IIdGenerator>())
            {
                IdentityHelper.RegisterIdGenerator(idGenerator);
            }

            foreach (var extensionService in ExtensionRegistrations)
            {
                extensionService.CheckRequirement(provider);
            }

            provider.GetService<IBootstrapper>()?.StartAsync();
        }
    }
}
