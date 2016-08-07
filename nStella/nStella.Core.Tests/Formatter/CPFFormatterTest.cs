using Microsoft.VisualStudio.TestTools.UnitTesting;
using nStella.Core.Format;
using System;

namespace nStella.Core.Tests.Formatter
{
    [TestClass]
    public class CPFFormatterTest
    {
        private IFormatter formatter;

        [TestInitialize]
        public void Init ()
        {
            formatter = new CPFFormatter();
        }

        [TestMethod]
        public void testFormat()
        {
            string unformattedValue = "11122233344";
            string formattedValue = formatter.Format(unformattedValue);
            Assert.AreEqual(formattedValue, "111.222.333-44");            
        }

        [TestMethod]
        public void testUnformat()
        {
            string formattedValue = "111.222.333-44";
            string unformattedValue = formatter.UnFormat(formattedValue);
            Assert.AreEqual(unformattedValue, "11122233344");
        }

        [TestMethod]
        public void shouldDetectIfAValueIsFormattedOrNot()
        {

            Assert.IsTrue(formatter.IsFormatted("111.222.333-44"));
            Assert.IsFalse(formatter.IsFormatted("11122233344"));
            Assert.IsFalse(formatter.IsFormatted("1.1a1.1-2"));
        }

        [TestMethod]
        public void shouldDetectIfAValueCanBeFormattedOrNot() 
        {            
            Assert.IsFalse(formatter.CanBeFormatted("111.222.333-44"));
            Assert.IsTrue(formatter.CanBeFormatted("11122233344"));
            Assert.IsFalse(formatter.CanBeFormatted("1.1a1.1-2"));
        }

    }
}
