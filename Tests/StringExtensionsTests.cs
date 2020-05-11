using ConsoleCalc.Extensions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [TestCase("1 + 5 * 2", ExpectedResult = "(1 + (5 * 2))")]
        [TestCase("1 + 7 * 2", ExpectedResult = "(1 + (7 * 2))")]
        [TestCase("1 * 5 + 2", ExpectedResult = "((1 * 5) + 2)")]
        [TestCase("1 * (5 + 2)", ExpectedResult = "(1 * (5 + 2))")] // не проходит
        [TestCase("1 * 5 + 2 * 9", ExpectedResult = "((1 * 5) + (2 * 9))")]
        [TestCase("(1 * 5) * 2 + 9", ExpectedResult = "(((1 * 5) * 2) + 9)")] // не проходит
        [TestCase("1 * 5 * 2 + 9", ExpectedResult = "(((1 * 5) * 2) + 9)")]
        public string AddBracers(string input)
        {
            return input.AddBracers();
        }
        
        [TestCase("1+5", ExpectedResult = "1 + 5")]
        public string AddSpacebars(string input)
        {
            return input.AddSpacebars();
        }

        [TestCase("1 + ----5", ExpectedResult = "1 + 5")]
        [TestCase("1 + ---5", ExpectedResult = "1 + -5")]
        public string RemoveExcessLeadingSigns(string input)
        {
            return input.RemoveExcessLeadingSign();
        }
        
        [TestCase("1 +     5", ExpectedResult = "1 + 5")]
        public string RemoveExcessSpacebar(string input)
        {
            return input.RemoveExcessSpacebar();
        }

        [TestCase("--1/        ----5 + 4", ExpectedResult = "(1 / 5) + 4")]
        public string RemoveExcessSpacebar_RemoveExcessLeadingSign_AddSpacebars_AddBracers(string input)
        {
            return input.RemoveExcessSpacebar().RemoveExcessLeadingSign().AddSpacebars().AddBracers();
        }
    }
}