using System;
using System.Linq;
using ConsoleCalc;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ExpressionEvaluatorTests
    {
        [Test]
        public void NormalizeInput_RemoveExcessSpacebar_HaveMultipleSpacebar()
        {
            var expressionEvaluator = new ExpressionEvaluator();
            var input = "1 +     5";
            var result = expressionEvaluator.NormalizeInput(input);
            Assert.AreEqual("1 + 5", result);
        }
        
        [Test]
        public void Parse_ExpressionValuesEqualInputValues_ExpressionParsed()
        {
            var expressionEvaluator = new ExpressionEvaluator();
            var input = "1 + 2";
            var expressionUnit = expressionEvaluator.Parse(input);
            Assert.AreEqual(1, expressionUnit.FirstValue.GetResult());
            Assert.AreEqual(2, expressionUnit.SecondValue.GetResult());
        }

        [Test]
        public void Parse_ExpressionFirstValueIsAnotherExpression_ExpressionParsed()
        {
            var expressionEvaluator = new ExpressionEvaluator();
            var input = "(1 + 2) - 1";  
            var expressionUnit = expressionEvaluator.Parse(input);
            Assert.AreEqual(typeof(ExpressionUnit), expressionUnit.FirstValue.GetType());
        }

        [Test]
        public void Parse_ExpressionSecondValueIsAnotherExpression_ExpressionParsed()
        {
            var expressionEvaluator = new ExpressionEvaluator();
            var input = "1 + (2 - 1)";  
            var expressionUnit = expressionEvaluator.Parse(input);
            Assert.AreEqual(typeof(ExpressionUnit), expressionUnit.SecondValue.GetType());
        }

        [Test]
        public void Parse_ExpressionBothValueIsExpression_ExpressionParsed()
        {
            var expressionEvaluator = new ExpressionEvaluator();
            var input = "(1 + 5) * (2 - 1)";  
            var expressionUnit = expressionEvaluator.Parse(input);
            Assert.AreEqual(typeof(ExpressionUnit), expressionUnit.FirstValue.GetType());
            Assert.AreEqual(typeof(ExpressionUnit), expressionUnit.SecondValue.GetType());
        }
    }
}
