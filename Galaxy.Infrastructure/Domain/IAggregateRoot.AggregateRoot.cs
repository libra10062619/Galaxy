using System;
using System.Collections.Generic;
using Galaxy.Infrastructure.Events;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxy.Infrastructure.Domain
{
    /// <summary>
    /// Aggregate root.
    /// </summary>
    //[Serializable]
    //public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>, IPurgeable
    //    where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>
    //{
    //    /// <summary>
    //    /// The domain events.
    //    /// </summary>
    //    protected Queue<DomainEvent> domainEvents;

    //    /// <summary>
    //    /// Gets the events.
    //    /// </summary>
    //    /// <value>The events.</value>
    //    public IEnumerable<DomainEvent> DomainEvents => domainEvents.AsEnumerable();

    //    /// <summary>
    //    /// Gets or sets the version. Default to -1
    //    /// </summary>
    //    /// <value>The version.</value>
    //    public int Version { get; set; } = -1;

    //    protected AggregateRoot()
    //    {
    //        domainEvents = new Queue<DomainEvent>();
    //    }

    //    /// <summary>
    //    /// 事件重放
    //    /// </summary>
    //    /// <param name="historyEvents">Domain events.</param>
    //    public virtual void Replay(IEnumerable<DomainEvent> historyEvents)
    //    {
    //        foreach (var @event in historyEvents)
    //            ApplyChange(@event, false);
    //    }

    //    /// <summary>
    //    /// 执行事件委托
    //    /// </summary>
    //    /// <param name="event">Event.</param>
    //    /// <param name="live"><c>true</c>表示实时操作，<c>false</c>表示重放操作</param>
    //    /// <typeparam name="TEvent">The 1st type parameter.</typeparam>
    //    protected virtual void ApplyChange<TEvent>(TEvent @event, bool live = true) 
    //        where TEvent : DomainEvent
    //    {
    //        // 执行事件内联业务
    //        var eventHanlders = EventHandlerHelper.GetInlineEventHandlerMethods(this.GetType(), @event);
    //        Parallel.ForEach(eventHanlders, (h) =>
    //        {
    //            h.Invoke(this, new object[] { @event });
    //        });

    //        if (live)
    //        {
    //            domainEvents.Enqueue(@event);   // 事件入队
    //        }
    //    }

    //    void IPurgeable.Purge()
    //    {
    //        if (domainEvents.Any())
    //            domainEvents.Clear();
    //    }
    //}

    [Serializable]
    public abstract class AggregateRoot : Entity<long>, IAggregateRoot, IPurgeable
    {
        /// <summary>
        /// The domain events.
        /// </summary>
        protected Queue<DomainEvent> domainEvents;

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <value>The events.</value>
        public IEnumerable<DomainEvent> DomainEvents => domainEvents.AsEnumerable();

        /// <summary>
        /// Gets or sets the version. Default to -1
        /// </summary>
        /// <value>The version.</value>
        public int Version { get; set; } = -1;

        public string TransactionId { get; set; }

        protected AggregateRoot()
        {
            domainEvents = new Queue<DomainEvent>();
        }

        /// <summary>
        /// 事件重放
        /// </summary>
        /// <param name="historyEvents">Domain events.</param>
        public virtual void Replay(IEnumerable<DomainEvent> historyEvents)
        {
            foreach (var @event in historyEvents)
                ApplyChange(@event, false);
        }

        /// <summary>
        /// 执行事件委托
        /// </summary>
        /// <param name="event">Event.</param>
        /// <param name="live"><c>true</c>表示实时操作，<c>false</c>表示重放操作</param>
        /// <typeparam name="TEvent">The 1st type parameter.</typeparam>
        protected virtual void ApplyChange<TEvent>(TEvent @event, bool live = true)
            where TEvent : DomainEvent
        {
            // 执行事件内联业务
            var eventHanlders = EventHandlerHelper.GetInlineEventHandlerMethods(this.GetType(), @event);
            Parallel.ForEach(eventHanlders, (h) =>
            {
                h.Invoke(this, new object[] { @event });
            });

            if (live)
            {
                domainEvents.Enqueue(@event);   // 事件入队
            }
        }

        void IPurgeable.Purge()
        {
            if (domainEvents.Any())
                domainEvents.Clear();
        }
    }
}
