using System;
using System.Collections.Generic;

namespace MilitaryFaculty.Logic
{
    public class FormulaInfo
    {
        #region Class Fields
        private string expression;
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
                if (value == null) throw new ArgumentNullException("variables");
                
                expression = value.Replace(" ", "");

                if (expression == String.Empty)
                {
                    throw new ArgumentException("expression");
                }

                expression = expression.ToLower();
            }
        }
        #endregion // Class Properties
    }
}
