using System;
namespace Galaxy.Infrastructure.Exceptions
{
    public class EntityExistException : GalaxyException
    {
        public EntityExistException(){}

        public EntityExistException(string message) : base(message) { }

        public EntityExistException(string message, Exception innerException) : base(message, innerException) { }
    }
}
