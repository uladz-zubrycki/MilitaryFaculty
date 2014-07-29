using System;
using System.Linq;
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

            var attr = @this.GetCustomAttributes(typeof (TAttribute), true)
                            .FirstOrDefault();

            var result = attr != null;

            return result;
        }

        public static TAttribute GetCustomAttribute<TAttribute>(this MemberInfo @this)
            where TAttribute : Attribute
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            var attr = @this.GetCustomAttributes(typeof (TAttribute), true)
                            .FirstOrDefault();

            var result = attr as TAttribute;
            
            return result;
        }
    }
}