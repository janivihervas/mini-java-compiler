using System.Collections.Generic;

namespace Grammar
{
    /// @author Jani Viherväs
    /// @version 5.5.2014
    /// 
    /// <summary>
    /// Types in the grammar
    /// </summary>
    public class Types
    {
        /// <summary>
        /// Boolean type
        /// </summary>
        public const string Bool = "bool";

        /// <summary>
        /// Integer type
        /// </summary>
        public const string Int = "int";

        /// <summary>
        /// String type
        /// </summary>
        public const string Void = "void";

        /// <summary>
        /// Returns all the types in an order from shortest length to longest.
        /// </summary>
        /// <returns>All the types</returns>
        public static IEnumerable<string> GetTypes()
        {
            return new[] { Int, Bool, Void };
        }
    }
}
