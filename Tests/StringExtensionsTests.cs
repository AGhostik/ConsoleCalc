using ConsoleCalc.Extensions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class StringExtensionsTests
    {
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