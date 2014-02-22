using System;

namespace MilitaryFaculty.Extensions
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumNameAttribute : Attribute
    {
        public string Name { get; private set; }

        public EnumNameAttribute(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException();
            }

            Name = name;
        }
    }
}