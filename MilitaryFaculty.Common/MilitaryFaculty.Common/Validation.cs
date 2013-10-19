using System;

namespace MilitaryFaculty.Common
{
    public static class Validation
    {
        public static void VerifyString(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }
            if (String.IsNullOrEmpty(value.Trim()))
            {
                throw new ArgumentException();
            }
        }

        public static void VerifyEnum(Type enumType, Enum value)
        {
            if (!Enum.IsDefined(enumType, value))
            {
                throw new ArgumentException();
            }
        }
    }
}
