namespace Grammar
{
    /// @author Jani Viherväs
    /// @version 5.5.2014
    /// 
    /// <summary>
    /// Base token class
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Current line of the source code
        /// </summary>
        public int Line { get; private set; }

        /// <summary>
        /// Starting column of the lexeme
        /// </summary>
        public int StartColumn { get; private set; }

        /// <summary>
        /// Lexeme
        /// </summary>
        public string Lexeme { get; protected set; }


        /// <summary>
        /// Creates a new token. ATTENTION! This constructor handles the 0th column and row, DON'T add one to neither one.
        /// </summary>
        /// <param name="line">Current line of the source code.</param>
        /// <param name="startColumn">Starting column of the lexeme.</param>
        /// <param name="identifier">Lexeme.</param>
        public Token(int line, int startColumn, string identifier)
        {
            Line = line + 1;
            StartColumn = startColumn + 1;
            Lexeme = identifier;
        }


        /// <summary>
        /// Creates a new token. ATTENTION! This constructor handles the 0th column and row, DON'T add one to neither one.
        /// </summary>
        /// <param name="line">Current line of the source code.</param>
        /// <param name="startColumn">Starting column of the lexeme.</param>
        protected Token(int line, int startColumn)
        {
            Line = line + 1;
            StartColumn = startColumn + 1;
        }

        /// <summary>
        /// Returns the lexeme
        /// </summary>
        /// <returns>Lexeme</returns>
        public override string ToString()
        {
            return Lexeme;
        }
    }
}
