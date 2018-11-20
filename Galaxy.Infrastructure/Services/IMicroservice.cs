using System;
using Galaxy.Infrastructure.Messaging;

namespace Galaxy.Infrastructure.Services
{
    public interface IMicroservice : IDisposable
    {
        void Start();
    }
}
