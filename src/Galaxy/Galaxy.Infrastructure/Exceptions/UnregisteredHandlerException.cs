using System;

namespace Galaxy.Infrastructure.Exceptions
{
    public class UnregisteredHandlerException : GalaxyException
    {
        public UnregisteredHandlerException() { }

        public UnregisteredHandlerException(string message) : base(message) { }

        public UnregisteredHandlerException(string message, Exception innerException) : base(message, innerException) { }
    }
}
