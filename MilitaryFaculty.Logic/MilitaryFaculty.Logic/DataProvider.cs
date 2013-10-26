using System;
using System.Linq;
using System.Reflection;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Logic
{
    public delegate int InfoMethod();

    public static class DataProvider
    {
        public static int GetValue(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var typeInfo = typeof (DataProvider);
            var method = typeInfo.GetMethods().First(m => ((FormulaArgumentAttribute)
                                                           m.GetCustomAttribute(typeof (FormulaArgumentAttribute)))
                                                              .Name == value);
            return ((InfoMethod) method.CreateDelegate(typeof (InfoMethod)))();
        }

        [FormulaArgument("ProfsCount")]
        private static int ProfessorsCount()
        {
            return 5;
        }
    }
}
