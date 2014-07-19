using System;
using System.Reflection;
using System.Resources;

namespace MilitaryFaculty.Common
{
    public static class EnumExtensions
    {
        public static bool IsDefined(this Enum value)
        {
            return Enum.IsDefined(value.GetType(), value);
        }

        public static string GetName(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var enumType = value.GetType();
            var enumFieldName = Enum.GetName(enumType, value);
            var localizationAttr = enumType.GetCustomAttribute<LocalizedEnumAttribute>();

            if (localizationAttr == null)
            {
                return enumFieldName;
            }

            var localized = FindLocalizedName(enumType,
                                              enumFieldName,
                                              localizationAttr.ResourceSource);

            return localized ?? enumFieldName;
        }

        private static string FindLocalizedName(Type enumType,
                                                string enumFieldName,
                                                Type resourceSource)
        {
            var resourceManager = new ResourceManager(resourceSource);
            var resourceStrName = enumType.Name + "_" + enumFieldName;
            var resourceStr = resourceManager.GetString(resourceStrName);

            return resourceStr;
        }
    }
}