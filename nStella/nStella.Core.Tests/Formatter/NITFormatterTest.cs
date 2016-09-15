using Microsoft.VisualStudio.TestTools.UnitTesting;
using nStella.Core.Format;

namespace nStella.Core.Tests.Formatter
{
    [TestClass]
    public class NITFormatterTest
    {
        private IFormatter formatter;

        [TestInitialize]
        public void Init()
        {
            formatter = new NITFormatter();
        }

        [TestMethod]
        public void TestFormat()
        {
            string unfotmatedValue = "17033259504";
            string formatedValue = formatter.Format(unfotmatedValue);
            Assert.AreEqual(formatedValue, "170.33259.50-4");
        }
        [TestMethod]
        public void TestUnformat()
        {
            string fotmatedValue = "170.33259.50-4";
            string unformatedValue = formatter.UnFormat(fotmatedValue);
            Assert.AreEqual(unformatedValue, "17033259504");
        }
        [TestMethod]
        public void verifyIfAValueIsAlreadyFormattedOrNot()
        {
            Assert.IsTrue(formatter.IsFormatted("170.33259.50-4"));
            Assert.IsFalse(formatter.IsFormatted("17033259504"));
            Assert.IsFalse(formatter.IsFormatted("170.C32b9.50-a"));
        }
        [TestMethod]
        public void verifyIfAValueCanBeFormattedOrNot()
        {
            Assert.IsFalse(formatter.CanBeFormatted("170.33259.50-4"));
            Assert.IsTrue(formatter.CanBeFormatted("17033259504"));
            Assert.IsFalse(formatter.CanBeFormatted("170.C32b9.50-a"));
        }
    }
}
