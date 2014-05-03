using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace MilitaryFaculty.FormulaEditor.Domain
{
    internal sealed class Formula
    {
        public readonly string Name;
        public readonly double MaxValue;
        public readonly string Expression;
        public readonly IReadOnlyCollection<Argument> Arguments;
        public readonly IReadOnlyCollection<Coefficient> Coefficients;

        public Formula(string name,
                       double maxValue,
                       string expression,
                       IEnumerable<Argument> arguments,
                       IEnumerable<Coefficient> coefficients)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            if (arguments == null)
            {
                throw new ArgumentNullException("arguments");
            }

            if (coefficients == null)
            {
                throw new ArgumentNullException("coefficients");
            }

            Name = name;
            MaxValue = maxValue;
            Expression = expression;
            Arguments = arguments.ToImmutableList();
            Coefficients = coefficients.ToImmutableList();
        }
    }
}