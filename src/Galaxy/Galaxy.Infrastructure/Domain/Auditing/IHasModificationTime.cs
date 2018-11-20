using System;
namespace Galaxy.Infrastructure.Domain.Auditing
{
    public interface IHasModificationTime
    {
        DateTime? ModificationTime { get; }
    }
}
