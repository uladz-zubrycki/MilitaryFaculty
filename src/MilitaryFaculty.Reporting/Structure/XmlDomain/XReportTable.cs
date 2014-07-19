using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MilitaryFaculty.Reporting.Structure.XmlDomain
{
    [Serializable]
    [XmlType(TypeName = "ReportTable")]
    public class XReportTable
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlArray("Groups")]
        [XmlArrayItem("Group", typeof (XReportTableGroup))]
        public List<XReportTableGroup> Groups { get; set; }
    }
}