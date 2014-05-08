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
    int i2;
    i2=2;
    while((i2 > (0 + 0+ 1 - 2)) == true) {
      i2 = i2 - 1;
      if (i2 == 0) {
         System.out.println (i2);
      }
      else 
         System.out.println (i2);
    }
    {
      i = (0);
      i2 = i - i2;
    }
    int[] array = new int[3];
    array[0] = 1;
    array[1] = 2;
    array[2] = 3;
    int sum = 0;
    int index = 0;
    while (index < array.length) {
      sum = sum + array[index];
      index = index + 1;
    }
    if (sum >= index) {
      System.out.println(i + (i2 - index) * sum);
    }
  }
}
class Fac {
boolean test = false;
int print = 0;

  public int ComputeFac (int num) {
    if (test)
       System.out.println (((print)));
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
