using FrontEnd;
using Helper;
using NUnit.Framework;

namespace UnitTests
{
    /// @author Jani Viherväs
    /// @version 5.5.2014
    /// 
    /// <summary>
    /// Tests for the parser
    /// </summary>
    [TestFixture]
    public class ParserTests
    {
        private Scanner _scanner;
        private Parser _parser;

        [SetUp]
        protected void SetUp()
        {
            _scanner = new Scanner();
            _parser = new Parser();
        }


        [Test]
        public void TestSampleProgram()
        {
            const string source = @"
class Factorial {
  public static void main () {
    new Fac ().ComputeFac (10);
    int i = new Fac ().ComputeFac (10);
    System.out.println (i);
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
            var tokens = _scanner.Tokenize(StringOperations.SplitAtLineBreaks(source));
            Assert.DoesNotThrow(() => _parser.Parse(tokens));
        }
    }
}
