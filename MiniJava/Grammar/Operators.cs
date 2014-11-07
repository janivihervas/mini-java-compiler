using System.Collections.Generic;

namespace Grammar
{
    /// @author Jani Viherväs
    /// @version 5.5.2014
    /// 
    /// <summary>
    /// Operators of the grammar
    /// </summary>
    public struct Operators
    {
        #region Arithmetic operators

        /// <summary>
        /// +
        /// </summary>
        public const string Plus = "+";

        /// <summary>
        /// -
        /// </summary>
        public const string Minus = "-";

        /// <summary>
        /// /
        /// </summary>
        public const string Divide = "/";

        /// <summary>
        /// *
        /// </summary>
        public const string Multiply = "*";

        /// <summary>
        /// %
        /// </summary>
        public const string Remainder = "%";

        #endregion

        #region Comparison

        /// <summary>
        /// &lt;
        /// </summary>
        public const string LesserThan = "<";

        /// <summary>
        /// &lt;=
        /// </summary>
        public const string LesserOrEqualThan = "<=";

        /// <summary>
        /// >
        /// </summary>
        public const string GreaterThan = ">";

        /// <summary>
        /// >=
        /// </summary>
        public const string GreaterOrEqualThan = ">=";

        /// <summary>
        /// ==
        /// </summary>
        public const string Equal = "==";

        /// <summary>
        /// !=
        /// </summary>
        public const string NotEqual = "!=";

        #endregion

        #region Logical

        /// <summary>
        /// &&
        /// </summary>
        public const string And = "&&";

        /// <summary>
        /// ||
        /// </summary>
        public const string Or = "||";

        /// <summary>
        /// !
        /// </summary>
        public const string Not = "!";

        #endregion

        /// <summary>
        /// (
        /// </summary>
        public const string ParenthesisLeft = "(";

        /// <summary>
        /// )
        /// </summary>
        public const string ParenthesisRight = ")";

        /// <summary>
        /// {
        /// </summary>
        public const string BraceLeft = "{";

        /// <summary>
        /// }
        /// </summary>
        public const string BraceRight = "}";

        /// <summary>
        /// [
        /// </summary>
        public const string BracketLeft = "[";

        /// <summary>
        /// ]
        /// </summary>
        public const string BracketRight = "]";

        /// <summary>
        /// .
        /// </summary>
        public const string Dot = ".";

        /// <summary>
        /// ,
        /// </summary>
        public const string Comma = ",";


        /// <summary>
        /// Returns all the operators in an order that doesn't mess up the longest matching rule, i.e. ">=" is before ">".
        /// </summary>
        /// <returns>All the operators</returns>
        public static IEnumerable<string> GetOperators()
        {
            return new[]
                       {
                           And,
                           BraceLeft,
                           BraceRight,
                           BracketLeft,
                           BracketRight,
                           Comma,
                           Divide,
                           Dot,
                           Equal,
                           GreaterOrEqualThan,
                           GreaterThan,
                           LesserOrEqualThan,
                           LesserThan,
                           Minus,
                           Multiply,
                           NotEqual,
                           Not,
                           Or,
                           ParenthesisLeft,
                           ParenthesisRight,
                           Plus,
                           Remainder
                       };
        }


        /// <summary>
        /// Gets all the binary operators
        /// </summary>
        /// <returns>The binary operators</returns>
        public static string[] GetBinaryOperators()
        {
            return new[]
                       {
                           And,
                           Or,
                           LesserThan,
                           LesserOrEqualThan,
                           GreaterThan,
                           GreaterOrEqualThan,
                           Equal,
                           Plus,
                           Minus,
                           Multiply,
                           Divide,
                           Remainder
                       };
        } 
    }
}
