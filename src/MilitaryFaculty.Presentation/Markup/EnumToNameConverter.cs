using System;
using System.Globalization;
using System.Windows.Data;
using MilitaryFaculty.Common;

namespace MilitaryFaculty.Presentation.Markup
{
    [ValueConversion(typeof (Enum), typeof (string))]
    public class EnumToNameConverter : Converter<EnumToNameConverter>
    {
        /// <summary>
        ///     Converts a value.
        /// </summary>
        /// <returns>
        ///     A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Enum))
            {
                return null;
            }

            return ((Enum) value).GetName();
        }
    }
}