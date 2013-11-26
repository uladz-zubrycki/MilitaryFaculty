using System;
using System.Reflection;

namespace MilitaryFaculty.Extensions
{
    public static class MethodInfoExtensions
    {
        public static bool HasAttribute<TAttribute>(this MethodInfo info)
            where TAttribute: Attribute
        {
            var attr = info.GetCustomAttribute<TAttribute>();
            return attr != null;
        }
    }
}
