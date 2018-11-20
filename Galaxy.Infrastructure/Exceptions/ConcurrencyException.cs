using System;
namespace Galaxy.Infrastructure.Exceptions
{
    public class ConcurrencyException : GalaxyException
    {
        public ConcurrencyException() { }

        public ConcurrencyException(string message) : base(message) { }

        public ConcurrencyException(string message, Exception innerException) : base(message, innerException) { }
    }
}
