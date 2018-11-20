using System;
using System.Data;
namespace Galaxy.Infrastructure.EventStorage.MySql
{
    /// <summary>
    /// Connection factory.
    /// </summary>
    public interface IConnectionFactory
    {
        IDbConnection GetOpenedConnection();
    }
}
