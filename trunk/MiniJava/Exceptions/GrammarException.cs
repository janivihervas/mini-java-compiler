using System;

namespace Exceptions
{
    /// @author Jani Viherväs
    /// @version 5.5.2014
    ///
    /// <summary>
    /// Class for grammar exceptions
    /// </summary>
    public class GrammarException : Exception
    {
        /// <summary>
        /// Creates a new file exception
        /// </summary>
        /// <param name="message">Message to be passed on with the exception</param>
        public GrammarException(string message) : base(message) { }
    }
}
