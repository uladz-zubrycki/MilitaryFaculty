using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace MilitaryFaculty.Logic.XmlDomain
{
    [Serializable]
    public class TableInfo
    {
        #region Class Fields

        private string name;
        private List<TablePart> tableParts;

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

        public List<TablePart> TableParts
        {
            get { return tableParts; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("value");
                }

                tableParts = value;
            }
        }

        #endregion // Class Properties

        #region Class Public Methods

        public static void Serialize(TableInfo tableInfo, string filename)
        {
            var serializer = new XmlSerializer(typeof(TableInfo));
            var stream = new StreamWriter(filename);
            
            try
            {
                serializer.Serialize(stream, tableInfo);
            }
            finally
            {
                stream.Close();
            }
        }

        public static TableInfo Deserialize(string filename)
        {
            TableInfo tableInfo;
            var serializer = new XmlSerializer(typeof(TableInfo));
            var stream = new FileStream(filename, FileMode.Open);

            try
            {
                tableInfo = (TableInfo)serializer.Deserialize(stream);
            }
            finally
            {
                stream.Close();
            }

            return tableInfo;
        }

        #endregion // Class Public Methods
    }
}
