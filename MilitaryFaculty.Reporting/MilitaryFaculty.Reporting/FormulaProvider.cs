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
        private readonly ICollection<string> files;
        private IDictionary<string, FormulaInfo> formulas;

        private IDictionary<string, FormulaInfo> Formulas
        {
            get
            {
                if (formulas == null)
                {
                    formulas = ReadFormulas();
                }

                return formulas;
            }
        }

        public FormulaProvider(IEnumerable<string> files)
        {
            if (files == null)
            {
                throw new ArgumentNullException("files");
            }

            this.files = files.ToList();
        }

        public FormulaInfo GetFormula(string id)
        {
            return Formulas[id];
        }

        private IDictionary<string, FormulaInfo> ReadFormulas()
        {
            return files.SelectMany(ReadFromFile)
                        .AsParallel()
                        .Select(f => new
                                     {
                                         f.Id,
                                         FormulaInfo = XFormula.ToFormulaInfo(f),
                                     })
                        .ToDictionary(pair => pair.Id,
                                      pair => pair.FormulaInfo);
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
