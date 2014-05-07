using System;
using System.Collections.Generic;
using System.Linq;
using FrontEnd;
using Grammar;
using Helper;
using NUnit.Framework;

namespace UnitTests
{
    /// @author Jani Viherväs
    /// @version 5.5.2014
    /// 
    /// <summary>
    /// Tests for the scanner. As the scanner as basicly the same as in the Mini-PL interpreter,
    /// I don't bother to test any more cases as the scanner was thoroughly tested previously
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
        public void TestSampleProgramIsTokenized()
        {
            const string s = @"
class Factorial {
  public static void main () {
    System.out.println (new Fac ().ComputeFac (10));
  }
}
class Fac {
  public int ComputeFac (int num) {
    assert (num > -1);
    int num_aux;
    if (num == 0)
      num_aux = 1;
    else 
      num_aux = num * this.ComputeFac (num-1);
    return num_aux;
  }
}";
            var lines = StringOperations.SplitAtLineBreaks(s);
            var tokens = _scanner.Tokenize(lines);

            Assert.AreEqual(80, tokens.Count);

            var expected = new List<string>
                               {
                                    ReservedKeywords.Class,
                                    "Factorial",
                                    Operators.BraceLeft,
                                    ReservedKeywords.Public,
                                    ReservedKeywords.Static,
                                    Types.Void,
                                    ReservedKeywords.Main,
                                    Operators.ParenthesisLeft,
                                    Operators.ParenthesisRight,
                                    Operators.BraceLeft,
                                    ReservedKeywords.System,
                                    Operators.Dot,
                                    ReservedKeywords.Out,
                                    Operators.Dot,
                                    ReservedKeywords.Println,
                                    Operators.ParenthesisLeft,
                                    ReservedKeywords.New,
                                    "Fac",
                                    Operators.ParenthesisLeft,
                                    Operators.ParenthesisRight,
                                    Operators.Dot,
                                    "ComputeFac",
                                    Operators.ParenthesisLeft,
                                    "10",
                                    Operators.ParenthesisRight,
                                    Operators.ParenthesisRight,
                                    ReservedKeywords.Semicolon,
                                    Operators.BraceRight,
                                    Operators.BraceRight,
                                    ReservedKeywords.Class,
                                    "Fac",
                                    Operators.BraceLeft,
                                    ReservedKeywords.Public,
                                    Types.Int,
                                    "ComputeFac",
                                    Operators.ParenthesisLeft,
                                    Types.Int,
                                    "num",
                                    Operators.ParenthesisRight,
                                    Operators.BraceLeft,
                                    ReservedKeywords.Assert,
                                    Operators.ParenthesisLeft,
                                    "num",
                                    Operators.GreaterThan,
                                    Operators.Minus,
                                    "1",
                                    Operators.ParenthesisRight,
                                    ReservedKeywords.Semicolon,
                                    Types.Int,
                                    "num_aux",
                                    ReservedKeywords.Semicolon,
                                    ReservedKeywords.If,
                                    Operators.ParenthesisLeft,
                                    "num",
                                    Operators.Equal,
                                    "0",
                                    Operators.ParenthesisRight,
                                    "num_aux",
                                    ReservedKeywords.Assignment,
                                    "1",
                                    ReservedKeywords.Semicolon,
                                    ReservedKeywords.Else,
                                    "num_aux",
                                    ReservedKeywords.Assignment,
                                    "num",
                                    Operators.Multiply,
                                    ReservedKeywords.This,
                                    Operators.Dot,
                                    "ComputeFac",
                                    Operators.ParenthesisLeft,
                                    "num",
                                    Operators.Minus,
                                    "1",
                                    Operators.ParenthesisRight,
                                    ReservedKeywords.Semicolon,
                                    ReservedKeywords.Return,
                                    "num_aux",
                                    ReservedKeywords.Semicolon,
                                    Operators.BraceRight,
                                    Operators.BraceRight
                               };

            TestTokensInOrder(expected, tokens);

            Assert.IsFalse(tokens[6] is TokenIdentifier);

            Assert.IsTrue(tokens[1] is TokenIdentifier);
            Assert.IsTrue(tokens[17] is TokenIdentifier);
            Assert.IsTrue(tokens[21] is TokenIdentifier);
            Assert.IsTrue(tokens[30] is TokenIdentifier);
            Assert.IsTrue(tokens[34] is TokenIdentifier);
            Assert.IsTrue(tokens[37] is TokenIdentifier);
            Assert.IsTrue(tokens[42] is TokenIdentifier);
            Assert.IsTrue(tokens[49] is TokenIdentifier);
            Assert.IsTrue(tokens[53] is TokenIdentifier);
            Assert.IsTrue(tokens[57] is TokenIdentifier);
            Assert.IsTrue(tokens[62] is TokenIdentifier);
            Assert.IsTrue(tokens[64] is TokenIdentifier);
            Assert.IsTrue(tokens[68] is TokenIdentifier);
            Assert.IsTrue(tokens[70] is TokenIdentifier);
            Assert.IsTrue(tokens[76] is TokenIdentifier);
        }

        [Test]
        public void TestEveryReservedKeyword()
        {
            var fields = typeof(ReservedKeywords).GetFields();
            var keywords = fields.Select(field => field.GetValue(null) as string).ToList();

            var tokens = _scanner.Tokenize(keywords);

            Assert.AreEqual(keywords.Count, tokens.Count);

            for ( var i = 0; i < tokens.Count; i++ )
            {
                Assert.AreEqual(keywords[i], tokens[i].Lexeme);
                Assert.IsFalse(tokens[i] is TokenIdentifier);
                Assert.IsFalse(tokens[i] is TokenTerminal<int>);
                Assert.IsFalse(tokens[i] is TokenTerminal<bool>);
            }
        }

        [Test]
        public void TestEveryOperator()
        {
            var fields = typeof(ReservedKeywords).GetFields();
            var operators = fields.Select(field => field.GetValue(null) as string).ToList();
            var tokens = _scanner.Tokenize(operators);

            Assert.AreEqual(operators.Count, tokens.Count);

            for ( var i = 0; i < tokens.Count; i++ )
            {
                Assert.AreEqual(operators[i], tokens[i].Lexeme);
                Assert.IsFalse(tokens[i] is TokenIdentifier);
                Assert.IsFalse(tokens[i] is TokenTerminal<int>);
                Assert.IsFalse(tokens[i] is TokenTerminal<bool>);
            }
        }

        [Test]
        public void TestEveryType()
        {
            var fields = typeof(ReservedKeywords).GetFields();
            var types = fields.Select(field => field.GetValue(null) as string).ToList();
            var tokens = _scanner.Tokenize(types);

            Assert.AreEqual(types.Count, tokens.Count);

            for ( var i = 0; i < tokens.Count; i++ )
            {
                Assert.AreEqual(types[i], tokens[i].Lexeme);
                Assert.IsFalse(tokens[i] is TokenIdentifier);
                Assert.IsFalse(tokens[i] is TokenTerminal<int>);
                Assert.IsFalse(tokens[i] is TokenTerminal<bool>);
            }
        }

        [Test]
        public void TestScannerCanTokenizeSimpleTypes()
        {
            var lines = new List<string>
                            {
                                "public void test() {",
                                "int x = 1;",
                                "boolean y = true;",
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
                                  x.Lexeme == Types.Boolean &&
                                  x.Line == 3 &&
                                  x.StartColumn == 1
                              ));
        }

        private void TestTokensInOrder(List<string> expected, List<Token> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count, "List sizes were different.");

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i].Lexeme);
            }
        }


    }
}
