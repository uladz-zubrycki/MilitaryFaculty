using System;
using System.Collections.Generic;

namespace MilitaryFaculty.Logic
{
    public class FormulaInfo
    {
        private string formula;

        #region Class Fields

        public string Formula
        {
            get { return formula; }
            set
            {
                if (value == null) throw new ArgumentNullException();
                if (String.IsNullOrEmpty(value.Trim())) throw new ArgumentException();
                if (value == null) throw new ArgumentNullException("variables");
                
                formula = value.Replace(" ", "");

                if (formula == String.Empty)
                {
                    throw new ArgumentException("formulaCore");
                }

                formula = formula.ToLower();
            }
        }

        public ICollection<string> Arguments { get; set; }
        public IDictionary<string, double> Coefficients { get; set; }

        #endregion // Class Fields
    }
}
