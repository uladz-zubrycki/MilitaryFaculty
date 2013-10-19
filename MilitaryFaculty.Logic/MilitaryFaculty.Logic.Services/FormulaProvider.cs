using System;
using System.Linq;
using System.Xml.Linq;

namespace MilitaryFaculty.Logic.Services
{
    public class FormulaProvider
    {
        public Formula GetFormula(string formula)
        {
            
            if (formula == null) throw new ArgumentNullException();
            if (String.IsNullOrEmpty(formula.Trim())) throw new ArgumentException();
            

            var xmlDoc = XDocument.Load("ServicesConfig.xml");

            if (xmlDoc.Root == null)
                throw new Exception();

            var xmlFormula = xmlDoc.Root.Elements().Single(x => x.Attribute("name").Value == formula);
            var xmlArguments = xmlFormula.Element("Arguments");
            var xmlCoefficients = xmlFormula.Element("Coefficients");

            if (xmlArguments == null)
                throw new Exception();
            if (xmlCoefficients == null)
                throw new Exception();

            var expression = xmlFormula.Attribute("expression").Value;
            
            var variables = xmlArguments.Elements()
                                     .Select(e => e.Attribute("name").Value)
                                     .ToArray();
            
            var coefficients = xmlCoefficients.Elements()
                                       .Select(e => e.Attribute("name").Value)
                                       .ToArray();

            var values = xmlCoefficients.Elements()
                                      .Select(e => e.Attribute("value").Value)
                                      .ToArray();

            for (int i = 0; i < coefficients.Length; i++)
            {
                expression = expression.Replace(coefficients[i], values[i]);
            }

            return new Formula(expression, variables);
        }
    }
}
