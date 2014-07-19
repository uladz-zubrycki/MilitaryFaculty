using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using MilitaryFaculty.Common;
using MilitaryFaculty.Reporting.Structure.XmlDomain;

namespace MilitaryFaculty.Reporting
{
    public class ReportTableProvider : IReportTableProvider
    {
        private const string TablePattern = @"table\d*";
        private const RegexOptions TablePatternOptions = RegexOptions.IgnoreCase;

        private readonly Lazy<ICollection<XReportTable>> _tables;

        public ReportTableProvider(string tablesPath)
        {
            if (tablesPath == null)
            {
                throw new ArgumentNullException("tablesPath");
            }

            _tables = Lazy.Create(() => RetreiveTables(tablesPath));
        }

        public ICollection<XReportTable> GetTables()
        {
            return _tables.Value;
        }

        private static ICollection<XReportTable> RetreiveTables(string tablesPath)
        {
            if (!Directory.Exists(tablesPath))
            {
                throw new DirectoryNotFoundException();
            }

            Func<string, bool> isReportTable =
                filePath =>
                {
                    var fileName = Path.GetFileName(filePath);

                    return Regex.IsMatch(fileName,
                                         TablePattern,
                                         TablePatternOptions);
                };

            var result = Directory.EnumerateFiles(tablesPath)
                                  .Where(isReportTable)
                                  .Select(ReadTable)
                                  .ToList();

            return result;
        }

        private static XReportTable ReadTable(string file)
        {
            var serializer = new XmlSerializer(typeof (XReportTable));
            using (var reader = XmlReader.Create(file))
            {
                return (XReportTable) serializer.Deserialize(reader);
            }
        }
    }
}