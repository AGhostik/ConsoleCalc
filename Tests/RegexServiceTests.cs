using ConsoleCalc;
using ConsoleCalc.Models;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class RegexServiceTests
    {
        [TestCase("a          b", ExpectedResult = "          ")]
        public string FindMultipleSpacebars(string input)
        {
            var regex = RegexService.GetRegex_FindMultipleSpacebars();
            var match = regex.Match(input);
            return match.Value;
        }

        [TestCase("91b8c5e5_1ee3_4807_ae18_74ea02134060", ExpectedResult = "")]
        [TestCase("\"91b8c5e5-1ee3-4807-ae18-74ea02134060\"", ExpectedResult = "")]
        [TestCase("\"91b8c5e5_1ee3_4807_ae18_74ea02134060\"", ExpectedResult = "\"91b8c5e5_1ee3_4807_ae18_74ea02134060\"")]
        public string FindGuid(string input)
        {
            var regex = RegexService.GetRegex_FindGuid();
            var match = regex.Match(input);
            return match.Value;
        }
        
        [TestCase("--a-b", ExpectedResult = "--")]
        [TestCase("a------b", ExpectedResult = "------")]
        public string FindMultipleMinusSymbol(string input)
        {
            var regex = RegexService.GetRegex_FindMultipleMinusSymbol();
            var match = regex.Match(input);
            return match.Value;
        }

        [TestCase("1+2", ExpectedResult = "1")]
        [TestCase("1 + 2", ExpectedResult = "1")]
        [TestCase("50 % 221", ExpectedResult = "50")]
        [TestCase("(1 * 3.1) - (2.5 / 4.01)", ExpectedResult = "(1 * 3.1)")]
        [TestCase("3.9/(2 / 4)", ExpectedResult = "3.9")]
        [TestCase("(1000 * 3) * 4321.1", ExpectedResult = "(1000 * 3)")]
        public string FindExpressionParts_FirstValue(string input)
        {
            var regex = RegexService.GetRegex_FindExpressionParts();
            var groups = regex.Match(input).Groups;
            return groups["firstValue"].Value;
        }

        [TestCase("1+2", ExpectedResult = "2")]
        [TestCase("1 + 2", ExpectedResult = "2")]
        [TestCase("50 % 221", ExpectedResult = "221")]
        [TestCase("(1 * 3.1) - (2.5 / 4.01)", ExpectedResult = "(2.5 / 4.01)")]
        [TestCase("3.9/(2 / 4)", ExpectedResult = "(2 / 4)")]
        [TestCase("(1000 * 3) * 4321.1", ExpectedResult = "4321.1")]
        public string FindExpressionParts_SecondValue(string input)
        {
            var regex = RegexService.GetRegex_FindExpressionParts();
            var groups = regex.Match(input).Groups;
            return groups["secondValue"].Value;
        }

        [TestCase("10*3+1", ExpectedResult = "3+1")]
        [TestCase("10 * 3 + 1", ExpectedResult = "3 + 1")]
        [TestCase("10.1 * 3.66 + 1", ExpectedResult = "3.66 + 1")]
        [TestCase("13 + 66 * \"0\"", ExpectedResult = "13 + 66")]
        [TestCase("13 + \"16ff277c_3c0f_4460_8dc0_f2895a98e290\" * \"d9171059_276c_4695_9561_eaa8c28d30ae\"", ExpectedResult = "13 + \"16ff277c_3c0f_4460_8dc0_f2895a98e290\"")]
        public string FindExpessionForBracersAdding_Plus(string input)
        {
            var regex = RegexService.GetRegex_FindExpessionForBracersAdding(Operation.Plus);
            var matches = regex.Matches(input);
            return matches[0].Value;
        }

        [TestCase("10*3-1", ExpectedResult = "3-1")]
        [TestCase("10 * 3 - 1", ExpectedResult = "3 - 1")]
        [TestCase("10.1 * 3.66 - 1", ExpectedResult = "3.66 - 1")]
        [TestCase("13 - 66 * \"0\"", ExpectedResult = "13 - 66")]
        [TestCase("13 - \"16ff277c_3c0f_4460_8dc0_f2895a98e290\" * \"d9171059_276c_4695_9561_eaa8c28d30ae\"", ExpectedResult = "13 - \"16ff277c_3c0f_4460_8dc0_f2895a98e290\"")]
        public string FindExpessionForBracersAdding_Minus(string input)
        {
            var regex = RegexService.GetRegex_FindExpessionForBracersAdding(Operation.Minus);
            var matches = regex.Matches(input);
            return matches[0].Value;
        }

        [TestCase("13 - \"16ff277c_3c0f_4460_8dc0_f2895a98e290\" * \"d9171059_276c_4695_9561_eaa8c28d30ae\"", ExpectedResult = 1)]
        public int FindExpessionForBracersAdding_Count_Minus(string input)
        {
            var regex = RegexService.GetRegex_FindExpessionForBracersAdding(Operation.Minus);
            var matches = regex.Matches(input);
            return matches.Count;
        }

        [TestCase("10*3+1", ExpectedResult = "10*3")]
        [TestCase("10 * 3 + 1", ExpectedResult = "10 * 3")]
        [TestCase("10.1 * 3.66 + 1", ExpectedResult = "10.1 * 3.66")]
        [TestCase("13 + 66 * \"0\"", ExpectedResult = "66 * \"0\"")]
        [TestCase("13 + \"16ff277c_3c0f_4460_8dc0_f2895a98e290\" * \"d9171059_276c_4695_9561_eaa8c28d30ae\"", ExpectedResult = "\"16ff277c_3c0f_4460_8dc0_f2895a98e290\" * \"d9171059_276c_4695_9561_eaa8c28d30ae\"")]
        public string FindExpessionForBracersAdding_Multiply(string input)
        {
            var regex = RegexService.GetRegex_FindExpessionForBracersAdding(Operation.Multiply);
            var matches = regex.Matches(input);
            return matches[0].Value;
        }
    }
}
