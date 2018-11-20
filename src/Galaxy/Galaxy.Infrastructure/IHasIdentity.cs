using System;
namespace Galaxy.Infrastructure
{
    public interface IHasIdentity<TKey> 
        where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>
    {
        TKey Id { get; }
    }
}
