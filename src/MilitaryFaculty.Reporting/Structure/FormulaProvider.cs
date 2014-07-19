using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using MilitaryFaculty.Common;
using MilitaryFaculty.Reporting.Structure.XmlDomain;

namespace MilitaryFaculty.Reporting.Structure
{
    public class FormulaProvider : IFormulaProvider
    {
        private readonly Lazy<IDictionary<int, FormulaInfo>> _formulas;

        private IDictionary<int, FormulaInfo> Formulas
        {
            get { return _formulas.Value; }
        }

        public FormulaProvider(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException();
            }

            _formulas = Lazy.Create(() => ReadFormulas(filePath));
        }

        public FormulaInfo GetFormula(int id)
        {
            return Formulas[id];
        }

        private static IDictionary<int, FormulaInfo> ReadFormulas(string filePath)
        {
            Func<XFormula, FormulaInfo> fromXml =
                xmlItem =>
                {
                    var coefficients = xmlItem.Coefficients
                                              .ToDictionary(c => c.Name,
                                                            c => c.Value);

                    var arguments = xmlItem.Arguments
                                           .Select(a => a.Name)
                                           .ToList();

                    return new FormulaInfo(name: xmlItem.Name,
                                           maxValue: xmlItem.MaxValue,
                                           arguments: arguments,
                                           coefficients: coefficients);
                };

            var result = ReadFromFile(filePath).ToDictionary(f => f.Id, fromXml);

            return result;
        }

        private static IEnumerable<XFormula> ReadFromFile(string file)
        {
            var serializer = new XmlSerializer(typeof (List<XFormula>));

            using (var stream = File.OpenRead(file))
            {
                return serializer.Deserialize<List<XFormula>>(stream);
            }
        }
    }
}