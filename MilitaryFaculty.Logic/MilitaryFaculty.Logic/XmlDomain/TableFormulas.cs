using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace MilitaryFaculty.Logic.XmlDomain
{
    [Serializable]
    public class TableFormulas
    {
        #region Class Fields

        private List<Formula> formulas;

        #endregion // Class Fields

        #region Class Properties

        public List<Formula> Formulas
        {
            get { return formulas; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("value");
                }

                formulas = value;
            }
        }

        #endregion // Class Properties

        #region Class Public Methods

        public static void Serialize(TableFormulas tableFormulas, string filename)
        {
            var serializer = new XmlSerializer(typeof(TableFormulas));
            var stream = new StreamWriter(filename);

            try
            {
                serializer.Serialize(stream, tableFormulas);
            }
            finally
            {
                stream.Close();
            }
        }

        public static TableFormulas Deserialize(string filename)
        {
            TableFormulas tableFormulas;
            var serializer = new XmlSerializer(typeof(TableFormulas));
            var stream = new FileStream(filename, FileMode.Open);

            try
            {
                tableFormulas = (TableFormulas)serializer.Deserialize(stream);
            }
            finally
            {
                stream.Close();
            }

            return tableFormulas;
        }

        #endregion // Class Public Methods
    }
}
