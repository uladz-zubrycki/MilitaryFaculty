using System;

namespace MilitaryFaculty.Common
{
    [AttributeUsage(AttributeTargets.Enum)]
    public class LocalizedEnumAttribute: Attribute
    {
        public readonly Type ResourceSource;

        public LocalizedEnumAttribute(Type resourceSource)
        {
            if (resourceSource == null)
            {
                throw new ArgumentNullException("ResourceSource");
            }

            ResourceSource = resourceSource;
        }
    }
}
