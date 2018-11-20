using System;
using System.Threading.Tasks;

namespace Galaxy.Infrastructure.Messaging
{
	public interface IMessagePublisher : IDisposable
	{
        Task<Result> PublishAsync(string topic, string message);
    }
}