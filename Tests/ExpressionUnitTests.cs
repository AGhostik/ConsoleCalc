using ConsoleCalc;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ExpressionUnitTests
    {
        [Test]
        public void GetValue_Addition_Decimal()
        {
            var expressionUnit = new ExpressionUnit(new ValueUnit(1.1M), Operation.Plus, new ValueUnit(1.2M));
            var result = expressionUnit.GetResult();
            Assert.AreEqual(2.3, result);
        }
        
        [Test]
        public void GetValue_Addition_Integer()
        {
            var expressionUnit = new ExpressionUnit(new ValueUnit(1), Operation.Plus, new ValueUnit(2));
            var result = expressionUnit.GetResult();
            Assert.AreEqual(3, result);
        }

        [Test]
        public void GetValue_Division_Decimal()
        {
            var expressionUnit = new ExpressionUnit(new ValueUnit(8.2M), Operation.Div, new ValueUnit(1.6M));
            var result = expressionUnit.GetResult();
            Assert.AreEqual(5.125, result);
        }

        [Test]
        public void GetValue_Division_Integer()
        {
            var expressionUnit = new ExpressionUnit(new ValueUnit(6), Operation.Div, new ValueUnit(3));
            var result = expressionUnit.GetResult();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void GetValue_DivRemainder_Decimal()
        {
            var expressionUnit = new ExpressionUnit(new ValueUnit(7.8M), Operation.DivRem, new ValueUnit(5.2M));
            var result = expressionUnit.GetResult();
            Assert.AreEqual(2.6, result);
        }

        [Test]
        public void GetValue_DivRemainder_Integer()
        {
            var expressionUnit = new ExpressionUnit(new ValueUnit(6), Operation.DivRem, new ValueUnit(4));
            var result = expressionUnit.GetResult();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void GetValue_Multiplication_Decimal()
        {
            var expressionUnit = new ExpressionUnit(new ValueUnit(1.5M), Operation.Multiply, new ValueUnit(3.3M));
            var result = expressionUnit.GetResult();
            Assert.AreEqual(4.95, result);
        }

        [Test]
        public void GetValue_Multiplication_Integer()
        {
            var expressionUnit = new ExpressionUnit(new ValueUnit(2), Operation.Multiply, new ValueUnit(2));
            var result = expressionUnit.GetResult();
            Assert.AreEqual(4, result);
        }

        [Test]
        public void GetValue_Subtraction_Decimal()
        {
            var expressionUnit = new ExpressionUnit(new ValueUnit(3.3M), Operation.Minus, new ValueUnit(1.1M));
            var result = expressionUnit.GetResult();
            Assert.AreEqual(2.2, result);
        }

        [Test]
        public void GetValue_Subtraction_Integer()
        {
            var expressionUnit = new ExpressionUnit(new ValueUnit(1), Operation.Minus, new ValueUnit(2));
            var result = expressionUnit.GetResult();
            Assert.AreEqual(-1, result);
        }

        [Test]
        public void TryParse_DivideOperation_ExpressionAndExpression()
        {
            var input = "(1 + 5) / (2 - 1)";
            var isSuccessfull = ExpressionUnit.TryParse(input, out var expressionUnit);
            Assert.IsTrue(isSuccessfull);
            Assert.AreEqual(Operation.Div, expressionUnit.Operation);
        }

        [Test]
        public void TryParse_DivideOperation_ExpressionAndValue()
        {
            var input = "(1 + 1) / 2";
            var isSuccessfull = ExpressionUnit.TryParse(input, out var expressionUnit);
            Assert.IsTrue(isSuccessfull);
            Assert.AreEqual(Operation.Div, expressionUnit.Operation);
        }

        [Test]
        public void TryParse_DivideOperation_ValueAndValue()
        {
            var input = "1 / 2";
            var isSuccessfull = ExpressionUnit.TryParse(input, out var expressionUnit);
            Assert.IsTrue(isSuccessfull);
            Assert.AreEqual(Operation.Div, expressionUnit.Operation);
        }

        [Test]
        public void TryParse_DivRemainderOperation_ExpressionAndExpression()
        {
            var input = "(1 + 5) % (2 - 1)";
            var isSuccessfull = ExpressionUnit.TryParse(input, out var expressionUnit);
            Assert.IsTrue(isSuccessfull);
            Assert.AreEqual(Operation.DivRem, expressionUnit.Operation);
        }

        [Test]
        public void TryParse_DivRemainderOperation_ExpressionAndValue()
        {
            var input = "(1 + 2) % 2";
            var isSuccessfull = ExpressionUnit.TryParse(input, out var expressionUnit);
            Assert.IsTrue(isSuccessfull);
            Assert.AreEqual(Operation.DivRem, expressionUnit.Operation);
        }

        [Test]
        public void TryParse_DivRemainderOperation_ValueAndValue()
        {
            var input = "1 % 2";
            var isSuccessfull = ExpressionUnit.TryParse(input, out var expressionUnit);
            Assert.IsTrue(isSuccessfull);
            Assert.AreEqual(Operation.DivRem, expressionUnit.Operation);
        }

        [Test]
        public void TryParse_ExpressionBothValueIsExpression_ExpressionAndExpression()
        {
            var input = "(1 + 5) * (2 - 1)";
            var isSuccessfull = ExpressionUnit.TryParse(input, out var expressionUnit);
            Assert.IsTrue(isSuccessfull);
            Assert.AreEqual(typeof(ExpressionUnit), expressionUnit.FirstValue.GetType());
            Assert.AreEqual(typeof(ExpressionUnit), expressionUnit.SecondValue.GetType());
        }

        [Test]
        public void TryParse_ExpressionBothValuesIsValueUnit_ValueAndValue()
        {
            var input = "1 + 2";
            var isSuccessfull = ExpressionUnit.TryParse(input, out var expressionUnit);
            Assert.IsTrue(isSuccessfull);
            Assert.AreEqual(typeof(ValueUnit), expressionUnit.FirstValue.GetType());
            Assert.AreEqual(typeof(ValueUnit), expressionUnit.SecondValue.GetType());
        }

        [Test]
        public void TryParse_ExpressionBothValuesIsValueUnit_ValueAndValue_Negative()
        {
            var input = "-1 + -2";
            var isSuccessfull = ExpressionUnit.TryParse(input, out var expressionUnit);
            Assert.IsTrue(isSuccessfull);
            Assert.AreEqual(typeof(ValueUnit), expressionUnit.FirstValue.GetType());
            Assert.AreEqual(typeof(ValueUnit), expressionUnit.SecondValue.GetType());
        }
        
        [Test]
        public void TryParse_FirstValueIsExpression_ExpressionAndValue()
        {
            var input = "(1 + 2) + 1";
            var isSuccessfull = ExpressionUnit.TryParse(input, out var expressionUnit);
            Assert.IsTrue(isSuccessfull);
            Assert.AreEqual(typeof(ExpressionUnit), expressionUnit.FirstValue.GetType());
        }

        [Test]
        public void TryParse_FirstValueIsValue_ExpressionAndValue()
        {
            var input = "1 + (2 - 1)";
            var isSuccessfull = ExpressionUnit.TryParse(input, out var expressionUnit);
            Assert.IsTrue(isSuccessfull);
            Assert.AreEqual(typeof(ValueUnit), expressionUnit.FirstValue.GetType());
        }

        [Test]
        public void TryParse_MinusOperation_ExpressionAndExpression()
        {
            var input = "(1 + 5) - (2 - 1)";
            var isSuccessfull = ExpressionUnit.TryParse(input, out var expressionUnit);
            Assert.IsTrue(isSuccessfull);
            Assert.AreEqual(Operation.Minus, expressionUnit.Operation);
        }

        [Test]
        public void TryParse_MinusOperation_ExpressionAndValue()
        {
            var input = "(1 * 8) - 2";
            var isSuccessfull = ExpressionUnit.TryParse(input, out var expressionUnit);
            Assert.IsTrue(isSuccessfull);
            Assert.AreEqual(Operation.Minus, expressionUnit.Operation);
        }

        [Test]
        public void TryParse_MinusOperation_ValueAndValue()
        {
            var input = "1 - 2";
            var isSuccessfull = ExpressionUnit.TryParse(input, out var expressionUnit);
            Assert.IsTrue(isSuccessfull);
            Assert.AreEqual(Operation.Minus, expressionUnit.Operation);
        }

        [Test]
        public void TryParse_MultiplyOperation_ExpressionAndExpression()
        {
            var input = "(1 + 5) * (2 - 1)";
            var isSuccessfull = ExpressionUnit.TryParse(input, out var expressionUnit);
            Assert.IsTrue(isSuccessfull);
            Assert.AreEqual(Operation.Multiply, expressionUnit.Operation);
        }

        [Test]
        public void TryParse_MultiplyOperation_ExpressionAndValue()
        {
            var input = "(1 - 0) * 2";
            var isSuccessfull = ExpressionUnit.TryParse(input, out var expressionUnit);
            Assert.IsTrue(isSuccessfull);
            Assert.AreEqual(Operation.Multiply, expressionUnit.Operation);
        }

        [Test]
        public void TryParse_MultiplyOperation_ValueAndValue()
        {
            var input = "1 * 2";
            var isSuccessfull = ExpressionUnit.TryParse(input, out var expressionUnit);
            Assert.IsTrue(isSuccessfull);
            Assert.AreEqual(Operation.Multiply, expressionUnit.Operation);
        }

        [Test]
        public void TryParse_PlusOperation_ExpressionAndExpression()
        {
            var input = "(1 + 5) + (2 - 1)";
            var isSuccessfull = ExpressionUnit.TryParse(input, out var expressionUnit);
            Assert.IsTrue(isSuccessfull);
            Assert.AreEqual(Operation.Plus, expressionUnit.Operation);
        }

        [Test]
        public void TryParse_PlusOperation_ExpressionAndValue()
        {
            var input = "(1 - 5) + 2";
            var isSuccessfull = ExpressionUnit.TryParse(input, out var expressionUnit);
            Assert.IsTrue(isSuccessfull);
            Assert.AreEqual(Operation.Plus, expressionUnit.Operation);
        }

        [Test]
        public void TryParse_PlusOperation_ValueAndValue()
        {
            var input = "1 + 2";
            var isSuccessfull = ExpressionUnit.TryParse(input, out var expressionUnit);
            Assert.IsTrue(isSuccessfull);
            Assert.AreEqual(Operation.Plus, expressionUnit.Operation);
        }

        [Test]
        public void TryParse_SecondValueIsExpression_ExpressionAndValue()
        {
            var input = "1 + (2 - 1)";
            var isSuccessfull = ExpressionUnit.TryParse(input, out var expressionUnit);
            Assert.IsTrue(isSuccessfull);
            Assert.AreEqual(typeof(ExpressionUnit), expressionUnit.SecondValue.GetType());
        }

        [Test]
        public void TryParse_SecondValueIsValue_ExpressionAndValue()
        {
            var input = "(1 + 2) + 1";
            var isSuccessfull = ExpressionUnit.TryParse(input, out var expressionUnit);
            Assert.IsTrue(isSuccessfull);
            Assert.AreEqual(typeof(ValueUnit), expressionUnit.SecondValue.GetType());
        }
    }
}