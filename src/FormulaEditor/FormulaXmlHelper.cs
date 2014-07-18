using System;
using System.Linq;
using MilitaryFaculty.FormulaEditor.Domain;
using MilitaryFaculty.Reporting.XmlDomain;

namespace MilitaryFaculty.FormulaEditor
{
    internal static class FormulaXmlHelper
    {
        public static XFormula ToXml(Formula formula)
        {
            if (formula == null)
            {
                throw new ArgumentNullException("formula");
            }

            var arguments =
                formula
                    .Arguments
                    .Select(ToXml)
                    .ToList();

            var coefficients =
                formula
                    .Coefficients
                    .Select(ToXml)
                    .ToList();

            return new XFormula
                   {
                       Arguments = arguments,
                       Coefficients = coefficients,
                       Expression = formula.Expression,
                       MaxValue = formula.MaxValue,
                       Name = formula.Name
                   };
        }

        public static Formula FromXml(XFormula formula)
        {
            if (formula == null)
            {
                throw new ArgumentNullException("formula");
            }

            throw new NotImplementedException();
        }

        private static XArgument ToXml(Argument argument)
        {
            return new XArgument
                   {
                       Name = argument.Name,
                       Text = argument.Text
                   };
        }

        private static XCoefficient ToXml(Coefficient coefficient)
        {
            return new XCoefficient
                   {
                       Name = coefficient.Name,
                       Value = coefficient.Value
                   };
        }
    }
}