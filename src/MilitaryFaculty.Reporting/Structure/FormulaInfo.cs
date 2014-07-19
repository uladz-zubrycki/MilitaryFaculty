using System;
using System.Collections.Generic;
using System.Linq;

namespace MilitaryFaculty.Reporting.Structure
{
    public class FormulaInfo
    {
        public readonly string Name;
        public readonly double MaxValue;
        public readonly ICollection<string> Arguments;
        public readonly IDictionary<string, double> Coefficients;
        public readonly string Expression;

        public FormulaInfo(string name,
                           double maxValue,
                           IEnumerable<string> arguments,
                           IDictionary<string, double> coefficients)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException();
            }

            if (maxValue < 0)
            {
                throw new ArgumentException();
            }

            if (String.IsNullOrWhiteSpace(Expression))
            {
                throw new ArgumentException();
            }

            if (arguments == null)
            {
                throw new ArgumentNullException();
            }

            if (coefficients == null)
            {
                throw new ArgumentNullException();
            }

            Name = name;
            MaxValue = maxValue;
            Expression = Expression.Replace(" ", "");
            Arguments = arguments.ToList();
            Coefficients = new Dictionary<string, double>(coefficients);
        }
    }
}