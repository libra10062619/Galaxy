using System;
using System.Reflection;
using System.Collections.Generic;

namespace Galaxy.Infrastructure.Helper
{
    public static class ReflectionHelper
    {
        public static IEnumerable<MethodInfo> GetMethods(Type type)
        {
            return type.GetMethods();
        }
    }
}
