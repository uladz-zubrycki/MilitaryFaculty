using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using MilitaryFaculty.Reporting.XmlDomain;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Reporting.Providers
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
            Func<XFormula, FormulaInfo> convert =
                xmlItem =>
                {
                    var coefficients = xmlItem.Coefficients
                                              .ToDictionary(c => c.Name,
                                                            c => c.Value);

                    var arguments = xmlItem.Arguments
                                           .Select(a => a.Name)
                                           .ToList();

                    return new FormulaInfo
                           {
                               Coefficients = coefficients,
                               Arguments = arguments,
                               Expression = xmlItem.Expression,
                               Name = xmlItem.Name,
                               MaxValue = xmlItem.MaxValue
                           };
                };

            return ReadFromFile(filePath).ToDictionary(f => f.Id, convert);
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