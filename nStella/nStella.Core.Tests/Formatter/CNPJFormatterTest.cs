using Microsoft.VisualStudio.TestTools.UnitTesting;
using nStella.Core.Format;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Tests.Formatter
{
    [TestClass]
    public class CNPJFormatterTest
    {
        private IFormatter formatter;

        [TestInitialize]
        public void Init()
        {
            formatter = new CNPJFormatter();
        }

        [TestMethod]
        public void TestFormat()
        {
            string unfotmatedValue = "26637142000158";
            string formatedValue = formatter.Format(unfotmatedValue);
            Assert.AreEqual(formatedValue, "26.637.142/0001-58");
        }

        [TestMethod]
        public void TestUnformat()
        {
            string fotmatedValue = "26.637.142/0001-58";
            string unformatedValue = formatter.UnFormat(fotmatedValue);
            Assert.AreEqual(unformatedValue, "26637142000158");
        }

        [TestMethod]
        public void TestShoudNotThrowExceptionIfAlreadyUnformated()
        {
            string fotmatedValue = "26637142000158";
            String unformatedValue = formatter.UnFormat(fotmatedValue);
            Assert.AreEqual(unformatedValue, "26637142000158");
        }

        [TestMethod]
        public void ShouldVerifyIfAValueIsAlreadyFormattedOrNot()
        {

            Assert.IsTrue(formatter.IsFormatted("26.637.142/0001-58"));
            Assert.IsFalse(formatter.IsFormatted("26637142000158"));
            Assert.IsFalse(formatter.IsFormatted("26.7.1x2/00a1-58"));
        }

        [TestMethod]
        public void ShouldVerifyIfAValueCanBeFormatted()
        {
            Assert.IsFalse(formatter.CanBeFormatted("26.637.142/0001-58"));
            Assert.IsTrue(formatter.CanBeFormatted("26637142000158"));
            Assert.IsFalse(formatter.CanBeFormatted("26.7.1x2/00a1-58"));
        }
    }
}
