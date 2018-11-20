using System;
namespace Galaxy.Infrastructure.Commands
{
    /// <summary>
    /// Command interface.
    /// </summary>
    public interface ICommand: IHasVersion
    {
        string Id { get; }
    }
}
