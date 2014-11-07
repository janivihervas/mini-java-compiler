using System;
using System.Collections.Generic;
using System.Linq;
using Grammar;

namespace FrontEnd
{
    /// @author Jani Viherväs
    /// @version 5.5.2014
    /// 
    /// <summary>
    /// Scanner to tokenize source code
    /// </summary>
    public class Scanner
    {
        //TODO: fix

        /// <summary>
        /// Current column
        /// </summary>
        private int _column;

        /// <summary>
        /// Current row
        /// </summary>
        private int _row;

        /// <summary>
        /// How many whitespaces where skipped. Used in combining consecutive error tokens 
        /// </summary>
        private int _whiteSpacesSkipped;

        /// <summary>
        /// Presivous token produced. Used in combining consecutive error tokens
        /// </summary>
        private Token _previousToken;

        /// <summary>
        /// Source code lines
        /// </summary>
        public static List<string> Lines;

        /// <summary>
        /// Produces tokens for the parser.
        /// </summary>
        /// <param name="lines">Source code</param>
        /// <returns>Tokens</returns>
        public List<Token> Tokenize(List<string> lines)
        {
            if ( lines == null )
            {
                throw new ArgumentNullException("lines");
            }
            Lines = lines;
            var tokens = new List<Token>();
            for ( _row = 0; _row < lines.Count; _row++ )
            {
                _column = 0;
                var line = lines[_row];
                while ( _column < line.Length )
                {
                    var stop = SkipWhiteSpace(line);
                    if ( stop )
                    {
                        break;
                    }
                    if ( _column < line.Length - 1 && line[_column] == '/' && line[_column + 1] == '*' ) // Multiline comment
                    {
                        SkipMultilineComment(lines);
                        continue;
                    }
                    if ( _column < line.Length - 1 && line[_column] == '/' && line[_column + 1] == '/' ) // line comment
                    {
                        break;
                    }

                    Token token;

                    if ( (token = CreateTypeToken(line)) != null )
                    {
                        tokens.Add(token);
                        _previousToken = token;
                        continue;
                    }
                    if ( (token = CreateOperatorToken(line)) != null )
                    {
                        tokens.Add(token);
                        _previousToken = token;
                        continue;
                    }
                    if ( (token = CreateReservedKeywordToken(line)) != null )
                    {
                        tokens.Add(token);
                        _previousToken = token;
                        continue;
                    }
                    if ( (token = CreateTerminalToken(line)) != null )
                    {
                        tokens.Add(token);
                        _previousToken = token;
                        continue;
                    }
                    if ( (token = CreateIdentifierToken(line)) != null )
                    {
                        tokens.Add(token);
                        _previousToken = token;
                        continue;
                    }
                    if ( (token = CreateErrorToken(line)) != null )
                    {
                        tokens.Add(token);
                        _previousToken = token;
                    }
                }
            }

            return tokens;
        }


        /// <summary>
        /// Skips multiline comments
        /// </summary>
        /// <param name="lines">Source code lines</param>
        private void SkipMultilineComment(List<string> lines)
        {
            if ( _column >= lines[_row].Length - 1 || lines[_row][_column] != '/' || lines[_row][_column + 1] != '*' )
            {
                return;
            }
            _column += 2;
            while ( _row < lines.Count )
            {
                while ( _column < lines[_row].Length )
                {
                    if ( _column < lines[_row].Length - 1 && lines[_row][_column] == '*' && lines[_row][_column + 1] == '/' )
                    {
                        _column += 2;
                        return;
                    }
                    _column++;
                }
                _column = 0;
                _row++;
            }
        }


        /// <summary>
        /// Skips whitespaces in the current line
        /// </summary>
        /// <param name="line">Current line</param>
        /// <returns>Boolean whether to stop the scanning of this line</returns>
        private bool SkipWhiteSpace(string line)
        {
            _whiteSpacesSkipped = 0;
            while ( _column < line.Length && Char.IsWhiteSpace(line[_column]) )
            {
                _column++;
                _whiteSpacesSkipped++;
                if ( _column == line.Length )
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Creates a token if it matches the given symbol
        /// </summary>
        /// <param name="line">Current line</param>
        /// <param name="symbol">Symbol to match</param>
        /// <returns>Created token or null</returns>
        private Token CreateToken(string line, string symbol)
        {
            // One can make this to work a lot faster, but this is nicer
            try
            {
                if ( line.Substring(_column, symbol.Length) == symbol )
                {
                    var token = new Token(_row, _column, symbol);
                    _column += symbol.Length;
                    return token;
                }
            }
            catch ( ArgumentOutOfRangeException )
            {
            }
            return null;
        }


        /// <summary>
        /// Creates a token if it matches the given symbols
        /// </summary>
        /// <param name="line">Current line</param>
        /// <param name="symbols">Symbols to match</param>
        /// <returns>Created token or null</returns>
        private Token CreateToken(string line, IEnumerable<string> symbols)
        {
            return symbols.Select(symbol => CreateToken(line, symbol)).FirstOrDefault(token => token != null);
        }


        /// <summary>
        /// Creates a new token for reserved keywords
        /// </summary>
        /// <param name="line">Current line</param>
        /// <returns>New token for reserved keywords or null</returns>
        private Token CreateReservedKeywordToken(string line)
        {
            return CreateToken(line, ReservedKeywords.GetReservedKeywords());
        }


        /// <summary>
        /// Creates a new token for variable types
        /// </summary>
        /// <param name="line">Current line</param>
        /// <returns>New token for variable types or null, if it wasn't a type token</returns>
        private Token CreateTypeToken(string line)
        {
            return CreateToken(line, Types.GetTypes());
        }


        /// <summary>
        /// Creates a new token for operators
        /// </summary>
        /// <param name="line">Current line</param>
        /// <returns>New token for operators or null, if it wasn't an operator token</returns>
        private Token CreateOperatorToken(string line)
        {
            return CreateToken(line, Operators.GetOperators());
        }


        /// <summary>
        /// Creates a new token for terminals
        /// </summary>
        /// <param name="line">Current line</param>
        /// <returns>New token for terminal or null</returns>
        private Token CreateTerminalToken(string line)
        {
            if ( Char.IsNumber(line[_column]) ) // int
            {
                var lenght = 1;
                while ( _column + lenght < line.Length && Char.IsNumber(line[_column + lenght]) )
                {
                    lenght++;
                }
                var subString = line.Substring(_column, lenght);
                try
                {
                    var value = int.Parse(subString);
                    var token = new TokenTerminal<int>(_row, _column, value);
                    _column += lenght;
                    return token;
                }
                catch ( FormatException )
                {
                    return null;
                }
            }
            if ( line[_column] == 't' || line[_column] == 'f' ) // boolean
            {
                var lenght = 1;
                while ( _column + lenght < line.Length && Char.IsLetter(line[_column + lenght]) )
                {
                    lenght++;
                }
                var subString = line.Substring(_column, lenght);
                try
                {
                    var value = bool.Parse(subString);
                    var token = new TokenTerminal<bool>(_row, _column, value);
                    _column += subString.Length;
                    return token;
                }
                catch ( FormatException )
                {
                    return null;
                }
            }
            return null;
        }


        /// <summary>
        /// Creates a new token for identifiers
        /// </summary>
        /// <param name="line">Current line</param>
        /// <returns>New token for identifier or null</returns>
        private TokenIdentifier CreateIdentifierToken(string line)
        {
            if ( !Char.IsLetter(line[_column]) ) // identifier must begin with a character
            {
                return null;
            }

            var lenght = 1;
            while ( _column + lenght < line.Length && (Char.IsLetterOrDigit(line[_column + lenght]) || line[_column + lenght] == '_') )
            {
                lenght++;
            }
            var subString = line.Substring(_column, lenght);
            var token = new TokenIdentifier(_row, _column, subString);
            _column += subString.Length;
            return token;
        }

        private TokenError CreateErrorToken(string line)
        {
            var i = _column;
            while ( i < line.Length )
            {
                if ( line[i] == ' ' || Char.IsLetterOrDigit(line[i]) )
                {
                    break;
                }
                i++;
            }
            var lexeme = line.Substring(_column, i - _column);
            var token = new TokenError(_row, _column, lexeme);
            _column += lexeme.Length;
            var previous = _previousToken as TokenError;
            if ( previous != null )
            {
                previous.CombineErrorTokens(token, _whiteSpacesSkipped);
                return null;
            }
            return token;
        }
    }
}
