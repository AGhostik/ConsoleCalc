using System.Linq;
using ConsoleCalc;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class InputValidationServiceTests
    {
        // не уверен как правильно, но пусть будет
        // ( - RightBracket
        // ) - LeftBracket
        // по названию стороны, в которую открыта скобочка

        //todo: добавить поиск незавершенных выражений, анпример "1 + 3 * "

        [Test]
        public void FindUnexpectedCharacters_NoErrors_NoUnexpectedCharacters()
        {
            var input = "(((1 + 2.4) * (45.09 / 2)) % 10) + (6 - 1)";
            var results = InputValidationService.FindUnexpectedCharacters(input).ToArray();
            Assert.AreEqual(0, results.Length);
        }

        [Test]
        public void FindInvalidBracket_NoErrors_NoInvalidBracket()
        {
            var input = "(((1 + 2.4) * (45.09 / 2)) % 10) + (6 - 1)";
            var results = InputValidationService.FindInvalidBracket(input).ToArray();
            Assert.AreEqual(0, results.Length);
        }

        [Test]
        public void FindInvalidBracket_OneErrors_OneUnclosedRightBracket()
        {
            //todo: обдумать, насколько полезна такая информация пользователю

            var input = "(((1 + 2.4) * (45.09 / 2)) % 10) + ((6 - 1)";
            var results = InputValidationService.FindInvalidBracket(input).ToArray();
            Assert.AreEqual(1, results.Length);
            Assert.AreEqual(input.Length + 1, results[0].Index);
            Assert.AreEqual('(', results[0].Character);
        }

        [Test]
        public void FindInvalidBracket_OneErrors_OneUnclosedLeftBracket()
        {
            var input = "(((1 + 2.4) * (45.09 / 2)) % 10)) + (6 - 1)";
            var results = InputValidationService.FindInvalidBracket(input).ToArray();
            Assert.AreEqual(1, results.Length);
            Assert.AreEqual(33, results[0].Index);
            Assert.AreEqual(')', results[0].Character);
        }

        [Test]
        public void FindInvalidBracket_TwoErrors_OneUnclosedLeftBracket()
        {
            var input = "(((1 + 2.4) * (45.09 / 2)) % 10)) + (6) - 1)";
            var results = InputValidationService.FindInvalidBracket(input).ToArray();
            Assert.AreEqual(2, results.Length);
            Assert.AreEqual(33, results[0].Index);
            Assert.AreEqual(')', results[0].Character);
            Assert.AreEqual(44, results[1].Index);
            Assert.AreEqual(')', results[1].Character);
        }

        [Test]
        public void FindUnexpectedCharacters_OneError_LetterBackslash()
        {
            var input = "(((1 + 2.4) * (45.09 \\ 2)) % 10) + (6 - 1)";
            var results = InputValidationService.FindUnexpectedCharacters(input).ToArray();
            Assert.AreEqual(1, results.Length);
            Assert.AreEqual(22, results[0].Index);
            Assert.AreEqual('\\', results[0].Character);
        }

        [Test]
        public void FindUnexpectedCharacters_OneError_LetterComma()
        {
            var input = "(((1 + 2,4) * (45.09 / 2)) % 10) + (6 - 1)";
            var results = InputValidationService.FindUnexpectedCharacters(input).ToArray();
            Assert.AreEqual(1, results.Length);
            Assert.AreEqual(9, results[0].Index);
            Assert.AreEqual(',', results[0].Character);
        }

        [Test]
        public void FindUnexpectedCharacters_OneError_LetterX()
        {
            var input = "(((1 + 2.4) *X (45.09 / 2)) % 10) + (6 - 1)";
            var results = InputValidationService.FindUnexpectedCharacters(input).ToArray();
            Assert.AreEqual(1, results.Length);
            Assert.AreEqual(14, results[0].Index);
            Assert.AreEqual('X', results[0].Character);
        }

        [Test]
        public void FindUnexpectedCharacters_OneError_QuestionMark()
        {
            var input = "(((1 + 2.4) ? (45.09 / 2)) % 10) + (6 - 1)";
            var results = InputValidationService.FindUnexpectedCharacters(input).ToArray();
            Assert.AreEqual(1, results.Length);
            Assert.AreEqual(13, results[0].Index);
            Assert.AreEqual('?', results[0].Character);
        }
    }
}