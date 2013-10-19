using System;
using System.Linq;
using System.Xml.Linq;

namespace MilitaryFaculty.Logic.Services
{
    public static class FormulaProvider
    {
        public static Formula GetFormula(IdsSecondTable id)
        {
            var xmlDoc = XDocument.Load("SecondTable.xml");
            return ReturnFormula(xmlDoc, id.ToString());
        }

        public static Formula GetFormula(IdsThirdTable id)
        {
            var xmlDoc = XDocument.Load("ThirdTable.xml");
            return ReturnFormula(xmlDoc, id.ToString());
        }

        public static Formula GetFormula(IdsFourthTable id)
        {
            var xmlDoc = XDocument.Load("FourthTable.xml");
            return ReturnFormula(xmlDoc, id.ToString());
        }

        private static Formula ReturnFormula(XDocument xmlDoc, string formulaId)
        {
            if (xmlDoc.Root == null)
                throw new Exception();

            var xmlFormula = xmlDoc.Root.Elements().Single(x => x.Attribute("id").Value == formulaId);
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
