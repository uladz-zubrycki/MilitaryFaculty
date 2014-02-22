using System;

namespace MilitaryFaculty.Extensions
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class FormulaArgumentAttribute : Attribute
    {
        public string Name { get; private set; }

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