using System;
using System.Collections.Generic;

namespace MilitaryFaculty.Reporting
{
    public class FormulaInfo
    {
        private ICollection<string> _arguments;
        private IDictionary<string, double> _coefficients;
        private string _expression;
        private double _maxValue;
        private string _name;

        public string Name
        {
            get { return _name; }
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

                _name = value;
            }
        }

        public double MaxValue
        {
            get { return _maxValue; }
            set
            {
                if (_maxValue < 0)
                {
                    throw new ArgumentException();
                }

                _maxValue = value;
            }
        }

        public ICollection<string> Arguments
        {
            get { return _arguments; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                _arguments = value;
            }
        }

        public IDictionary<string, double> Coefficients
        {
            get { return _coefficients; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                _coefficients = value;
            }
        }

        public string Expression
        {
            get { return _expression; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                _expression = value.Replace(" ", "");

                if (_expression == String.Empty)
                {
                    throw new ArgumentException("expression");
                }
            }
        }
    }
}