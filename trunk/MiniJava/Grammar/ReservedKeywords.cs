using System.Collections.Generic;

namespace Grammar
{
    /// @author Jani Viherväs
    /// @version 5.5.2014
    /// 
    /// <summary>
    /// Reserved keywords of the grammar
    /// </summary>
    public struct ReservedKeywords
    {
        /// <summary>
        /// "assert"
        /// </summary>
        public const string Assert = "assert";

        /// <summary>
        /// "="
        /// </summary>
        public const string Assignment = "=";
        
        /// <summary>
        /// "class"
        /// </summary>
        public const string Class = "class";

        /// <summary>
        /// "else"
        /// </summary>
        public const string Else = "else";

        /// <summary>
        /// extends
        /// </summary>
        public const string Extends = "extends";

        /// <summary>
        /// "if"
        /// </summary>
        public const string If = "if";

        /// <summary>
        /// length
        /// </summary>
        public const string Length = "length";

        /// <summary>
        /// "main"
        /// </summary>
        public const string Main = "main";

        /// <summary>
        /// "new"
        /// </summary>
        public const string New = "new";

        /// <summary>
        /// "out"
        /// </summary>
        public const string Out = "out";

        /// <summary>
        /// "println"
        /// </summary>
        public const string Println = "println";

        /// <summary>
        /// "public"
        /// </summary>
        public const string Public = "public";

        /// <summary>
        /// "return"
        /// </summary>
        public const string Return = "return";

        /// <summary>
        /// ";"
        /// </summary>
        public const string Semicolon = ";";

        /// <summary>
        /// "static"
        /// </summary>
        public const string Static = "static";

        /// <summary>
        /// "System"
        /// </summary>
        public const string System = "System";

        /// <summary>
        /// "this"
        /// </summary>
        public const string This = "this";

        /// <summary>
        /// while
        /// </summary>
        public const string While = "while";

        /// <summary>
        /// Returns all the reserved keywords in an order from shortest to longest length
        /// </summary>
        /// <returns>All the reserved keywords sorted</returns>
        public static IEnumerable<string> GetReservedKeywords()
        {
            return new[]
                       {
                           Semicolon,
                           Assignment,
                           If,
                           New,
                           Out,
                           Else,
                           Main,
                           This,
                           Class,
                           While,
                           Assert,
                           Length,
                           Public,
                           Return,
                           Static,
                           System,
                           Extends,
                           Println
                       };
        }
    }
}
