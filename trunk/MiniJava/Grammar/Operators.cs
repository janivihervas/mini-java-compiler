using System.Collections.Generic;

namespace Grammar
{
    /// @author Jani Viherväs
    /// @version 5.5.2014
    /// 
    /// <summary>
    /// Operators of the grammar
    /// </summary>
    public class Operators
    {
        //TODO: fix

        #region Arithmetic operators

        /// <summary>
        /// Plus operator
        /// </summary>
        public const string Plus = "+";

        /// <summary>
        /// Minus operator
        /// </summary>
        public const string Minus = "-";

        /// <summary>
        /// Divide operator
        /// </summary>
        public const string Divide = "/";

        /// <summary>
        /// Multiply operator
        /// </summary>
        public const string Multiply = "*";

        #endregion

        #region Comparison

        /// <summary>
        /// Lesser than operator
        /// </summary>
        public const string LesserThan = "<";

        /// <summary>
        /// Lesser or equal than operator
        /// </summary>
        public const string LesserOrEqualThan = "<=";

        /// <summary>
        /// Greater than operator
        /// </summary>
        public const string GreaterThan = ">";

        /// <summary>
        /// Greater or equal than operator
        /// </summary>
        public const string GreaterOrEqualThan = ">=";

        /// <summary>
        /// Equal operator
        /// </summary>
        public const string Equal = "=";

        /// <summary>
        /// Not equal operator
        /// </summary>
        public const string NotEqual = "!=";

        #endregion

        #region Logical

        /// <summary>
        /// Logical and operator
        /// </summary>
        public const string And = "&";

        /// <summary>
        /// Logical not operator
        /// </summary>
        public const string Not = "!";

        #endregion

        /// <summary>
        /// Left parenthesis
        /// </summary>
        public const string ParenthesisLeft = "(";

        /// <summary>
        /// Right parenthesis
        /// </summary>
        public const string ParenthesisRight = ")";


        /// <summary>
        /// Returns all the operators in an order that doesn't mess up the longest matching rule, i.e. ">=" is before ">".
        /// </summary>
        /// <returns>All the operators</returns>
        public static IEnumerable<string> GetOperators()
        {
            return new[]
                       {
                           Plus, Minus, Multiply, Divide,
                           ParenthesisLeft, ParenthesisRight,
                           GreaterOrEqualThan, LesserOrEqualThan,
                           GreaterThan, LesserThan, NotEqual, Equal,
                           And, Not,
                       };
        }
    }
}
