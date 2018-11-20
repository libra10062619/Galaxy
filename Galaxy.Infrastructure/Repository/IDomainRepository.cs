using System;
using System.Threading.Tasks;
using Galaxy.Infrastructure.Domain;
namespace Galaxy.Infrastructure.Repository
{
    //public interface IRepository
    //{
    //    Task SaveAsync<T, TKey>(T aggregateRoot, bool purge = true) 
    //        where T: AggregateRoot<TKey>
    //        where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>;

    //    Task<T> GetAsync<T, TKey>(TKey key)
    //        where T : AggregateRoot<TKey>, new()
    //        where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>;
    //}

    //public interface IDomainRepository<T, TKey>
    //    where T : AggregateRoot<TKey>
    //    where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>
    //{
    //    Task SaveAsync(T entity);

    //    Task<T> GetAsync(TKey key);
    //}

    public interface IDomainRepository<T>
        where T : AggregateRoot
    {
        Task SaveAsync(T entity);

        Task<T> GetAsync(long key);
    }
}
