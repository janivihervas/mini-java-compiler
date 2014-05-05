using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Helper
{
    /// <summary>
    /// Class library for string operations
    /// </summary>
    public static class StringOperations
    {

        /// <summary>
        /// Parses string to a list
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static List<string> SplitAtLineBreaks(string s)
        {
            return new List<string>(Regex.Split(s, "\r\n"));
        }
    }
}
