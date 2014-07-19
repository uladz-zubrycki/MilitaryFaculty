using System;
using System.Reflection;

namespace MilitaryFaculty.Common
{
    public static class MemberInfoExtensions
    {
        public static bool HasAttribute<TAttribute>(this MemberInfo @this)
            where TAttribute : Attribute
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            var attr = @this.GetCustomAttribute<TAttribute>();
            
            return attr != null;
        }
    }
}