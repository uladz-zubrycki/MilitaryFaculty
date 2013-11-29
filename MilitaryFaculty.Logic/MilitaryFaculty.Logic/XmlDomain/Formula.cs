using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace MilitaryFaculty.Logic.XmlDomain
{
    [Serializable]
    public class Formula
    {
        #region Class Fields
        private string name;
        private string id;
        private string expression;
        private double maxValue;
        private List<Argument> arguments;
        private List<Coefficient> coefficients;
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

        [XmlAttribute("id")]
        public string Id
        {
            get { return id; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("value");
                }

                id = value;
            }
        }

        [XmlAttribute("expression")]
        public string Expression
        {
            get { return expression; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("value");
                }

                expression = value;
            }
        }

        [XmlAttribute("maxValue")]
        public double MaxValue
        {
            get { return maxValue; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("value");
                }

                maxValue = value;
            }
        }

        public List<Argument> Arguments
        {
            get { return arguments; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                arguments = value;
            }
        }

        public List<Coefficient> Coefficients
        {
            get { return coefficients; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                coefficients = value;
            }
        }

        #endregion // Class Properties

        #region Class Public Methods

        public FormulaInfo ToFormulaInfo()
        {
            return new FormulaInfo
            {
                Coefficients = this.Coefficients.ToDictionary(c => c.Name,
                                                              c => c.Value),
                Arguments = this.Arguments.Select(a => a.Name).ToList(),
                Expression = this.Expression,
                Name = this.Name,
                MaxValue = this.MaxValue
            };
        }

        #endregion // Class Public Methods
    }
}
