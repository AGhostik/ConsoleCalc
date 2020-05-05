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
            var input = "50 % 221";
            var groups = regex.Match(input).Groups;
            
            Assert.AreEqual("50", groups["firstValue"].Value);
            Assert.AreEqual("%", groups["operation"].Value);
            Assert.AreEqual("221", groups["secondValue"].Value);
        }

        [Test]
        public void FindExpressionParts_ExpressionMinusExpression()
        {
            var regex = RegexService.GetRegex_FindExpressionParts();
            var input = "(1 * 3.1) - (2.5 / 4.01)";
            var groups = regex.Match(input).Groups;
            
            Assert.AreEqual("(1 * 3.1)", groups["firstValue"].Value);
            Assert.AreEqual("-", groups["operation"].Value);
            Assert.AreEqual("(2.5 / 4.01)", groups["secondValue"].Value);
        }

        [Test]
        public void FindExpressionParts_ValueDivExpression()
        {
            var regex = RegexService.GetRegex_FindExpressionParts();
            var input = "3.9 / (2 / 4)";
            var groups = regex.Match(input).Groups;
            
            Assert.AreEqual("3.9", groups["firstValue"].Value);
            Assert.AreEqual("/", groups["operation"].Value);
            Assert.AreEqual("(2 / 4)", groups["secondValue"].Value);
        }

        [Test]
        public void FindExpressionParts_ExpressionMultiplyValue()
        {
            var regex = RegexService.GetRegex_FindExpressionParts();
            var input = "(1000 * 3) * 4321.1";
            var groups = regex.Match(input).Groups;
            
            Assert.AreEqual("(1000 * 3)", groups["firstValue"].Value);
            Assert.AreEqual("*", groups["operation"].Value);
            Assert.AreEqual("4321.1", groups["secondValue"].Value);
        }

        [Test]
        public void FindExpessionForBracersAdding_FindMultiplyExpression()
        {
            var regex = RegexService.GetRegex_FindExpessionForBracersAdding();
            var input = "10 * 3 + 1";
            var matches = regex.Matches(input);

            Assert.AreEqual(1, matches.Count);
            Assert.AreEqual("10 * 3", matches[0].Value);
        }

        [Test]
        public void FindExpessionForBracersAdding_FindMultiplyExpression_Decimal()
        {
            var regex = RegexService.GetRegex_FindExpessionForBracersAdding();
            var input = "10.1 * 3.66 + 1";
            var matches = regex.Matches(input);

            Assert.AreEqual(1, matches.Count);
            Assert.AreEqual("10.1 * 3.66", matches[0].Value);
        }

        [Test]
        public void FindExpessionForBracersAdding_FindPlusExpression()
        {
            var regex = RegexService.GetRegex_FindExpessionForBracersAdding(false);
            var input = "10 * 3 + 1";
            var matches = regex.Matches(input);

            Assert.AreEqual(1, matches.Count);
            Assert.AreEqual("3 + 1", matches[0].Value);
        }

        [Test]
        public void FindExpessionForBracersAdding_FindMultiplyExpression_OneValueReplaced()
        {
            var regex = RegexService.GetRegex_FindExpessionForBracersAdding();
            var input = "13 + 66 * \"0\"";
            var matches = regex.Matches(input);

            Assert.AreEqual(1, matches.Count);
            Assert.AreEqual("66 * \"0\"", matches[0].Value);
        }

        [Test]
        public void FindExpessionForBracersAdding_FindPlusExpression_OneValueReplaced()
        {
            var regex = RegexService.GetRegex_FindExpessionForBracersAdding(false);
            var input = "13 + \"1\" * \"2\"";
            var matches = regex.Matches(input);

            Assert.AreEqual(1, matches.Count);
            Assert.AreEqual("13 + \"1\"", matches[0].Value);
        }
    }
}
