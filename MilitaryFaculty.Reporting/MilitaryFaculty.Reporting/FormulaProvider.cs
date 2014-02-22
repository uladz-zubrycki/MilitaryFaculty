using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using MilitaryFaculty.Reporting.XmlDomain;

namespace MilitaryFaculty.Reporting
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
            return _files.SelectMany(ReadFromFile)
                         .AsParallel()
                         .ToDictionary(f => f.Id, XFormula.ToFormulaInfo);
        }

        private static IEnumerable<XFormula> ReadFromFile(string file)
        {
            var serializer = new XmlSerializer(typeof (List<XFormula>));

            using (var stream = new FileStream(file, FileMode.Open))
            {
                return ((List<XFormula>) serializer.Deserialize(stream));
            }
        }
    }
}