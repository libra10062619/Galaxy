using System;
using System.Threading.Tasks;
using Galaxy.Infrastructure.Domain;

namespace Galaxy.Infrastructure.Repository
{
    /// <summary>
    /// Event storage.
    /// </summary>
    //public interface IEventStorage
    //{
    //    /// <summary>
    //    /// Saves the async.
    //    /// </summary>
    //    /// <returns>The async.</returns>
    //    /// <param name="aggregateRoot">Aggregate root.</param>
    //    /// <typeparam name="T">The 1st type parameter.</typeparam>
    //    /// <typeparam name="TKey">The 2nd type parameter.</typeparam>
    //    Task SaveAsync<T, TKey>(T aggregateRoot)
    //        where T : AggregateRoot<TKey>, new()
    //        where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>;

    //    /// <summary>
    //    /// Gets the async.
    //    /// </summary>
    //    /// <returns>The async.</returns>
    //    /// <param name="aggregateRootId">Aggregate root identifier.</param>
    //    /// <typeparam name="T">The 1st type parameter.</typeparam>
    //    /// <typeparam name="TKey">The 2nd type parameter.</typeparam>
    //    Task<T> GetAsync<T, TKey>(TKey aggregateRootId)
    //        where T : AggregateRoot<TKey>, new()
    //        where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>;
    //}
    public interface IEventStorage<T> where T : AggregateRoot, new()
    {
        /// <summary>
        /// Saves the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="aggregateRoot">Aggregate root.</param>
        Task SaveAsync(T aggregateRoot);

        /// <summary>
        /// Gets the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="aggregateRootId">Aggregate root identifier.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        /// <typeparam name="TKey">The 2nd type parameter.</typeparam>
        Task<T> GetAsync(long aggregateRootId);
    }
}
