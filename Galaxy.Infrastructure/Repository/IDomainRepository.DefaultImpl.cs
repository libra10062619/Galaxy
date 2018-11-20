using System;
using System.Threading.Tasks;
using Galaxy.Infrastructure.Domain;
using Galaxy.Infrastructure.Messaging;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Galaxy.Infrastructure.Repository
{
    /// <summary>
    /// Domain repository.
    /// </summary>
    //internal class DomainRepository<T, TKey> : IDomainRepository<T, TKey>
    //    where T : AggregateRoot<TKey>, new()
    //    where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>
    //{

    //    /// <summary>
    //    /// The event storage.
    //    /// </summary>
    //    protected readonly IEventStorage _eventStorage;

    //    /// <summary>
    //    /// The message publisher.
    //    /// </summary>
    //    protected readonly IEventPublisher _eventPublisher;

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="T:Galaxy.Infrastructure.DomainRepository`2"/> class.
    //    /// </summary>
    //    /// <param name="eventStorage">Event storage.</param>
    //    /// <param name="eventPublisher">Event publisher.</param>
    //    public DomainRepository(IEventStorage eventStorage,
    //                            IEventPublisher eventPublisher)
    //    {
    //        _eventStorage = eventStorage;
    //        _eventPublisher = eventPublisher;
    //    }

    //    /// <summary>
    //    /// Gets <typeparamref name="T"/> the async.
    //    /// </summary>
    //    /// <returns>The async.</returns>
    //    /// <param name="key">Key.</param>
    //    public async Task<T> GetAsync(TKey key)
    //    {
    //        return await _eventStorage.GetAsync<T, TKey>(key);
    //    }

    //    /// <summary>
    //    /// Saves <paramref name="aggregateRoot"/> async.
    //    /// </summary>
    //    /// <returns>The async.</returns>
    //    /// <param name="aggregateRoot">Aggregate root.</param>
    //    public async Task SaveAsync(T aggregateRoot)
    //    {


    //        await _eventStorage.SaveAsync<T,TKey>(aggregateRoot);
    //    }


    //}

    internal class DomainRepository<T> : IDomainRepository<T>
        where T : AggregateRoot, new()
    {

        /// <summary>
        /// The event storage.
        /// </summary>
        protected readonly IEventStorage<T> _eventStorage;

        /// <summary>
        /// The message publisher.
        /// </summary>
        protected readonly IEventPublisher _eventPublisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galaxy.Infrastructure.DomainRepository`2"/> class.
        /// </summary>
        /// <param name="eventStorage">Event storage.</param>
        /// <param name="eventPublisher">Event publisher.</param>
        public DomainRepository(IEventStorage<T> eventStorage,
                                IEventPublisher eventPublisher,
                                ILoggerFactory loggerFactory)
        {
            _eventStorage = eventStorage;
            _eventPublisher = eventPublisher;
        }

        /// <summary>
        /// Gets <typeparamref name="T"/> the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="key">Key.</param>
        public async Task<T> GetAsync(long key)
        {
            return await _eventStorage.GetAsync(key);
        }

        /// <summary>
        /// Saves <paramref name="aggregateRoot"/> async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="aggregateRoot">Aggregate root.</param>
        public async Task SaveAsync(T aggregateRoot)
        {
            await _eventStorage.SaveAsync(aggregateRoot);

            foreach (var e in aggregateRoot.DomainEvents)
            {
                await _eventPublisher.PublishAsync(e);
            }

            ((IPurgeable)aggregateRoot).Purge();
        }
    }
}
