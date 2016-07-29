using Microsoft.VisualStudio.TestTools.UnitTesting;
using nStella.Core.Format;
using System;

namespace nStella.Core.Tests.Formatter
{
    [TestClass]
    public class CEPFormatterTest
    {
        private IFormatter formatter;

        [TestInitialize]
        public void Init()
        {
            formatter = new CEPFormatter();
        }

        [TestMethod]
        public void FormatTest()
        {
            string unfotmatedValue = "12345678";
            string formatedValue = formatter.Format(unfotmatedValue);
            Assert.AreEqual(formatedValue, "12345-678");
        }

        [TestMethod]
        public void UnformatTest()
        {
            string unfotmatedValue = "12345-678";
            string formatedValue = formatter.UnFormat(unfotmatedValue);
            Assert.AreEqual(formatedValue, "12345678");
        }

        [TestMethod]        
        public void ShouldVerifyIfAValueIsFormattedOrNot()
        {
            Assert.IsTrue(formatter.IsFormatted("12345-678"));
            Assert.IsFalse(formatter.IsFormatted("12345678"));
            Assert.IsFalse(formatter.IsFormatted("12345-67a"));
        }

        [TestMethod]        
        public void ShouldVerifyIfAValueCanBeFormattedOrNot()
        {
            Assert.IsFalse(formatter.CanBeFormatted("12345-678"));
            Assert.IsTrue(formatter.CanBeFormatted("12345678"));
            Assert.IsFalse(formatter.CanBeFormatted("12345-678"));
        }

        [TestMethod]
        public void ShoudNotThrowExceptionIfAlreadyUnformatedTest()
        {
            string fotmatedValue = "12345678";
            string unformatedValue = formatter.UnFormat(fotmatedValue);
            Assert.AreEqual(unformatedValue, "12345678");
        }
    }
}
