using System;
namespace Galaxy.Infrastructure
{
    /// <summary>
    /// Versioning.
    /// </summary>
    public interface IHasVersion
    {
        int Version { get; }
    }
}
