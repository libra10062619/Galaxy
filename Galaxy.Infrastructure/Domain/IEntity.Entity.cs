using System;
using System.Reflection;
using Galaxy.Infrastructure.Helper;

namespace Galaxy.Infrastructure.Domain
{
    /// <summary>
    /// 实体
    /// </summary>
    [Serializable]
    public abstract class Entity<TKey> : IEntity<TKey> 
        where TKey : IComparable, IConvertible, IComparable<TKey>, IEquatable<TKey>
    {
        /// <summary>
        /// 获取或设置Id
        /// </summary>
        /// <value>The identifier.</value>
        public virtual TKey Id { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galaxy.Infrastructure.Domain.Entity`1"/> class.
        /// </summary>
        protected Entity() => this.Id = IdentityHelper.NextIdentity<TKey>();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Galaxy.Infrastructure.Domain.Entity`1"/> class.
        /// </summary>
        /// <param name="id">Identifier.</param>
        protected Entity(TKey id) => this.Id = id;

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<TKey>))
            {
                return false;
            }

            //Same instances must be considered as equal
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (Entity<TKey>)obj;

            //Must have a IS-A relation of types or must be same type
            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.GetTypeInfo().IsAssignableFrom(typeOfOther) && !typeOfOther.GetTypeInfo().IsAssignableFrom(typeOfThis))
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
        {
            return Equals(left, null) ? Equals(right, null) : left.Equals(right);
        }

        public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
        {
            return !(left == right);
        }
    }
}
