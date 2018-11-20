using System;
using System.Collections.Concurrent;
namespace Galaxy.Infrastructure
{
    public interface IIdGenerator{}

    public interface IIdGenerator<TIdentity> : IIdGenerator
    {
        TIdentity NextIdentity();
    }
}
