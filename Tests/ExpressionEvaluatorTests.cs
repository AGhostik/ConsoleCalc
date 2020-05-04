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
    }
}
