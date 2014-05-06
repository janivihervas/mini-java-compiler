using System;

namespace Exceptions
{
    /// @author Jani Viherväs
    /// @version 27.2.2014
    ///
    /// <summary>
    /// Represent syntax error
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Gets the line where the error occured and a cursor pointing the column, f.g.:
        /// varr i : int := 1;
        /// ^
        /// </summary>
        public string LineAndErrorCursor { get; private set; }

        /// <summary>
        /// "Line x, column y: Error message"
        /// </summary>
        public string ErrorMessage { get; private set; }

        public Error(string line, int lineNumber, int startColumn, string errorMessage)
        {
            ErrorMessage = String.Format("Line {0}, column {1}: {2}", lineNumber, startColumn, errorMessage);
            var cursor = "";
            for ( var i = 0; i < startColumn - 1; i++ )
            {
                cursor += " ";
            }
            cursor += "^";
            LineAndErrorCursor = line + "\n" + cursor;
        }

        public Error(string message)
        {
            ErrorMessage = message;
        }

        /// <summary>
        /// Concatenates ErrorMessage and LineAndErrorCursor
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ErrorMessage + "\n" + LineAndErrorCursor;
        }
    }
}
