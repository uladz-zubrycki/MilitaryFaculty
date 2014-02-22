using System.Text.RegularExpressions;

namespace MilitaryFaculty.Extensions
{
    public static class StringExtensions
    {
        public static string MergeSpaces(this string value)
        {
            return Regex.Replace(value, @"\s+", " ");
        }
    }
}