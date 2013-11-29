using System;
using System.Xml.Serialization;

namespace MilitaryFaculty.Logic.XmlDomain
{
    [Serializable]
    public class Argument
    {
        #region Class Fields

        private string name;
        private string text;

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

        [XmlAttribute("text")]
        public string Text
        {
            get { return text; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("value");
                }

                text = value;
            }
        }

        #endregion // Class Properties
    }
}
