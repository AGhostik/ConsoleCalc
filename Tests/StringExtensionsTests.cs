using ConsoleCalc.Extensions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void NormalizeInput_AddSpacebars_DoNotHaveSpacebars()
        {
            var input = "1+5";
            var result = input.AddSpacebars();
            Assert.AreEqual("1 + 5", result);
        }

        [Test]
        public void NormalizeInput_AddSpacebars_RemoveExcessLeadingSigns_DoNotHaveSpacebars()
        {
            var input = "1---5";
            var result = input.RemoveExcessLeadingSign().AddSpacebars();
            Assert.AreEqual("1 - 5", result);
        }

        [Test]
        public void NormalizeInput_RemoveExcessLeadingSigns_HaveEvenNumberOfLeadingSign()
        {
            var input = "1 + ----5";
            var result = input.RemoveExcessLeadingSign();
            Assert.AreEqual("1 + 5", result);
        }

        [Test]
        public void NormalizeInput_RemoveExcessLeadingSigns_HaveOddNumberOfLeadingSign()
        {
            var input = "1 + ---5";
            var result = input.RemoveExcessLeadingSign();
            Assert.AreEqual("1 + -5", result);
        }

        [Test]
        public void NormalizeInput_RemoveExcessSpacebar_HaveMultipleSpacebar()
        {
            var input = "1 +     5";
            var result = input.RemoveExcessSpacebar();
            Assert.AreEqual("1 + 5", result);
        }
    }
}