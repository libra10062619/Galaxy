using System;
namespace Galaxy.Infrastructure.Exceptions
{
    /// <summary>
    /// Base exception.
    /// </summary>
    public class GalaxyException : Exception
    {
        public GalaxyException() {}

        public GalaxyException(string message) : base(message){}

        public GalaxyException(string message, Exception innerException) : base(message, innerException){}
    }
}
