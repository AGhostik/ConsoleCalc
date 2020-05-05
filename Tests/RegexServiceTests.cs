using System;
using System.Collections.Generic;
using System.Text;
using ConsoleCalc;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class RegexServiceTests
    {
        [Test]
        public void FindMultipleSpacebars()
        {
            var regex = RegexService.GetRegex_FindMultipleSpacebars();
            var spacebars = "          ";
            var input = $"a{spacebars}b";
            var matches = regex.Matches(input);

            Assert.AreEqual(1, matches.Count);
            Assert.AreEqual(spacebars, matches[0].Value);
        }
        
        [Test]
        public void FindMultipleMinusSymbol()
        {
            var regex = RegexService.GetRegex_FindMultipleMinusSymbol();
            var minuses1 = "--";
            var minuses2 = "-------";
            var input = $"{minuses1}a{minuses2}b";
            var matches = regex.Matches(input);

            Assert.AreEqual(2, matches.Count);
            Assert.AreEqual(minuses1, matches[0].Value);
            Assert.AreEqual(minuses2, matches[1].Value);
        }

        [Test]
        public void FindExpressionParts_ValuePlusValue()
        {
            var regex = RegexService.GetRegex_FindExpressionParts();
            var input = "1 + 2";
            var groups = regex.Match(input).Groups;
            
            Assert.AreEqual("1", groups["firstValue"].Value);
            Assert.AreEqual("+", groups["operation"].Value);
            Assert.AreEqual("2", groups["secondValue"].Value);
        }

        [Test]
        public void FindExpressionParts_ValueDivRemValue()
        {
            var regex = RegexService.GetRegex_FindExpressionParts();
            var input = "5 % 2";
            var groups = regex.Match(input).Groups;
            
            Assert.AreEqual("5", groups["firstValue"].Value);
            Assert.AreEqual("%", groups["operation"].Value);
            Assert.AreEqual("2", groups["secondValue"].Value);
        }

        [Test]
        public void FindExpressionParts_ExpressionMinusExpression()
        {
            var regex = RegexService.GetRegex_FindExpressionParts();
            var input = "(1 * 3) - (2 / 4)";
            var groups = regex.Match(input).Groups;
            
            Assert.AreEqual("(1 * 3)", groups["firstValue"].Value);
            Assert.AreEqual("-", groups["operation"].Value);
            Assert.AreEqual("(2 / 4)", groups["secondValue"].Value);
        }

        [Test]
        public void FindExpressionParts_ValueDivExpression()
        {
            var regex = RegexService.GetRegex_FindExpressionParts();
            var input = "3 / (2 / 4)";
            var groups = regex.Match(input).Groups;
            
            Assert.AreEqual("3", groups["firstValue"].Value);
            Assert.AreEqual("/", groups["operation"].Value);
            Assert.AreEqual("(2 / 4)", groups["secondValue"].Value);
        }

        [Test]
        public void FindExpressionParts_ExpressionMultiplyValue()
        {
            var regex = RegexService.GetRegex_FindExpressionParts();
            var input = "(1 * 3) * 4";
            var groups = regex.Match(input).Groups;
            
            Assert.AreEqual("(1 * 3)", groups["firstValue"].Value);
            Assert.AreEqual("*", groups["operation"].Value);
            Assert.AreEqual("4", groups["secondValue"].Value);
        }
    }
}
