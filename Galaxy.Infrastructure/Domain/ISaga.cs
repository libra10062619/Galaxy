using System;
using Galaxy.Infrastructure.Events;
using System.Threading.Tasks;
namespace Galaxy.Infrastructure.Domain
{
    //public interface ISaga<TKey, TEvent> : IAggregateRoot<TKey>
    //    where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>
    //    where TEvent : IDomainEvent
    //{
    //    void Transit(TEvent @event);

    //    Task TransitAsync(TEvent @event);
    //}

    public interface ISaga<TEvent> : IAggregateRoot
        where TEvent : IDomainEvent
    {
        void Transit(TEvent @event);

        Task TransitAsync(TEvent @event);
    }
}
