using System;
using System.Collections.Generic;

namespace MilitaryFaculty.Reporting
{
    public class FormulaInfo
    {
        #region Class Fields

        private string expression;
        private string name;
        private double maxValue;
        private ICollection<string> arguments;
        private IDictionary<string, double> coefficients;

        #endregion // Class Fields

        #region Class Properties

        public string Name
        {
            get { return name; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                if (String.IsNullOrEmpty(value.Trim()))
                {
                    throw new ArgumentException();
                }

                name = value;
            }
        }

        public double MaxValue
        {
            get { return maxValue; }
            set
            {
                if (maxValue < 0)
                {
                    throw new ArgumentException();
                }

                maxValue = value;
            }
        }

        public ICollection<string> Arguments
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

        public IDictionary<string, double> Coefficients
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

        public string Expression
        {
            get { return expression; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
               
                expression = value.Replace(" ", "");

                if (expression == String.Empty)
                {
                    throw new ArgumentException("expression");
                }
            }
        }

        #endregion // Class Properties
    }
}