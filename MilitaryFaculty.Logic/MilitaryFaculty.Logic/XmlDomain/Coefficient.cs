using System;
using System.Xml.Serialization;

namespace MilitaryFaculty.Logic.XmlDomain
{
    [Serializable]
    public class Coefficient
    {
        #region Class Fields

        private string name;

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

        [XmlAttribute("value")]
        public double Value { get; set; }

        #endregion // Class Properties
    }
}
