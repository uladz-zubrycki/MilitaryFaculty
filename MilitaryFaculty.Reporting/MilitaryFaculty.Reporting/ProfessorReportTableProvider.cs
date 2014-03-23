using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using MilitaryFaculty.Reporting.XmlDomain;

namespace MilitaryFaculty.Reporting
{
    class ProfessorReportTableProvider : IReportTableProvider
    {
        private readonly ICollection<string> _files;

        private ICollection<XReportTable> _tables;

        public ProfessorReportTableProvider(IEnumerable<string> files)
        {
            if (files == null)
            {
                throw new ArgumentNullException("files");
            }

            _files = files.ToList();
        }

        public ICollection<XReportTable> GetTables()
        {
            return _tables ?? (_tables = InitTables());
        }

        private ICollection<XReportTable> InitTables()
        {
            return _files.Select(ReadTable)
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
