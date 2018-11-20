using System;
namespace Galaxy.Infrastructure.Domain
{
    public interface IEntity<TKey> : IHasIdentity<TKey>
        where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>
    {
    }
}
