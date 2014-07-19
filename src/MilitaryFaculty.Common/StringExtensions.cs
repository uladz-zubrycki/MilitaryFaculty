using System;
using System.Text.RegularExpressions;

namespace MilitaryFaculty.Common
{
    public static class StringExtensions
    {
        public static string MergeSpaces(this string @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            return Regex.Replace(@this, @"\s+", " ");
        }

        // ReSharper disable InconsistentNaming
        public static string f(this string @this, params object[] args)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            return String.Format(@this, args);
        }

        // ReSharper restore InconsistentNaming
    }
}