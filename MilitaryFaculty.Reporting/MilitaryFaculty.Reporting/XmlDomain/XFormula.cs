using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace MilitaryFaculty.Reporting.XmlDomain
{
    [Serializable]
    [XmlType(TypeName = "Formula")]
    public class XFormula
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("expression")]
        public string Expression { get; set; }

        [XmlAttribute("maxValue")]
        public double MaxValue { get; set; }

        [XmlArray("Arguments")]
        [XmlArrayItem("Argument")]
        public List<XArgument> Arguments { get; set; }

        [XmlArray("Coefficients")]
        [XmlArrayItem("Coefficient")]
        public List<XCoefficient> Coefficients { get; set; }

        public static FormulaInfo ToFormulaInfo(XFormula formula)
        {
            if (formula == null)
            {
                throw new ArgumentNullException("XFormula");
            }

            return new FormulaInfo
                   {
                       Coefficients = formula.Coefficients.ToDictionary(c => c.Name, c => c.Value),
                       Arguments = formula.Arguments.Select(a => a.Name).ToList(),
                       Expression = formula.Expression,
                       Name = formula.Name,
                       MaxValue = formula.MaxValue
                   };
        }
    }
}