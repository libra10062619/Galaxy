using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace Galaxy.Infrastructure.EventStorage.MySql
{
    /// <summary>
    /// My sql connection factory.
    /// </summary>
    sealed class MySqlConnectionFactory : IConnectionFactory
    {
        readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galaxy.Infrastructure.MySql.Storage.MySqlConnectionFactory"/> class.
        /// </summary>
        public MySqlConnectionFactory(ILoggerFactory loggerFactory) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galaxy.Infrastructure.MySql.Storage.MySqlConnectionFactory"/> class.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        public MySqlConnectionFactory(string connectionString, ILoggerFactory loggerFactory)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Gets the opened connection.
        /// </summary>
        /// <returns>The opened connection.</returns>
        public IDbConnection GetOpenedConnection()
        {
            var connection = new  MySqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
