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
        public void NormalizeInput_RemoveExcessLeadingSigns_HaveOddNumberOfLeadingSign()
        {
            var expressionEvaluator = new ExpressionEvaluator();
            var input = "1 + ---5";
            var result = expressionEvaluator.NormalizeInput(input);
            Assert.AreEqual("1 + -5", result);
        }

        [Test]
        public void NormalizeInput_RemoveExcessLeadingSigns_HaveEvenNumberOfLeadingSign()
        {
            var expressionEvaluator = new ExpressionEvaluator();
            var input = "1 + ----5";
            var result = expressionEvaluator.NormalizeInput(input);
            Assert.AreEqual("1 + 5", result);
        }
    }
}
