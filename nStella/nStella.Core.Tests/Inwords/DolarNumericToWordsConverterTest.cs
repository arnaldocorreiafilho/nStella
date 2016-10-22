using Microsoft.VisualStudio.TestTools.UnitTesting;
using nStella.Core.Inwords;
using System;
using System.Globalization;

namespace nStella.Core.Tests.Inwords
{
    [TestClass]
    public class DolarNumericToWordsConverterTest
    {
        private readonly NumericToWordsConverter converter = new NumericToWordsConverter(new FormatoDeDolar(), new CultureInfo("en-US"));

        [TestMethod]
        public void ShouldTransform0InWords()
        {
            double zero = 0;
            string actual = converter.ToWords(zero);
            Assert.AreEqual("zero", actual);
        }
    }
}