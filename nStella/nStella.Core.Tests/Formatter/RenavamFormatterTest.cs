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
    public class RenavamFormatterTest
    {
        private readonly IFormatter formatter = new RenavamFormatter();

        [TestMethod]
        public void ShouldFormatAnUnformattedRenavam()
        {
            string formattedRenavam = formatter.Format("00736407677");
            Assert.AreEqual("0073.640767-7", formattedRenavam);
        }

        [TestMethod]
        public void ShouldUnformatAFormattedRenavam()
        {
            string unformattedRenavam = formatter.UnFormat("73.640767-7");
            Assert.AreEqual("736407677", unformattedRenavam);
        }

        [TestMethod]
        public void ShouldVerifyIfAValueIsFormattedOrNot()
        {
            Assert.IsTrue(formatter.IsFormatted("73.640767-7"));
            Assert.IsTrue(formatter.IsFormatted("0073.640767-7"));
            Assert.IsFalse(formatter.IsFormatted("736407677"));
            Assert.IsFalse(formatter.IsFormatted("73.x407a7-7"));
        }

        [TestMethod]
        public void ShouldVerifyIfAValueCanBeFormattedOrNot()
        {
            Assert.IsFalse(formatter.CanBeFormatted("73.640767-7"));
            Assert.IsFalse(formatter.CanBeFormatted("0073.640767-7"));
            Assert.IsTrue(formatter.CanBeFormatted("736407677"));
            Assert.IsTrue(formatter.CanBeFormatted("00736407677"));
            Assert.IsFalse(formatter.CanBeFormatted("73.x407a7-7"));
        }
    }
}