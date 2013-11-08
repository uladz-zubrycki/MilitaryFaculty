using System;
using System.Linq;
using System.Reflection;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Logic
{
    #region Class Public Methods

    public delegate int InfoMethod();

    public static class DataProvider
    {
        public static double GetValue(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }


            var method = typeof (DataProvider).GetMethods()
                                              .FirstOrDefault(m =>
                                                  {
                                                      var attr = m.GetCustomAttribute<FormulaArgumentAttribute>();
                                                      return attr != null && attr.Name == value;
                                                  });
            return method != null ? (double)method.Invoke(null, null) : 0;
        }

        [FormulaArgument("ProfsCount")]
        public static double ProfessorsCount()
        {
            return 5;
        }

        #endregion // Class Public Methods
    }
}
