using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Galaxy.Infrastructure.Helper
{
    public static class IdentityHelper
    {
        public static ConcurrentDictionary<Type, IIdGenerator> idGenerators =
            new ConcurrentDictionary<Type, IIdGenerator>();

        public static T NextIdentity<T>()
        {
            Type type = typeof(T); 
            IIdGenerator idGenerator;
            if (idGenerators.TryGetValue(type, out idGenerator))
            {
                return ((IIdGenerator<T>)idGenerator).NextIdentity();

            }

            throw new NotSupportedException($"The identity generator of type {type.Name} is nonsupport.");
        }

        public static void RegisterIdGenerator(IIdGenerator idGenerator)
        {
            var type = idGenerator.GetType().GetInterfaces().First(p => p.IsGenericType).GenericTypeArguments[0];

            RegisterIdGenerator(type, idGenerator);
        }

        public static void RegisterIdGenerator(Type type, IIdGenerator idGenerator)
        {
            idGenerators.TryAdd(type, idGenerator);
        }

        public static void RegisterIdGenerator<T>(IIdGenerator<T> idGenerator)
        {
            Type type = typeof(T);

            RegisterIdGenerator(type, idGenerator);
        }
    }
}
