using ConsoleCalc;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ExpressionUnitTests
    {
        [Test]
        public void GetValue_Addition_Integer()
        {
            var expressionUnit = new ExpressionUnit(new ValueUnit(1), Operation.Plus, new ValueUnit(2));
            var result = expressionUnit.GetResult();
            Assert.AreEqual(3, result);
        }

        [Test]
        public void GetValue_Subtraction_Integer()
        {
            var expressionUnit = new ExpressionUnit(new ValueUnit(1), Operation.Minus, new ValueUnit(2));
            var result = expressionUnit.GetResult();
            Assert.AreEqual(-1, result);
        }

        [Test]
        public void GetValue_Multiplication_Integer()
        {
            var expressionUnit = new ExpressionUnit(new ValueUnit(2), Operation.Multiply, new ValueUnit(2));
            var result = expressionUnit.GetResult();
            Assert.AreEqual(4, result);
        }

        [Test]
        public void GetValue_Division_Integer()
        {
            var expressionUnit = new ExpressionUnit(new ValueUnit(6), Operation.Div, new ValueUnit(3));
            var result = expressionUnit.GetResult();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void GetValue_DivRemainder_Integer()
        {
            var expressionUnit = new ExpressionUnit(new ValueUnit(6), Operation.DivRem, new ValueUnit(4));
            var result = expressionUnit.GetResult();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void GetValue_Addition_Decimal()
        {
            var expressionUnit = new ExpressionUnit(new ValueUnit(1.1M), Operation.Plus, new ValueUnit(1.2M));
            var result = expressionUnit.GetResult();
            Assert.AreEqual(2.3, result);
        }

        [Test]
        public void GetValue_Subtraction_Decimal()
        {
            var expressionUnit = new ExpressionUnit(new ValueUnit(3.3M), Operation.Minus, new ValueUnit(1.1M));
            var result = expressionUnit.GetResult();
            Assert.AreEqual(2.2, result);
        }

        [Test]
        public void GetValue_Multiplication_Decimal()
        {
            var expressionUnit = new ExpressionUnit(new ValueUnit(1.5M), Operation.Multiply, new ValueUnit(3.3M));
            var result = expressionUnit.GetResult();
            Assert.AreEqual(4.95, result);
        }

        [Test]
        public void GetValue_Division_Decimal()
        {
            var expressionUnit = new ExpressionUnit(new ValueUnit(8.2M), Operation.Div, new ValueUnit(1.6M));
            var result = expressionUnit.GetResult();
            Assert.AreEqual(5.125, result);
        }

        [Test]
        public void GetValue_DivRemainder_Decimal()
        {
            var expressionUnit = new ExpressionUnit(new ValueUnit(7.8M), Operation.DivRem, new ValueUnit(5.2M));
            var result = expressionUnit.GetResult();
            Assert.AreEqual(2.6, result);
        }
    }
}