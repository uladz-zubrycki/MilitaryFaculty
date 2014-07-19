using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MilitaryFaculty.Reporting.Structure.XmlDomain
{
    [Serializable]
    [XmlType(TypeName = "Formula")]
    public class XFormula
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("expression")]
        public string Expression { get; set; }

        [XmlAttribute("maxValue")]
        public double MaxValue { get; set; }

        [XmlArray("Arguments")]
        [XmlArrayItem("Argument")]
        public List<XArgument> Arguments { get; set; }

        [XmlArray("Coefficients")]
        [XmlArrayItem("Coefficient")]
        public List<XCoefficient> Coefficients { get; set; }
    }
}