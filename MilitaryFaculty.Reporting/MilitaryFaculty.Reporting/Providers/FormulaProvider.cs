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
        private readonly ICollection<string> _files;
        private IDictionary<string, FormulaInfo> _formulas;

        private IDictionary<string, FormulaInfo> Formulas
        {
            get
            {
                if (_formulas == null)
                {
                    _formulas = ReadFormulas();
                }

                return _formulas;
            }
        }

        public FormulaProvider(IEnumerable<string> files)
        {
            if (files == null)
            {
                throw new ArgumentNullException("files");
            }

            _files = files.ToList();
        }

        public FormulaInfo GetFormula(string id)
        {
            return Formulas[id];
        }

        private IDictionary<string, FormulaInfo> ReadFormulas()
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

            return _files.SelectMany(ReadFromFile)
                         .ToDictionary(f => f.Id, convert);
        }

        private static IEnumerable<XFormula> ReadFromFile(string file)
        {
            var serializer = new XmlSerializer(typeof (List<XFormula>));

            using (var stream = new FileStream(file, FileMode.Open))
            {
                return serializer.Deserialize<List<XFormula>>(stream);
            }
        }
    }
}