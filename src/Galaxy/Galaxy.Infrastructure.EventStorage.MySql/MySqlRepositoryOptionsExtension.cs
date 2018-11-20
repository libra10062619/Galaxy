using System;
using Galaxy.Infrastructure;
using Galaxy.Infrastructure.EventStorage.MySql;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// My sql repository options extension.
    /// </summary>
    public static class MySqlRepositoryOptionsExtension
    {
        public static GalaxyOptions AddMySqlDomainReposity(this GalaxyOptions galaxy, Action<MySqlRepositoryOptions> setupAction)
        {
            if (null == setupAction) throw new ArgumentNullException(nameof(setupAction));

            galaxy.RegisterServiceExtensions(new MySqlRepositoryRegistration(setupAction));

            return galaxy;
        }
    }
}
