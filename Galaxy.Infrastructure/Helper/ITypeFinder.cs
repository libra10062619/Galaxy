using System;
using System.Collections.Generic;
using System.Reflection;

namespace Galaxy.Infrastructure.Helper
{
    public interface ITypeFinder
    {
        /// <summary>
        /// Finds the type of the class which is implemented/extended the given interface/type.
        /// </summary>
        /// <returns>The class of type.</returns>
        /// <param name="targetAssembly">Target assembly.</param>
        /// <param name="onlyConcreteClasses">If set to <c>true</c> only concrete classes.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        IEnumerable<Type> FindClassOfType<T>(IEnumerable<string> targetAssembly = null, bool onlyConcreteClasses = true);
        /// <summary>
        /// Finds the type of the class which is implemented/extended the given interface/type.
        /// </summary>
        /// <returns>The class of type.</returns>
        /// <param name="targetType">Target type.</param>
        /// <param name="targetAssembly">Target assembly.</param>
        /// <param name="onlyConcreteClasses">If set to <c>true</c> only concrete classes.</param>
        IEnumerable<Type> FindClassOfType(Type targetType, IEnumerable<string> targetAssembly = null, bool onlyConcreteClasses = true);
        /// <summary>
        /// Finds the type of the class which is implemented/extended the given interface/type.
        /// </summary>
        /// <returns>The class of type.</returns>
        /// <param name="targetType">Target type.</param>
        /// <param name="assemblies">Assemblies.</param>
        /// <param name="onlyConcreteClasses">If set to <c>true</c> only concrete classes.</param>
        IEnumerable<Type> FindClassOfType(Type targetType, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);
    }
}
