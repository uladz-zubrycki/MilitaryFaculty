using System;
using System.Linq;

namespace MilitaryFaculty.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetName(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var type = value.GetType();
            var fieldName = Enum.GetName(type, value);
            var attr = type.GetField(fieldName)
                          .GetCustomAttributes(typeof (EnumNameAttribute), false)
                          .Single() as EnumNameAttribute;

            return attr == null ? String.Empty : attr.Name;
        }

        public static bool IsDefined(this Enum value)
        {
            return Enum.IsDefined(value.GetType(), value);
        }
    }
}
