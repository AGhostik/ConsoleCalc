using ConsoleCalc;
using ConsoleCalc.Models;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ValueUnitTests
    {
        [Test]
        public void TryParse_Integer()
        {
            var input = "5";
            var result = ValueUnit.TryParse(input, out var valueUnit);
            Assert.IsTrue(result);
            Assert.AreEqual(5, valueUnit.GetResult());
        }

        [Test]
        public void TryParse_NegativeInteger()
        {
            var input = "-5";
            var result = ValueUnit.TryParse(input, out var valueUnit);
            Assert.IsTrue(result);
            Assert.AreEqual(-5, valueUnit.GetResult());
        }

        [Test]
        public void TryParse_Decimal()
        {
            var input = "1.3";
            var result = ValueUnit.TryParse(input, out var valueUnit);
            Assert.IsTrue(result);
            Assert.AreEqual(1.3, valueUnit.GetResult());
        }

        [Test]
        public void TryParse_NegativeDecimal()
        {
            var input = "-1.3";
            var result = ValueUnit.TryParse(input, out var valueUnit);
            Assert.IsTrue(result);
            Assert.AreEqual(-1.3, valueUnit.GetResult());
        }
    }
}