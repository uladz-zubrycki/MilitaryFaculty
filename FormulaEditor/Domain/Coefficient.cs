using System;

namespace MilitaryFaculty.FormulaEditor.Domain
{
    /// <summary>
    /// Formula coefficient.
    /// </summary>
    internal sealed class Coefficient
    {
        public readonly string Name;
        public readonly double Value;

        /// <summary>
        /// Creates new instance of <see cref="Coefficient"/> using provided parameters.
        /// </summary>
        /// <param name="name">Name of coefficient.</param>
        /// <param name="value">Coefficient value.</param>
        public Coefficient(string name, double value)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
            Value = value;
        }
    }
}