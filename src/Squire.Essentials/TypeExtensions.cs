namespace Squire
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public static class TypeExtensions
    {
        public static bool Implements<T>(this Type type)
        {
            return type.GetInterfaces().Any(i => i == typeof(T));
        }
    }
}
