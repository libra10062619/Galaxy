using System;
namespace Galaxy.Infrastructure.Exceptions
{
    public class AggregateNotFoundException : GalaxyException
    {
        public AggregateNotFoundException() { }

        public AggregateNotFoundException(string message) : base(message) { }

        public AggregateNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
