using ConsoleCalc.Extensions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class StringExtensionsTests
    {
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
        public void RemoveExcessSpacebar_RemoveExcessLeadingSign_AddSpacebars_AddBracers()
        {
            var input = "--1/        ----5 + 4";
            var result = input.RemoveExcessSpacebar().RemoveExcessLeadingSign().AddSpacebars().AddBracers();
            Assert.AreEqual("(1 / 5) + 4", result);
        }
    }
}