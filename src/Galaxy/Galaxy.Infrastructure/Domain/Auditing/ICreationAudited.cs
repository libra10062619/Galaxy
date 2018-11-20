using System;
namespace Galaxy.Infrastructure.Domain.Auditing
{
    public interface ICreationAudited<TForeign> : IHasCreationTime
    {
        TForeign CreatorId { get; }
    }
}
