using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.Custom
{
    [ValueConversion(typeof(Enum), typeof(ICollection<Enum>))]
    internal class EnumToValuesConverter : Converter<EnumToValuesConverter>
    {
        public EnumToValuesConverter()
        {
            // Empty
        }

        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if (!(value is Enum))
            {
                throw new ArgumentException();
            }

            return Enum.GetValues(value.GetType());
        }
    }
}
