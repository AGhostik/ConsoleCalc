using System;
using ConsoleCalc.Models;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ExpressionUnitTests
    {
        [TestCase(1, Operation.Plus, 2, ExpectedResult = 3)]
        [TestCase(1.1, Operation.Plus, 1.2, ExpectedResult = 2.3)]
        [TestCase(1, Operation.Minus, 2, ExpectedResult = -1)]
        [TestCase(3.3, Operation.Minus, 1.1, ExpectedResult = 2.2)]
        [TestCase(2, Operation.Multiply, 2, ExpectedResult = 4)]
        [TestCase(1.5, Operation.Multiply, 3.3, ExpectedResult = 4.95)]
        [TestCase(6, Operation.Div, 3, ExpectedResult = 2)]
        [TestCase(8.2, Operation.Div, 1.6, ExpectedResult = 5.125)]
        [TestCase(6, Operation.DivRem, 4, ExpectedResult = 2)]
        [TestCase(7.8, Operation.DivRem, 5.2, ExpectedResult = 2.6)]
        public decimal GetResult(decimal firstValue, Operation operation, decimal secondValue)
        {
            var expressionUnit = new ExpressionUnit(new ValueUnit(firstValue), operation, new ValueUnit(secondValue));
            return expressionUnit.GetResult();
        }
        
        [TestCase("1 + 2", ExpectedResult = Operation.Plus)]
        [TestCase("1 - 2", ExpectedResult = Operation.Minus)]
        [TestCase("1 * 2", ExpectedResult = Operation.Multiply)]
        [TestCase("1 / 2", ExpectedResult = Operation.Div)]
        [TestCase("1 % 2", ExpectedResult = Operation.DivRem)]
        [TestCase("(1 - 5) + 2", ExpectedResult = Operation.Plus)]
        [TestCase("(1 * 5) - 2", ExpectedResult = Operation.Minus)]
        [TestCase("(1 - 5) * 2", ExpectedResult = Operation.Multiply)]
        [TestCase("(1 + 5) / 2", ExpectedResult = Operation.Div)]
        [TestCase("(1 + 5) % 2", ExpectedResult = Operation.DivRem)]
        [TestCase("(3 + 8) + (2 - 1)", ExpectedResult = Operation.Plus)]
        [TestCase("(3 + 8) - (2 - 1)", ExpectedResult = Operation.Minus)]
        [TestCase("(3 + 8) * (2 - 1)", ExpectedResult = Operation.Multiply)]
        [TestCase("(3 + 8) / (2 - 1)", ExpectedResult = Operation.Div)]
        [TestCase("(3 + 8) % (2 - 1)", ExpectedResult = Operation.DivRem)]
        public Operation TryParse_Operation(string input)
        {
            ExpressionUnit.TryParse(input, out var expressionUnit);
            return expressionUnit.Operation;
        }
        
        [TestCase("1 + 2", ExpectedResult = typeof(ValueUnit))]
        [TestCase("-1 + -2", ExpectedResult = typeof(ValueUnit))]
        [TestCase("1 + (2 - 1)", ExpectedResult = typeof(ValueUnit))]
        [TestCase("(1 + 2) + 1", ExpectedResult = typeof(ExpressionUnit))]
        [TestCase("(1 + 5) * (2 - 1)", ExpectedResult = typeof(ExpressionUnit))]
        public Type TryParse_FirstValueType(string input)
        {
            ExpressionUnit.TryParse(input, out var expressionUnit);
            return expressionUnit.FirstValue.GetType();
        }

        [TestCase("1 + 2", ExpectedResult = typeof(ValueUnit))]
        [TestCase("-1 + -2", ExpectedResult = typeof(ValueUnit))]
        [TestCase("1 + (2 - 1)", ExpectedResult = typeof(ExpressionUnit))]
        [TestCase("(1 + 2) + 1", ExpectedResult = typeof(ValueUnit))]
        [TestCase("(1 + 5) * (2 - 1)", ExpectedResult = typeof(ExpressionUnit))]
        public Type TryParse_SecondValueType(string input)
        {
            ExpressionUnit.TryParse(input, out var expressionUnit);
            return expressionUnit.SecondValue.GetType();
        }
    }
}