using Microsoft.VisualStudio.TestTools.UnitTesting;
using nStella.Core.Format;

namespace nStella.Core.Tests.Formatter
{
    [TestClass]
    public class TituloEleitoralFormatterTest
    {

        private IFormatter formatter;

        [TestInitialize]
        public void before()
        {
            formatter = new TituloEleitoralFormatter();
        }

        [TestMethod]
        public void TestFormat()
        {
            string unfotmatedValue = "133968200302";
            string formatedValue = formatter.Format(unfotmatedValue);
            Assert.AreEqual(formatedValue, "1339682003/02");
        }

        [TestMethod]
        public void TestUnformat()
        {
            string fotmatedValue = "1339682003/02";
            string unformatedValue = formatter.UnFormat(fotmatedValue);
            Assert.AreEqual(unformatedValue, "133968200302");
        }

        [TestMethod]
        public void ShouldVerifyIfAValueIsFormattedOrNot()
        {
            Assert.IsTrue(formatter.IsFormatted("1339682003/02"));
            Assert.IsFalse(formatter.IsFormatted("133968200302"));
            Assert.IsFalse(formatter.IsFormatted("1339682003/0x"));
        }

        [TestMethod]
        public void ShouldVerifyIfAValueCanBeFormattedOrNot()
        {
            Assert.IsFalse(formatter.CanBeFormatted("1339682003/02"));
            Assert.IsTrue(formatter.CanBeFormatted("133968200302"));
            Assert.IsFalse(formatter.CanBeFormatted("1339682003/0x"));
        }

    }
}