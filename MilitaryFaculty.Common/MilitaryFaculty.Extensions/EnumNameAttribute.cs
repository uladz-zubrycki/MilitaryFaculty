using System;

namespace MilitaryFaculty.Extensions
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumNameAttribute : Attribute
    {
        #region Class Properties

        public string Name { get; private set; }

        #endregion // Class Properties

        #region Class Constructors

        public EnumNameAttribute(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException();
            }

            Name = name;
        }

        #endregion // Class Constructors
    }
}