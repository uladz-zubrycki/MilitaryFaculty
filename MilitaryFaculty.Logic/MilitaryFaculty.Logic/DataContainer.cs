using System;
using System.Linq;
using System.Xml.Linq;

namespace MilitaryFaculty.Logic
{

    public class DataContainer
    {
        private XDocument xmlFormulas;
        private XDocument xmlInfo;

        public DataContainer(XDocument XmlFormulas, XDocument XmlInfo)
        {
            xmlFormulas = XmlFormulas;
            xmlInfo = XmlInfo;
        }

        public void FormExcel(/*excel sheet*/)
        {
            //AppDomain.CurrentDomain.BaseDirectory
            //var xmlDoc = XDocument.Load("FormulasFourthTable.xml");

            if (xmlInfo.Root == null)
                throw new Exception();

            var parts = xmlInfo.Root.Elements();
            foreach (var part in parts)
            {
                var ids = part.Elements();
                foreach (var id in ids)
                {
                    //TODO: Formuls Calculations
                    var idName = id.Attribute("name");
                }
            }

            //var xmlFormula = xmlDoc.Root.Elements().Single(x => x.Attribute("id").Value == formulaId);
            //var xmlArguments = xmlFormula.Element("Arguments");
            //var xmlCoefficients = xmlFormula.Element("Coefficients");
        }

        private static int GetResult()
        {
            return 0;
        }
    }
}
