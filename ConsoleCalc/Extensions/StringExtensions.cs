using System.Text.RegularExpressions;

namespace ConsoleCalc.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Пока что работает только на знаки "-"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveExcessLeadingSign(this string value)
        {
            var regex = new Regex("[-]{2,}");
            var match = regex.Match(value);
            if (match.Success)
            {
                if (match.Length % 2 > 0)
                    return regex.Replace(value, "-");
                else
                    return regex.Replace(value, "");
            }
            else
            {
                return value;
            }
        }
        
        public static string RemoveExcessSpacebar(this string value)
        {
            var regex = new Regex("[ ]{2,}");
            var result = regex.Replace(value, " ");
            return result;
        }
    }
}