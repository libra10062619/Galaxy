using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Galaxy.Infrastructure.Helper
{
    /// <summary>
    /// Type finder.
    /// </summary>
    public class TypeFinder : ITypeFinder
    {
        /// <summary>
        /// The assimbles which should be skiped when loading.
        /// </summary>
        private List<string> AssemblyScope = new List<string>();

        public TypeFinder(){}

        public TypeFinder(IEnumerable<string> assemblyScope)
        {
            AssemblyScope.AddRange(assemblyScope);
        }

        /// <summary>
        /// Finds the type of the class which is implemented/extended the given interface/type.
        /// </summary>
        /// <returns>The class of type.</returns>
        /// <param name="targetAssembly">Target assembly.</param>
        /// <param name="onlyConcreteClasses">If set to <c>true</c> only concrete classes.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public virtual IEnumerable<Type> FindClassOfType<T>(IEnumerable<string> targetAssembly = null, bool onlyConcreteClasses = true)
        {
            return FindClassOfType(typeof(T), targetAssembly, onlyConcreteClasses);
        }

        /// <summary>
        /// Finds the type of the class which is implemented/extended the given interface/type.
        /// </summary>
        /// <returns>The class of type.</returns>
        /// <param name="targetType">Target type.</param>
        /// <param name="targetAssembly">Target assembly.</param>
        /// <param name="onlyConcreteClasses">If set to <c>true</c> only concrete classes.</param>
        public virtual IEnumerable<Type> FindClassOfType(Type targetType, IEnumerable<string> targetAssembly = null, bool onlyConcreteClasses = true)
        {
            var assemblies = GetAllAssemblies();

            return FindClassOfType(targetType, assemblies, onlyConcreteClasses);
        }

        /// <summary>
        /// Finds the type of the class which is implemented/extended the given interface/type.
        /// </summary>
        /// <returns>The class of type.</returns>
        /// <param name="targetType">Target type.</param>
        /// <param name="assemblies">Assemblies.</param>
        /// <param name="onlyConcreteClasses">If set to <c>true</c> only concrete classes.</param>
        public virtual IEnumerable<Type> FindClassOfType(Type targetType, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            var result = new List<Type>();

            foreach (var t in assemblies.SelectMany(o => o.GetTypes()).Where(p => p.IsClass))
            {
                if (targetType.GetTypeInfo().IsAssignableFrom(t) ||
                   (targetType.IsGenericTypeDefinition && DoesTypeImplementOpenGeneric(t, targetType)))
                {
                    if (onlyConcreteClasses)
                    {
                        if (t.IsClass && !t.IsAbstract)
                        {
                            result.Add(t);
                        }
                    }
                    else
                    {
                        result.Add(t);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Does the type implemented an generic type.
        /// </summary>
        /// <returns><c>true</c>, if type implement open generic was doesed, <c>false</c> otherwise.</returns>
        /// <param name="type">Type.</param>
        /// <param name="openGeneric">Open generic.</param>
        protected virtual bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
        {
            try
            {
                var genericTypeDefinition = openGeneric.GetGenericTypeDefinition();
                foreach (var implementedInterface in type.FindInterfaces((objType, objCriteria) => true, null))
                {
                    if (!implementedInterface.IsGenericType)
                        continue;

                    var isMatch = genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
                    return isMatch;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets all assemblies.
        /// </summary>
        /// <returns>The assemblies.</returns>
        protected virtual IEnumerable<Assembly> GetAllAssemblies() => AppDomain.CurrentDomain.GetAssemblies();
    }
}
