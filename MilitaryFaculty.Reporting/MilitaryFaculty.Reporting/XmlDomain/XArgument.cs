using System;
using System.Xml.Serialization;

namespace MilitaryFaculty.Reporting.XmlDomain
{
    [Serializable]
    [XmlType(TypeName = "Argument")]
    public class XArgument
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("text")]
        public string Text { get; set; }
    }
}
