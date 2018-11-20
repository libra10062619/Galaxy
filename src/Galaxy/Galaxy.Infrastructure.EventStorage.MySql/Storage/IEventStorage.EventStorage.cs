using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Galaxy.Infrastructure.Events;
using Galaxy.Infrastructure.Exceptions;
using Galaxy.Infrastructure.Domain;
using Galaxy.Infrastructure.Repository;
using Newtonsoft.Json;

namespace Galaxy.Infrastructure.EventStorage.MySql
{
    /// <summary>
    /// Event storage.
    /// </summary>
    //    sealed class EventStorage : IEventStorage
    //    {

    //#pragma warning disable RECS0108 // 就通用类型中的静态字段发出警告
    //        static readonly object storageLock = new object();
    //#pragma warning restore RECS0108 // 就通用类型中的静态字段发出警告

    //    readonly IConnectionFactory _connectionFactory;

    //    public IDbConnection Connection => _connectionFactory.GetOpenedConnection();

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="T:Galaxy.Infrastructure.MySql.Storage.EventStorage"/> class.
    //    /// </summary>
    //    /// <param name="connectionFactory">Connection factory.</param>
    //    public EventStorage(IConnectionFactory connectionFactory)
    //    {
    //        _connectionFactory = connectionFactory;
    //    }

    //    /// <summary>
    //    /// Saves aggregate root.
    //    /// </summary>
    //    /// <returns>The async.</returns>
    //    /// <param name="aggregateRoot">Aggregate root.</param>
    //    /// <typeparam name="T">The 1st type parameter.</typeparam>
    //    /// <typeparam name="TKey">The 2nd type parameter.</typeparam>
    //    public async Task SaveAsync<T, TKey>(T aggregateRoot)
    //        where T : AggregateRoot<TKey>, new()
    //        where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>
    //    {
    //        var uncommitted = aggregateRoot.DomainEvents.ToList();

    //        if (uncommitted.Any())
    //        {
    //            lock (storageLock)
    //            {
    //                var version = aggregateRoot.Version;

    //                if (!IsConcurrentVersion<T, TKey>(aggregateRoot.Id, aggregateRoot.Version).Result)
    //                    throw new ConcurrencyException($"Aggregate {aggregateRoot.Id} has been previoursly modified.");
    //                SaveEventsAsync<DomainEvent, TKey>(uncommitted).RunSynchronously();
    //            }

    //            var snapshot = ((ISnapshotter)aggregateRoot).Snapshoot();

    //            await SaveSnapshotAsync(snapshot, aggregateRoot.GetType().Name);
    //        }

    //        ((IPurgeable)aggregateRoot).Purge();
    //    }

    //    /// <summary>
    //    /// Gets the async.
    //    /// </summary>
    //    /// <returns>The async.</returns>
    //    /// <param name="aggregateRootId">Aggregate root identifier.</param>
    //    /// <typeparam name="T">The 1st type parameter.</typeparam>
    //    /// <typeparam name="TKey">The 2nd type parameter.</typeparam>
    //    public async Task<T> GetAsync<T, TKey>(TKey aggregateRootId)
    //        where T : AggregateRoot<TKey>, new()
    //        where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>
    //    {
    //        var aggregateRoot = new T();
    //        // 从快照恢复聚合根
    //        var snapshot = await GetSnapshotAsync(aggregateRootId, typeof(T).Name);
    //        if (null != snapshot)
    //        {
    //            ((ISnapshotter)aggregateRoot).RestorFromSnapshot(snapshot);
    //        }

    //        // 获取快照后的事件源，重放事件
    //        var events = await GetEventsAsync<DomainEvent, TKey>(aggregateRoot.Id, aggregateRoot.Version);
    //        if (events.Any())
    //        {
    //            aggregateRoot.Replay(events);
    //        }

    //        return aggregateRoot;
    //    }

    //    /// <summary>
    //    /// Gets domain events.
    //    /// </summary>
    //    /// <returns>The events async.</returns>
    //    /// <param name="aggregateRootId">Aggregate root identifier.</param>
    //    /// <param name="version">Version.</param>
    //    /// <typeparam name="TDomainEvent">The 1st type parameter.</typeparam>
    //    /// <typeparam name="TKey">The 2nd type parameter.</typeparam>
    //    async Task<IEnumerable<TDomainEvent>> GetEventsAsync<TDomainEvent, TKey>(TKey aggregateRootId, int version = 0)
    //        where TDomainEvent : DomainEvent
    //        where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>
    //    {
    //        var result = await Connection.QueryAsync<EventEntity>("SELECT * FROM EVENT_ENTITY Where AggregateRootId=@aggregateRootId and Version>=@version",
    //                                                              new { aggregateRootId, version });
    //        return result.Select(p => EventEntity.ToDomainEvent<TDomainEvent>(p));
    //    }

    //    /// <summary>
    //    /// Saves the snapshot async.
    //    /// </summary>
    //    /// <returns>The snapshot async.</returns>
    //    /// <param name="snapshot">Snapshot.</param>
    //    /// <param name="tableName">Table name.</param>
    //    /// <typeparam name="TKey">The 1st type parameter.</typeparam>
    //    async Task SaveSnapshotAsync<TKey>(Snapshot<TKey> snapshot, string tableName)
    //        where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>
    //    {
    //        await Connection.ExecuteAsync($"INSERT INTO SNAP_{tableName} VALUES(@Id, @Version, @Content)", snapshot);
    //    }

    //    /// <summary>
    //    /// Gets the snapshot async.
    //    /// </summary>
    //    /// <returns>The snapshot async.</returns>
    //    /// <param name="key">Key.</param>
    //    /// <param name="tableName">Table name.</param>
    //    /// <typeparam name="TKey">The 1st type parameter.</typeparam>
    //    async Task<dynamic> GetSnapshotAsync<TKey>(TKey key, string tableName)
    //    {
    //        return await Connection.QueryFirstAsync($"SELECT * FROM {tableName} WHERE Id=@Id", new{Id = key});
    //    }

    //    /// <summary>
    //    /// Saves the events async.
    //    /// </summary>
    //    /// <returns>The events async.</returns>
    //    /// <param name="domainEvents">Domain events.</param>
    //    /// <typeparam name="TKey">The 1st type parameter.</typeparam>
    //    async Task SaveEventsAsync<TDomainEvent, TKey>(IEnumerable<TDomainEvent> domainEvents)
    //        where TDomainEvent : DomainEvent
    //        where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>
    //    {
    //        var events = domainEvents.Select(EventEntity.ToEventEntity);
    //        var tran = Connection.BeginTransaction();
    //        try
    //        {
    //            await Connection.ExecuteAsync("INSERT INTO EVENT_ENTITY VALUES(@Id, @AggregateRootId, @EventContent, @Version)", events);
    //            tran.Commit();
    //        }
    //        catch (Exception ex)
    //        {
    //            tran.Rollback();
    //            throw new GalaxyException("", ex);
    //        }
    //        finally
    //        {
    //            tran.Rollback();
    //        }
    //    }

    //    /// <summary>
    //    /// Is the concurrent version.
    //    /// </summary>
    //    /// <returns>The concurrent version.</returns>
    //    /// <param name="key">Key.</param>
    //    /// <param name="version">Version.</param>
    //    async Task<bool> IsConcurrentVersion<T, TKey>(TKey key, int version)
    //        where T : AggregateRoot<TKey>, new()
    //        where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>
    //    {
    //        if (version < 0)
    //            return true;

    //        T entity = await GetAsync<T, TKey>(key);

    //        return entity != null && version == entity.Version;
    //    }
    //}
    sealed class EventStorage<T> : IEventStorage<T> where T : AggregateRoot, new()
    {

#pragma warning disable RECS0108 // 就通用类型中的静态字段发出警告
        static readonly object storageLock = new object();
#pragma warning restore RECS0108 // 就通用类型中的静态字段发出警告

        readonly IConnectionFactory _connectionFactory;
        readonly MySqlRepositoryOptions _options;

        public IDbConnection Connection { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galaxy.Infrastructure.MySql.Storage.EventStorage"/> class.
        /// </summary>
        /// <param name="connectionFactory">Connection factory.</param>
        public EventStorage(IConnectionFactory connectionFactory, MySqlRepositoryOptions options)
        {
            _connectionFactory = connectionFactory;
            _options = options;
            Connection = _connectionFactory.GetOpenedConnection();
        }

        /// <summary>
        /// Saves aggregate root.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="aggregateRoot">Aggregate root.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        /// <typeparam name="TKey">The 2nd type parameter.</typeparam>
        public async Task SaveAsync(T aggregateRoot)
        {
            var uncommitted = aggregateRoot.DomainEvents.ToList();

            if (aggregateRoot.DomainEvents.Any())
            {
                lock (storageLock)
                {
                    if (aggregateRoot.Version >= 0 && !IsConcurrentVersion(aggregateRoot.Id, aggregateRoot.Version).Result)
                        throw new ConcurrencyException($"Aggregate {aggregateRoot.Id} has been previoursly modified.");
                    if (aggregateRoot.Version <= 0) aggregateRoot.Version = 0;

                    SaveAggregate(aggregateRoot);
                }
            }
            await Task.CompletedTask;
        }

        /// <summary>
        /// Gets the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="aggregateRootId">Aggregate root identifier.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        /// <typeparam name="TKey">The 2nd type parameter.</typeparam>
        public async Task<T> GetAsync(long aggregateRootId)
        {
            var aggregateRoot = new T();
            if (_options.AllowSnapshot)
            {
                // 从快照恢复聚合根
                await GetSnapshotAsync(aggregateRoot, aggregateRootId);
            }
            // 获取快照后的事件源，重放事件
            var events = await GetEventsAsync<DomainEvent>(aggregateRootId, aggregateRoot.Version);
            if (events.Any())
            {
                aggregateRoot.Replay(events);
            }

            return aggregateRoot;
        }

        /// <summary>
        /// Gets domain events.
        /// </summary>
        /// <returns>The events async.</returns>
        /// <param name="aggregateRootId">Aggregate root identifier.</param>
        /// <param name="version">Version.</param>
        /// <typeparam name="TDomainEvent">The 1st type parameter.</typeparam>
        /// <typeparam name="TKey">The 2nd type parameter.</typeparam>
        async Task<IEnumerable<TDomainEvent>> GetEventsAsync<TDomainEvent>(long aggregateRootId, int version = 0)
            where TDomainEvent : DomainEvent
        {
            var result = await Connection.QueryAsync<EventEntity>("SELECT * FROM EVENT_ENTITY Where AggregateRootId=@aggregateRootId and Version>=@version",
                                                                    new { aggregateRootId, version });
            return result.Select(p => EventEntity.ToDomainEvent<TDomainEvent>(p));
        }

        /// <summary>
        /// Saves the snapshot async.
        /// </summary>
        /// <returns>The snapshot async.</returns>
        /// <param name="aggregateRoot">Aggregate root.</param>
        void SaveSnapshot(AggregateRoot aggregateRoot)
        {
            Snapshot snapshot = ((ISnapshooter)aggregateRoot).Snapshoot();

            var entity = new { snapshot.Id, snapshot.Version, Content = JsonConvert.SerializeObject(snapshot) };

            Connection.Execute($"INSERT INTO SNAP_{aggregateRoot.GetType().Name}(Id, Version, Content) VALUES(@Id, @Version, @Content) ON DUPLICATE KEY UPDATE Version=@Version, Content=@Content;", entity);
        }


        async Task GetSnapshotAsync(AggregateRoot aggregateRoot, long aggregateRootId)// long key, string tableName)
        {
            object entity = await Connection.QueryFirstOrDefaultAsync($"SELECT * FROM SNAP_{aggregateRoot.GetType().Name} WHERE Id=@Id;", new { Id = aggregateRootId });
            if (null != entity)
            {
                Snapshot snapshot = JsonConvert.DeserializeObject<Snapshot>(entity.Content);
                if (null != snapshot) ((ISnapshooter)aggregateRoot).RestorFromSnapshot(snapshot);
            }
        }

        void SaveAggregate(AggregateRoot aggregateRoot)
        {
            var events = aggregateRoot.DomainEvents.Select(EventEntity.ToEventEntity);
            var tran = Connection.BeginTransaction();
            try
            {
                foreach(var evt in events)
                {
                    Connection.Execute("INSERT INTO EVENT_ENTITY(Id, AggregateRootId, EventContent, Version) VALUES(@Id, @AggregateRootId, @EventContent, @Version);", events);
                    evt.Version++;
                }

                if (_options.AllowSnapshot)
                    SaveSnapshot(aggregateRoot);
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new GalaxyException("", ex);
            }
        }

        /// <summary>
        /// Is the concurrent version.
        /// </summary>
        /// <returns>The concurrent version.</returns>
        /// <param name="key">Key.</param>
        /// <param name="version">Version.</param>
        async Task<bool> IsConcurrentVersion(long key, int version)
        {
            T entity = await GetAsync(key);

            return entity != null && version == entity.Version;
        }
    }
}
