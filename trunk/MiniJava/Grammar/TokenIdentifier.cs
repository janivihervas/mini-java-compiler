using System.Linq;
using Exceptions;

namespace Grammar
{
    /// @author Jani Viherväs
    /// @version 5.5.2014
    ///
    /// <summary>
    /// Identifier token class.
    /// </summary>
    public class TokenIdentifier : Token
    {
        /// <summary>
        /// Gets the identifier
        /// </summary>
        public string Identifier { get; private set; }

        /// <summary>
        /// Creates a new integer token. ATTENTION! This constructor handles the 0th column and row, DON'T add one to neither one.
        /// </summary>
        /// <param name="line">Current line of the source code.</param>
        /// <param name="startColumn">Starting column of the lexeme.</param>
        /// <param name="identifier">Identifier</param>
        public TokenIdentifier(int line, int startColumn, string identifier)
            : base(line, startColumn, identifier)
        {
            if ( ReservedKeywords.GetReservedKeywords().Contains(identifier) || Types.GetTypes().Contains(identifier) ||
                identifier == "true" || identifier == "false") // Have to do this and remove true and false from ReservedKeywords, because
            {                                                  // otherwise they wouldn't be scanned as TokenTerminal<bool>
                throw new GrammarException("Can't assign reserved keyword as an identifier.");
            }
            Identifier = identifier;
        }
    }
}
