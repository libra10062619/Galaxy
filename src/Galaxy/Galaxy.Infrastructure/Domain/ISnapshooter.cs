using System;
namespace Galaxy.Infrastructure.Domain
{
    /// <summary>
    /// Snapper.
    /// </summary>
    public interface ISnapshooter
    {
        Snapshot Snapshoot();

        void RestorFromSnapshot(Snapshot snapshot);
    }
}
