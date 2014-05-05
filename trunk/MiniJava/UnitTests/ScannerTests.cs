using System.Collections.Generic;
using FrontEnd;
using Grammar;
using NUnit.Framework;

namespace UnitTests
{
    /// @author Jani Viherväs
    /// @version 5.5.2014
    /// 
    /// <summary>
    /// Tests for the scanner
    /// </summary>
    [TestFixture]
    public class ScannerTests
    {
        private Scanner _scanner;

        [SetUp]
        protected void SetUp()
        {
            _scanner = new Scanner();
        }

        [Test]
        public void TestScannerCanTokenizeSimpleTypes()
        {
            var lines = new List<string>
                            {
                                "public void test() {",
                                "int x;",
                                "boolean y;",
                                "}"
                            };

            var tokens = _scanner.Tokenize(lines);

            Assert.IsTrue(3 <= tokens.Count);

            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == Types.Void &&
                                  x.Line == 1 &&
                                  x.StartColumn == 8
                              ));

            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == Types.Int &&
                                  x.Line == 2 &&
                                  x.StartColumn == 1
                              ));

            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == Types.Bool &&
                                  x.Line == 3 &&
                                  x.StartColumn == 1
                              ));
        }

    }
}
