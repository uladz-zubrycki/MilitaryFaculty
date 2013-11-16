using System;
using System.Collections.Generic;

namespace MilitaryFaculty.Logic
{
    public class FormulaInfo
    {
        #region Class Fields
        private string expression;
        private string name;
        private double maxValue;
        #endregion // Class Fields

        #region Class Properties
        public ICollection<string> Arguments { get; set; }
        public IDictionary<string, double> Coefficients { get; set; }

        public string Expression
        {
            get { return expression; }
            set
            {
                if (value == null) throw new ArgumentNullException();
                if (String.IsNullOrEmpty(value.Trim())) throw new ArgumentException();
                
                expression = value.Replace(" ", "");

                if (expression == String.Empty)
                {
                    throw new ArgumentException("expression");
                }
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (value == null) throw new ArgumentNullException();
                if (String.IsNullOrEmpty(value.Trim())) throw new ArgumentException();

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
        #endregion // Class Properties
    }
}
