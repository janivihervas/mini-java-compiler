using System;
using System.Collections.Generic;
using System.Linq;
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
        // TODO: add follow sets for every skip
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
            Program();
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
        /// <param name="token">Token to check</param>
        /// <param name="lexeme">Lexeme expected</param>
        private static bool CheckToken(Token token, params string[] lexeme)
        {
            return token != null && lexeme.Any(s => token.Lexeme == s);
        }

        /// <summary>
        /// Checks the token for null reference and correct lexeme
        /// </summary>
        /// <param name="lexeme">Lexeme expected</param>
        /// <param name="followSet">Follow set to skip to </param>
        private void CheckTokenAndSkip(string lexeme, params string[] followSet)
        {
            var token = NextToken();
            if (CheckToken(token, lexeme)) return;
            AddSyntaxError(token, lexeme);
            SkipTokens(followSet);
        }

        /// <summary>
        /// Skips tokens until one with the lexeme that is in the follow set is found
        /// </summary>
        /// <param name="followSet">Follow set</param>
        private void SkipTokens(params string[] followSet)
        {
            if (followSet.Length < 1)
            {
                return;
            }
            _i--;
            while ( _i < _tokens.Count && !followSet.Contains(CurrentToken().Lexeme) )
            {
                _i++;
            }
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

        /// <summary>
        /// Check if the token is type token
        /// </summary>
        /// <param name="token">Token to check</param>
        /// <returns>True if the token is a type token</returns>
        private bool IsTypeToken(Token token)
        {
            if ( IsSimpleTypeToken(token) )
            {
                return true;
            }
            if (token is TokenIdentifier)
            {
                return IsTypeIdentifier(token.Lexeme);
            }
            return false;
        }

        private bool IsSimpleTypeToken(Token token)
        {
            return CheckToken(token, Types.Int, Types.Boolean, Types.Void);
        }

        private bool IsTypeIdentifier(string identifier)
        {
            // TODO: implement
            return true;
        }


        private void Program()
        {
            MainClass();
            while (_i < _tokens.Count)
            {
                ClassDeclaration();
            }
        }

        private void MainClass()
        {
            CheckTokenAndSkip(ReservedKeywords.Class);
            NewIdentifier();
            CheckTokenAndSkip(Operators.BraceLeft);
            CheckTokenAndSkip(ReservedKeywords.Public);
            CheckTokenAndSkip(ReservedKeywords.Static);
            CheckTokenAndSkip(Types.Void);
            CheckTokenAndSkip(ReservedKeywords.Main);
            CheckTokenAndSkip(Operators.ParenthesisLeft);
            CheckTokenAndSkip(Operators.ParenthesisRight);
            CheckTokenAndSkip(Operators.BraceLeft);
            while ( !CheckToken(CurrentToken(), Operators.BraceRight) )
            {
                Statement();
            }
            NextToken();
            CheckTokenAndSkip(Operators.BraceRight);
        }

        private void ClassDeclaration()
        {
            CheckTokenAndSkip(ReservedKeywords.Class);
            NewIdentifier();
            if (CheckToken(CurrentToken(), ReservedKeywords.Extends))
            {
                NextToken();
                Identifier();
            }
            CheckTokenAndSkip(Operators.BraceLeft);
            while ( !CheckToken(CurrentToken(), Operators.BraceRight) )
            {
                Declaration();
            }
            NextToken();
        }

        private void Declaration()
        {
            if (CheckToken(CurrentToken(), ReservedKeywords.Public))
            {
                MethodDeclaration();
            }
            else
            {
                VariableDeclaration();
            }
        }

        private void MethodDeclaration()
        {
            CheckTokenAndSkip(ReservedKeywords.Public);
            TypeProduction();
            NewIdentifier();
            CheckTokenAndSkip(Operators.ParenthesisLeft);
            if (!CheckToken(CurrentToken(), Operators.ParenthesisRight))
            {
                Formals();
            }
            CheckTokenAndSkip(Operators.ParenthesisRight);
            CheckTokenAndSkip(Operators.BraceLeft);
            while ( !CheckToken(CurrentToken(), Operators.BraceRight) )
            {
                Statement();
            }
            NextToken();
        }

        private void VariableDeclaration()
        {
            TypeProduction();
            NewIdentifier();
            VariableAssignment();
            CheckTokenAndSkip(ReservedKeywords.Semicolon);
        }

        private void VariableAssignment()
        {
            if (CheckToken(CurrentToken(), ReservedKeywords.Semicolon)) // epsilon
            {
                return;
            }
            CheckTokenAndSkip(ReservedKeywords.Assignment);
            Expr();
        }

        private void Formals()
        {
            do
            {
                TypeProduction();
                NewIdentifier();
                if (!CheckToken(CurrentToken(), Operators.ParenthesisRight))
                {
                    CheckTokenAndSkip(Operators.Comma);
                }
            } while (!CheckToken(CurrentToken(), Operators.ParenthesisRight));
        }

        private void TypeProduction()
        {
            SimpleType();
            ArrayType();
        }

        private void SimpleType()
        {
            var token = NextToken();
            if (token == null)
            {
                AddSyntaxError(null, "int, boolean, void or identifier");
                return;
            }
            switch (token.Lexeme) 
            {
                case Types.Int:
                    {
                        return;
                    }
                case Types.Boolean:
                    {
                        return;
                    }
                case Types.Void:
                    {
                        return;
                    }
            }
            _i--;
            TypeIdentifier();
        }

        private void ArrayType()
        {
            if (!CheckToken(CurrentToken(), Operators.BracketLeft)) return; // epsilon
            NextToken();
            CheckTokenAndSkip(Operators.BracketRight);
        }

        private void TypeIdentifier()
        {
            Identifier();
        }

        private void Statement()
        {
            var token = NextToken();
            if ( token == null )
            {
                AddSyntaxError(null, "assert, variable declaration or assignment, nested statements, if, while, print, return or method invocation statement");
                return;
            }
            switch (token.Lexeme)
            {
                case ReservedKeywords.Assert:
                    {
                        CheckTokenAndSkip(Operators.ParenthesisLeft);
                        Expr();
                        CheckTokenAndSkip(Operators.ParenthesisRight);
                        CheckTokenAndSkip(ReservedKeywords.Semicolon);
                        return;
                    }
                case Operators.BraceLeft:
                    {
                        //_i--;
                        while (!CheckToken(CurrentToken(), Operators.BraceRight))
                        {
                            Statement();
                        }
                        NextToken();
                        return;
                    }
                case ReservedKeywords.If:
                    {
                        CheckTokenAndSkip(Operators.ParenthesisLeft);
                        Expr();
                        CheckTokenAndSkip(Operators.ParenthesisRight);
                        Statement();
                        Else();
                        return;
                    }
                case ReservedKeywords.While:
                    {
                        CheckTokenAndSkip(Operators.ParenthesisLeft);
                        Expr();
                        CheckTokenAndSkip(Operators.ParenthesisRight);
                        Statement();
                        return;
                    }
                case ReservedKeywords.System:
                    {
                        CheckTokenAndSkip(Operators.Dot);
                        CheckTokenAndSkip(ReservedKeywords.Out);
                        CheckTokenAndSkip(Operators.Dot);
                        CheckTokenAndSkip(ReservedKeywords.Println);
                        CheckTokenAndSkip(Operators.ParenthesisLeft);
                        Expr();
                        CheckTokenAndSkip(Operators.ParenthesisRight);
                        CheckTokenAndSkip(ReservedKeywords.Semicolon, Operators.BraceRight, ReservedKeywords.Else); // TODO: skip to next statement
                        return;
                    }
                case ReservedKeywords.Return: 
                    {
                        Expr();
                        CheckTokenAndSkip(ReservedKeywords.Semicolon);
                        if (!CheckToken(CurrentToken(), Operators.BraceRight))
                        {
                            // TODO: add warning because of unreachable code after return
                            SkipTokens(Operators.BraceRight);
                        }
                        return;
                    }
            }
            if ( token is TokenIdentifier && CheckToken(CurrentToken(), ReservedKeywords.Assignment, Operators.BracketLeft) )
            {
                _i--;
                Identifier();
                if (CheckToken(CurrentToken(), Operators.BracketLeft))
                {
                    NextToken();
                    Expr();
                    CheckTokenAndSkip(Operators.BracketRight);
                }
                //NextToken(); // CurrentToken.lexeme === "="
                CheckTokenAndSkip(ReservedKeywords.Assignment);
                Expr();
                CheckTokenAndSkip(ReservedKeywords.Semicolon);
                return;
            }
            if ( IsTypeToken(token) )
            {
                _i--;
                LocalVariableDeclaration();
                return;
            }
            _i--;
            MethodInvocation();
            CheckTokenAndSkip(ReservedKeywords.Semicolon);
        }

        private void Else()
        {
            if (!CheckToken(CurrentToken(), ReservedKeywords.Else)) return; // epsilon
            NextToken();
            Statement();
        }

        private void LocalVariableDeclaration()
        {
            VariableDeclaration();
        }

        private void MethodInvocation()
        {
            Expr1();
            CheckTokenAndSkip(Operators.Dot);
            MethodTail();
        }

        private void MethodTail()
        {
            if (CheckToken(CurrentToken(), ReservedKeywords.Length))
            {
                NextToken();
                return;
            }
            Identifier();
            CheckTokenAndSkip(Operators.ParenthesisLeft);
            while ( !CheckToken(CurrentToken(), Operators.ParenthesisRight) )
            {
                Expr();
                if ( !CheckToken(CurrentToken(), Operators.ParenthesisRight) )
                {
                    CheckTokenAndSkip(Operators.Comma);
                }
            }
            NextToken();

        }

        private void Expr()
        {
            Expr1();
            Expr2();
        }

        private void Expr1()
        {
            var token = NextToken();
            if (token == null)
            {
                AddSyntaxError(null, "new, !, (, this, boolean or integer value or method invocation");
                return;
            }
            switch (token.Lexeme)
            {
                case ReservedKeywords.New:
                    {
                        New();
                        return;
                    }
                case Operators.Not:
                    {
                        Expr();
                        return;
                    }
                case Operators.Minus:
                    {
                        Expr();
                        return;
                    }
                case Operators.ParenthesisLeft:
                    {
                        Expr();
                        CheckTokenAndSkip(Operators.ParenthesisRight);
                        return;
                    }
                case ReservedKeywords.This:
                    {
                        return;
                    }
            }
            if (token is TokenTerminal<int>)
            {
                return;
            }
            if (token is TokenTerminal<bool>)
            {
                return;
            }
            if (token is TokenIdentifier)
            {
                _i--;
                Identifier();
            }
        }

        private void Expr2()
        {
            var token = NextToken();
            if (CheckToken(token, Operators.ParenthesisRight, ReservedKeywords.Semicolon, Operators.BracketRight)) // epsilon
            {
                _i--;
                return;
            }
            if (token == null)
            {
                AddSyntaxError(null, "new, !, (, this, boolean or integer value or method invocation");
                return;
            }

            switch (token.Lexeme)
            {
                case Operators.BracketLeft:
                    {
                        Expr();
                        CheckTokenAndSkip(Operators.BracketRight);
                        return;
                    }
                case Operators.Dot:
                    {
                        MethodTail();
                        return;
                    }
            }
            _i--;
            BinaryOperator();
        }

        private void New()
        {
            if (IsSimpleTypeToken(CurrentToken()))
            {
                SimpleType();
                CheckTokenAndSkip(Operators.BracketLeft);
                Expr();
                CheckTokenAndSkip(Operators.BracketRight);
                return;
            }
            TypeIdentifier();
            CheckTokenAndSkip(Operators.ParenthesisLeft);
            CheckTokenAndSkip(Operators.ParenthesisRight);
        }

        private void BinaryOperator()
        {
            var token = NextToken();
            if ( !CheckToken(token, Operators.GetBinaryOperators()) )
            {
                AddSyntaxError(token, "binary operator");
                return;
            }
            Expr();
        }

        /// <summary>
        /// Adds a new identifier to the symbol table
        /// </summary>
        private void NewIdentifier()
        {
            var token = NextToken() as TokenIdentifier;
            if ( token == null )
            {
                AddSyntaxError(null, "identifier");
            }
        }

        /// <summary>
        /// Finds an identifier from the symbol table
        /// </summary>
        private void Identifier()
        {
            var token = NextToken() as TokenIdentifier;
            if (token == null)
            {
                AddSyntaxError(null, "identifier");
            }
        }

    }
}
