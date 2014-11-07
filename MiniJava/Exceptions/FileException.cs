using System;

namespace Exceptions
{
    /// @author Jani Viherväs
    /// @version 28.2.2014
    ///
    /// <summary>
    /// Class for file exceptions
    /// </summary>
    public class FileException : Exception
    {
        /// <summary>
        /// Creates a new file exception
        /// </summary>
        /// <param name="message">Message to be passed on with the exception</param>
        public FileException(string message) : base(message) { }
    }
}
