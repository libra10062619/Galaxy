using System;
namespace Galaxy.Infrastructure
{
    internal class Singleton<T> where T : class, new()
    {
        private static readonly T instance = new Singleton<T>().GenerateInstance();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Singleton()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private Singleton()
        {
        }

        private T GenerateInstance()
        {
            return Activator.CreateInstance<T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static T Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
