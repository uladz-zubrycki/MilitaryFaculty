using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MilitaryFaculty.Logic.XmlDomain
{
    [Serializable]
    public class TablePart
    {
        #region Class Fields

        private string name;
        private List<string> identifiers;

        #endregion // Class Fields

        #region Class Properties
        [XmlAttribute("name")]
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("value");
                }

                name = value;
            }
        }

        public List<string> Identifiers
        {
            get { return identifiers; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("value");
                }

                identifiers = value;
            }
        }

        #endregion // Class Properties
    }
}
