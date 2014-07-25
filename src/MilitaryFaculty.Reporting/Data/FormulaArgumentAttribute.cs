using System;

namespace MilitaryFaculty.Reporting.Data
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class FormulaArgumentAttribute : Attribute
    {
        public readonly string Name;

        public FormulaArgumentAttribute(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException();
            }

            Name = name;
        }
    }
}