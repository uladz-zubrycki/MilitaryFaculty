using System;
using System.Linq;
using System.Reflection;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Logic
{



    public delegate int InfoMethod();

    public class DataProvider
    {
        #region Class Fields
        private readonly Professor professor;
        #endregion Class Fields

        #region Class Constructors

        public DataProvider()
        {
            professor = null;
        }

        public DataProvider(Professor professor)
        {
            this.professor = professor;
        }

        #endregion Class Constructors

        #region Class Public Methods

        public double GetValue(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (professor != null)
                value += "Prof";

            var method = typeof (DataProvider).GetMethods()
                                              .FirstOrDefault(m =>
                                                  {
                                                      var attr = m.GetCustomAttribute<FormulaArgumentAttribute>();
                                                      return attr != null && attr.Name == value;
                                                  });
            return method != null ? (double) method.Invoke(null, null) : 0;
        }

        #endregion // Class Public Methods

        #region Class Public Argument Methods

        [FormulaArgument("a1")]
        public static double Pc1()
        {
            return 1;
        }

        [FormulaArgument("a2")]
        public static double Pc2()
        {
            return 2;
        }

        [FormulaArgument("a3")]
        public static double Pc3()
        {
            return 3;
        }

        [FormulaArgument("a4")]
        public static double Pc4()
        {
            return 4;
        }

        [FormulaArgument("a5")]
        public static double Pc5()
        {
            return 5;
        }

        #endregion Class Public Argument Methods
    }
}
