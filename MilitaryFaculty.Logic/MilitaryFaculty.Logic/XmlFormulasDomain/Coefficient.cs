using System;
using System.Xml.Serialization;

namespace MilitaryFaculty.Logic.XmlFormulasDomain
{
    [Serializable]
    public class Coefficient
    {
        #region Class Properties

        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("value")]
        public double Value { get; set; }

        #endregion // Class Properties
    }
}
