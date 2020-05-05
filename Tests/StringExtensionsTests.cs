using ConsoleCalc.Extensions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        //todo: добавить:
        //todo: 1. добавление скобочек "((x * y) + z) + w" или "(x * y) + (z + w)" - это нужно для того, чтобы парсеры работали корректно
        //todo: 2. удаление лишних скобочек: "((x + y))"
        //todo: 3. поиск ошибок, выдача номера символа с ошибкой "(x + y" - ErrorIndex=5, ErrorMessage="expected ')'"

        [Test]
        public void AddBracers_PlusMultiply()
        {
            var input = "1 + 5 * 2";
            var result = input.AddBracers();
            Assert.AreEqual("1 + (5 * 2)", result);
        }

        [Test]
        public void AddBracers_MultiplyPlus()
        {
            var input = "1 * 5 + 2";
            var result = input.AddBracers();
            Assert.AreEqual("(1 * 5) + 2", result);
        }

        [Test]
        public void AddBracers_MultiplyPlusMultiply()
        {
            var input = "1 * 5 + 2 * 9";
            var result = input.AddBracers();
            Assert.AreEqual("(1 * 5) + (2 * 9)", result);
        }

        [Test]
        public void AddBracers_BracersMultiplyMultiplyPlus()
        {
            var input = "(1 * 5) * 2 + 9";
            var result = input.AddBracers();
            Assert.AreEqual("((1 * 5) * 2) + 9", result);
        }

        [Test]
        public void AddBracers_MultiplyMultiplyPlus()
        {
            var input = "1 * 5 * 2 + 9";
            var result = input.AddBracers();
            Assert.AreEqual("((1 * 5) * 2) + 9", result);
        }

        [Test]
        public void AddSpacebars()
        {
            var input = "1+5";
            var result = input.AddSpacebars();
            Assert.AreEqual("1 + 5", result);
        }

        [Test]
        public void RemoveExcessLeadingSigns_HaveEvenNumberOfLeadingSign()
        {
            var input = "1 + ----5";
            var result = input.RemoveExcessLeadingSign();
            Assert.AreEqual("1 + 5", result);
        }

        [Test]
        public void RemoveExcessLeadingSigns_HaveOddNumberOfLeadingSign()
        {
            var input = "1 + ---5";
            var result = input.RemoveExcessLeadingSign();
            Assert.AreEqual("1 + -5", result);
        }

        [Test]
        public void RemoveExcessSpacebar()
        {
            var input = "1 +     5";
            var result = input.RemoveExcessSpacebar();
            Assert.AreEqual("1 + 5", result);
        }

        [Test]
        public void RemoveExcessSpacebar_RemoveExcessLeadingSign_AddSpacebars()
        {
            var input = "--1/        ----5";
            var result = input.RemoveExcessSpacebar().RemoveExcessLeadingSign().AddSpacebars();
            Assert.AreEqual("1 / 5", result);
        }
    }
}