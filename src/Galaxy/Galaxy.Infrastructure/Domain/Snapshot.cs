using System;
namespace Galaxy.Infrastructure.Domain
{
    //public abstract class Snapshot<TKey> : IHasIdentity<TKey>, IHasVersion
    //    where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>
    //{
    //    public TKey Id { get; set; }

    //    public int Version { get; set; }
    //}

    public abstract class Snapshot : IHasIdentity<long>, IHasVersion
    {
        public long Id { get; set; }

        public int Version { get; set; }
    }
}
