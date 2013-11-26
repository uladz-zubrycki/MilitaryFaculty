using System;
using System.Xml.Serialization;

namespace MilitaryFaculty.Logic.XmlDomain
{
    [Serializable]
    public class Argument
    {
        #region Class Properties

        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("text")]
        public string Text { get; set; }

        #endregion // Class Properties
    }
}
