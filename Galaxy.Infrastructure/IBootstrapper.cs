using System;
using System.Threading.Tasks;
namespace Galaxy.Infrastructure
{
    public interface IBootstrapper
    {
        Task StartAsync();
    }
}
