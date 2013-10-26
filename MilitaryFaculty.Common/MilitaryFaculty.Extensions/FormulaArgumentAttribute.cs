using System;

namespace MilitaryFaculty.Extensions
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class FormulaArgumentAttribute : Attribute
    {
        #region Class Properties

        public string Name { get; private set; }

        #endregion // Class Properties

        #region Class Constructors

        public FormulaArgumentAttribute(string name)
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
