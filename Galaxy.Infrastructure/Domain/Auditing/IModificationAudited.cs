using System;
namespace Galaxy.Infrastructure.Domain.Auditing
{
    public interface IModificationAudited : IModificationAudited<int?>
    {
    }

    public interface IModificationAudited<TForeign>
    {
        TForeign ModificatorId { get; set; }
    }
}
