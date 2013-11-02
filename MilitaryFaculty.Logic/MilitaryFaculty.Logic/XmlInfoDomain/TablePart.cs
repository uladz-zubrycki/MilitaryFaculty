using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MilitaryFaculty.Logic.XmlInfoDomain
{
    [Serializable]
    public class TablePart
    {
        #region Class Properties
        [XmlAttribute("name")]
        public string Name { get; set; }
        public List<string> Identifiers { get; set; }

        #endregion // Class Properties
    }
}
