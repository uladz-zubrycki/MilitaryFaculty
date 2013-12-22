using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using MilitaryFaculty.Reporting.XmlDomain;

namespace MilitaryFaculty.Reporting
{
    public class ReportTableProvider : IReportTableProvider
    {
        private readonly ICollection<string> files;

        private ICollection<XReportTable> tables;

        public ReportTableProvider(IEnumerable<string> files)
        {
            if (files == null)
            {
                throw new ArgumentNullException("files");
            }

            this.files = files.ToList();
        }

        public ICollection<XReportTable> GetTables()
        {
            return tables ?? (tables = InitTables());
        }

        private ICollection<XReportTable> InitTables()
        {
            return files.Select(ReadTable)
                        .ToList();
        }

        private static XReportTable ReadTable(string file)
        {
            var serializer = new XmlSerializer(typeof(XReportTable));
            using (var reader = XmlReader.Create(file))
            {
                return (XReportTable)serializer.Deserialize(reader);
            }
        }
    }
}