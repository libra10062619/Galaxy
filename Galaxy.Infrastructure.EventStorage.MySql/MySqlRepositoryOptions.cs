using System;
namespace Galaxy.Infrastructure.EventStorage.MySql
{
    /// <summary>
    /// My sql repository options.
    /// </summary>
    public class MySqlRepositoryOptions
    {
        public string ConnectionString { get; set; }

        public bool AllowSnapshot { get; set; } = true;
    }
}
