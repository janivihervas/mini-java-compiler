namespace Grammar
{
    /// @author Jani Viherväs
    /// @version 15.3.2014
    ///
    /// <summary>
    /// Class for error tokens
    /// </summary>
    public class TokenError : Token
    {
        /// <summary>
        /// Creates a new error token
        /// </summary>
        /// <param name="line">Current line</param>
        /// <param name="startColumn">Starting column</param>
        /// <param name="errorLexeme">The unidentified lexeme</param>
        public TokenError(int line, int startColumn, string errorLexeme)
            : base(line, startColumn, errorLexeme)
        {
        }


        /// <summary>
        /// Combines two consecutive error tokens
        /// </summary>
        /// <param name="token">Previous error token</param>
        /// <param name="whiteSpacesSkipped">How many white spaces where skipped.</param>
        public void CombineErrorTokens(TokenError token, int whiteSpacesSkipped)
        {
            var whiteSpaces = "";
            for ( var i = 0; i < whiteSpacesSkipped; i++ )
            {
                whiteSpaces += " ";
            }
            Lexeme += whiteSpaces + token.Lexeme;
        }
    }
}
