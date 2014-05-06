using System;
using System.Collections.Generic;
using Exceptions;
using Grammar;

namespace FrontEnd
{
    /// @author Jani Viherväs
    /// @version 5.5.2014
    /// 
    /// <summary>
    /// Parser to create abstract syntax tree
    /// </summary>
    public class Parser
    {
        private int _i;
        private List<Token> _tokens;
        private List<Error> _syntaxErrors;


        /// <summary>
        /// Parses the token list
        /// </summary>
        /// <param name="tokens">Tokens scanned</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Parse(List<Token> tokens)
        {
            if ( tokens == null )
            {
                throw new ArgumentNullException("tokens");
            }
            //SymbolTable.DeleteAllSymbols();
            _syntaxErrors = new List<Error>();
            _tokens = tokens;
            _i = 0;
            //var rootNode = Statements();
            if ( 0 < _syntaxErrors.Count )
            {
                throw new GrammarException(_syntaxErrors);
            }
            //return rootNode;
        }


        /// <summary>
        /// Gets the current token or null if there are no more tokens. 
        /// Adds the counter so the next caller gets the next token.
        /// </summary>
        /// <returns>The current token or null if there are no more tokens</returns>
        private Token NextToken()
        {
            return _i < _tokens.Count
                       ? _tokens[_i++]
                       : null;
        }


        /// <summary>
        /// Gets the current token or null if there are no more tokens. 
        /// Doesn't add the counter so the next caller gets the same token.
        /// </summary>
        /// <returns>The current token or null if there are no more tokens</returns>
        private Token CurrentToken()
        {
            return _i < _tokens.Count
                       ? _tokens[_i]
                       : null;
        }


        /// <summary>
        /// Checks the token for null reference and correct lexeme
        /// </summary>
        /// <param name="token">Current token</param>
        /// <param name="lexeme">Lexeme expected</param>
        /// <returns>True if ok</returns>
        public static bool CheckToken(Token token, string lexeme)
        {
            return token != null && token.Lexeme == lexeme;
        }


        /// <summary>
        /// Adds a syntax error to the list of errors
        /// </summary>
        /// <param name="token">Current token</param>
        /// <param name="expectedLexeme">Expected lexeme</param>
        /// <param name="useDefault">Use default message: "Was expecting {expectedLexeme}."</param>
        private void AddSyntaxError(Token token, string expectedLexeme, bool useDefault = true)
        {
            if ( token == null )
            {
                _syntaxErrors.Add(
                    new Error(Scanner.Lines[Scanner.Lines.Count - 1], Scanner.Lines.Count - 1, Scanner.Lines[Scanner.Lines.Count - 1].Length, String.Format("Unexpected end of file, was expecting {0}.", expectedLexeme)));
                return;
            }
            if ( useDefault )
            {
                _syntaxErrors.Add(
                    new Error(Scanner.Lines[token.Line - 1], token.Line, token.StartColumn, String.Format("Was expecting {0}.", expectedLexeme)));
            }
            else
            {
                _syntaxErrors.Add(
                    new Error(Scanner.Lines[token.Line - 1], token.Line, token.StartColumn, expectedLexeme));

            }
        }
    }
}
