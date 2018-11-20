using System;
using System.Collections.Generic;
using Galaxy.Infrastructure.Events;
namespace Galaxy.Infrastructure.Domain
{
    /// <summary>
    /// Aggregate root.
    /// </summary>
    //public interface IAggregateRoot<TKey> : IEntity<TKey>, IHasVersion 
    //    where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>
    //{
    //    /// <summary>
    //    /// Gets the domain events.
    //    /// </summary>
    //    /// <value>The events.</value>
    //    IEnumerable<DomainEvent> DomainEvents { get; }
    //    /// <summary>
    //    /// Replay the specified domainEvents to build aggregate root.
    //    /// </summary>
    //    /// <param name="domainEvents">Domain events.</param>
    //    void Replay(IEnumerable<DomainEvent> domainEvents);
    //}

    public interface IAggregateRoot: IEntity<long>, IHasVersion
    {
        /// <summary>
        /// Gets the domain events.
        /// </summary>
        /// <value>The events.</value>
        IEnumerable<DomainEvent> DomainEvents { get; }
        /// <summary>
        /// Replay the specified domainEvents to build aggregate root.
        /// </summary>
        /// <param name="domainEvents">Domain events.</param>
        void Replay(IEnumerable<DomainEvent> domainEvents);
    }
}
