using System;
using System.Collections.Generic;

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
        /// Gets the syntax errors
        /// </summary>
        public List<Error> Errors { get; private set; }

        /// <summary>
        /// Error message
        /// </summary>
        private static string _message = "There were syntax errors.";

        /// <summary>
        /// Creates a new grammar exception
        /// </summary>
        /// <param name="message">Message to be passed on with the exception</param>
        public GrammarException(string message) : base(message)
        {
            _message = message;
            Errors = new List<Error>();
        }

        /// <summary>
        /// Creates a new grammar exception
        /// </summary>
        /// <param name="errors">Syntax errors</param>
        public GrammarException(List<Error> errors)
            : base(_message)
        {
            Errors = errors ?? new List<Error>();
        }


        /// <summary>
        /// Gets the error message
        /// </summary>
        public override string Message
        {
            get { return _message + "\n" + String.Join("\n", Errors); }
        }
    }
}
