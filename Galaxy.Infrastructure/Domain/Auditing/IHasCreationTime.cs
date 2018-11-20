using System;
namespace Galaxy.Infrastructure.Domain.Auditing
{
    public interface IHasCreationTime
    {
        DateTime? CreationTime { get; }
    }
}
