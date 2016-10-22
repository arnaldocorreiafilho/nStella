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

        [TestMethod]
        public void ShouldTransformLongZeroInWords()
        {
            long zero = 0;
            string actual = converter.ToWords(zero);
            Assert.AreEqual("zero", actual);
        }

        [TestMethod]
        public void ShouldTransform1InWords()
        {
            double one = 1.0;
            string actual = converter.ToWords(one);
            Assert.AreEqual("one dollar", actual);
        }

        [TestMethod]
        public void ShouldTransformLongOneInWords()
        {
            long one = 1L;
            string actual = converter.ToWords(one);
            Assert.AreEqual("one dollar", actual);
        }

        [TestMethod]
        public void ShouldTransformOneCentInWords()
        {
            double val = 0.01;
            string actual = converter.ToWords(val);
            Assert.AreEqual("one cent", actual);
        }

        [TestMethod]
        public void ShouldTransformDoubleOneWithCentsInWords()
        {
            double val = 1.65;
            string actual = converter.ToWords(val);
            Assert.AreEqual("one dollar and sixty-five cents", actual);
        }

        [TestMethod]
        public void ShouldTransform2InWords()
        {
            double two = 2;
            string actual = converter.ToWords(two);
            Assert.AreEqual("two dollars", actual);
        }

        [TestMethod]
        public void ShouldTransform14InWords()
        {
            double fourteen = 14;
            string actual = converter.ToWords(fourteen);
            Assert.AreEqual("fourteen dollars", actual);
        }

        [TestMethod]
        public void ShouldTransform53InWordsUsingAnd()
        {
            double fiftyThree = 53;
            string actual = converter.ToWords(fiftyThree);
            Assert.AreEqual("fifty-three dollars", actual);
        }

        [TestMethod]
        public void ShouldTransformOneHundredInWords()
        {
            double oneHundred = 100;
            string actual = converter.ToWords(oneHundred);
            Assert.AreEqual("one hundred dollars", actual);
        }

        [TestMethod]
        public void ShouldTransformOneHundredInWordsUsingAnd()
        {
            double oneHundredAndNine = 109;
            string actual = converter.ToWords(oneHundredAndNine);
            Assert.AreEqual("one hundred and nine dollars", actual);
        }

        [TestMethod]
        public void ShouldTransformTwoHundredInWords()
        {
            double twoHundred = 200;
            string actual = converter.ToWords(twoHundred);
            Assert.AreEqual("two hundred dollars", actual);
        }

        [TestMethod]
        public void ShouldTransformThousandInWords()
        {
            double thousand = 1000;
            string actual = converter.ToWords(thousand);
            Assert.AreEqual("one thousand dollars", actual);
        }

        [TestMethod]
        public void ShouldTransformThousandInWordsUsingAnd()
        {
            double thousand = 1031;
            string actual = converter.ToWords(thousand);
            Assert.AreEqual("one thousand and thirty-one dollars", actual);
        }

        [TestMethod]
        public void ShouldTransformAMillionIntoNumberInWords()
        {
            double oneMillion = 1000000;
            string actual = converter.ToWords(oneMillion);
            Assert.AreEqual("one million dollars", actual);
        }

        [TestMethod]
        public void ShouldTransformAMillionIntoNumberInWordsUsingAnd()
        {
            double twoHundred = 1000150.99;
            string actual = converter.ToWords(twoHundred);
            Assert.AreEqual("one million and one hundred and fifty dollars and ninety-nine cents", actual);
        }

        [TestMethod]
        public void ShouldTransformAMillionAndThousandIntoNumberInWordsUsingAnd()
        {
            double twoHundred = 1023850;
            string actual = converter.ToWords(twoHundred);
            Assert.AreEqual("one million, twenty-three thousand and eight hundred and fifty dollars", actual);
        }

        [TestMethod]
        public void ShouldTransformANumberInWordsUsingFraction()
        {
            double twoHundred = 0.22;
            string actual = converter.ToWords(twoHundred);
            Assert.AreEqual("twenty-two cents", actual);
        }

        [TestMethod]
        public void ShouldTransformOneDecimalWords()
        {
            double oneDecimal = 0.1;
            string actual = converter.ToWords(oneDecimal);
            Assert.AreEqual("ten cents", actual);
        }

        [TestMethod]
        public void ShouldRoundAndTransformANumberInWordsUsingFraction()
        {
            double twoHundred = 0.229;
            string words = converter.ToWords(twoHundred);
            Assert.AreEqual("twenty-three cents", words);
        }

        [TestMethod]
        public void ShouldTransformAThousandAndOne()
        {
            double number = 1001;
            string words = converter.ToWords(number);
            Assert.AreEqual("one thousand and one dollars", words);
        }

        [TestMethod]
        public void ShouldTransformAMillionAndOne()
        {
            double number = 1000001;
            string words = converter.ToWords(number);
            Assert.AreEqual("one million and one dollars", words);
        }

        [TestMethod]
        public void ShouldTransformABillion()
        {
            double number = 1E9;
            string words = converter.ToWords(number);
            Assert.AreEqual("one billion dollars", words);
        }

        [TestMethod]
        public void ShouldTransform1000000000000000001AsLong()
        {
            long number = (1000000000000000001L);
            string words = converter.ToWords(number);
            Assert.AreEqual("one quintillion and one dollars", words);
        }

        [TestMethod]
        public void ShouldTransform999999999999999L()
        {
            long number = 999999999999999L;
            string words = converter.ToWords(number);
            Assert.AreEqual("nine hundred and ninety-nine trillion, " + "nine hundred and ninety-nine billion, "
                    + "nine hundred and ninety-nine million, " + "nine hundred and ninety-nine thousand and "
                    + "nine hundred and ninety-nine dollars", words);
        }
    }
}