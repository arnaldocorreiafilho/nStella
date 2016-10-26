using Microsoft.VisualStudio.TestTools.UnitTesting;
using nStella.Core.Format;

namespace nStella.Core.Tests.Format
{
    [TestClass]
    public class LeftSideZerosFormatterTest
    {

        private IFormatter formatter;

        [TestInitialize]
        public void SetUp()
        {
            formatter = new LeftSideZerosFormatter(14);
        }

        [TestMethod]
        public void TestFormat()
        {
            string actual = formatter.Format("1234567890");
            Assert.AreEqual("00001234567890", actual);
        }

        [TestMethod]
        public void TestUnformat()
        {
            string actual = formatter.UnFormat("000567890");
            Assert.AreEqual("567890", actual);
        }

        [TestMethod]
        public void ShouldVerifyIfAValueIsAlreadyFormattedOrNot()
        {
            Assert.IsTrue(formatter.IsFormatted("00001234567890"));
            Assert.IsFalse(formatter.IsFormatted("00001234"));
            Assert.IsFalse(formatter.IsFormatted("1234567890"));
            Assert.IsFalse(formatter.IsFormatted("123456789012345"));
        }

        [TestMethod]
        public void ShouldVerifyIfAValueCanBeFormattedOrNot()
        {
            Assert.IsTrue(formatter.CanBeFormatted("00001234567890"));
            Assert.IsTrue(formatter.CanBeFormatted("00001234"));
            Assert.IsTrue(formatter.CanBeFormatted("1234567890"));
            Assert.IsFalse(formatter.CanBeFormatted("123456789012345"));
            Assert.IsFalse(formatter.CanBeFormatted("abc123"));
        }


    }
}