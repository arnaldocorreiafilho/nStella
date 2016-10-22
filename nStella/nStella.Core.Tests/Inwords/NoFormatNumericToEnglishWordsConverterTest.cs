using Microsoft.VisualStudio.TestTools.UnitTesting;
using nStella.Core.Inwords;
using System;
using System.Globalization;

namespace nStella.Core.Tests.Inwords
{
    [TestClass]
    public class NoFormatNumericToEnglishWordsConverterTest
    {
        private readonly NumericToWordsConverter converter = new NumericToWordsConverter(new InteiroSemFormato(), new CultureInfo("en-US"));

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldTransformNegativXeLong()
        {
            long negative = -1;
            converter.ToWords(negative);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldNotTransformNegativeDouble()
        {
            double negative = -1;
            converter.ToWords(negative);
        }

        [TestMethod]
        public void ShouldTransformMaxLong()
        {
            long max = long.MaxValue;
            string actual = converter.ToWords(max);
            Assert.AreEqual("nine quintillion, two hundred and twenty-three quadrillion,"
                    + " three hundred and seventy-two trillion, thirty-six billion,"
                    + " eight hundred and fifty-four million, seven hundred and seventy-five thousand and"
                    + " eight hundred and seven", actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldNotTransformAMissingResource()
        {
            double max = Double.MaxValue;
            converter.ToWords(max);
        }

        [TestMethod]
        public void ShouldTransform0InWords()
        {
            double zero = 0;
            string actual = converter.ToWords(zero);
            Assert.AreEqual("zero", actual);
        }

        [TestMethod]
        public void ShouldTransform1InWords()
        {
            double one = 1;
            string actual = converter.ToWords(one);
            Assert.AreEqual("one", actual);
        }

        [TestMethod]
        public void ShouldTransform2InWords()
        {
            double two = 2;
            string actual = converter.ToWords(two);
            Assert.AreEqual("two", actual);
        }

        [TestMethod]
        public void ShouldTransform14InWords()
        {
            double fourteen = 14;
            string actual = converter.ToWords(fourteen);
            Assert.AreEqual("fourteen", actual);
        }

        [TestMethod]
        public void ShouldTransform53InWordsUsingAnd()
        {
            double fiftyThree = 53;
            string actual = converter.ToWords(fiftyThree);
            Assert.AreEqual("fifty-three", actual);
        }

        [TestMethod]
        public void ShouldTransform99InWordsUsingAnd()
        {
            double ninetyNine = 99;
            string actual = converter.ToWords(ninetyNine);
            Assert.AreEqual("ninety-nine", actual);
        }

        [TestMethod]
        public void ShouldTransformOneHundredInWords()
        {
            double oneHundred = 100;
            string actual = converter.ToWords(oneHundred);
            Assert.AreEqual("one hundred", actual);
        }

        [TestMethod]
        public void ShouldTransform101InWordsUsingAnd()
        {
            double oneHundredAndOne = 101;
            string actual = converter.ToWords(oneHundredAndOne);
            Assert.AreEqual("one hundred and one", actual);
        }

        [TestMethod]
        public void ShouldTransform199InWordsUsingAnd()
        {
            double oneHundredAndNinetyNine = 199;
            string actual = converter.ToWords(oneHundredAndNinetyNine);
            Assert.AreEqual("one hundred and ninety-nine", actual);
        }

        [TestMethod]
        public void ShouldTransform200InWords()
        {
            double twoHundred = 200;
            string actual = converter.ToWords(twoHundred);
            Assert.AreEqual("two hundred", actual);
        }

        [TestMethod]
        public void ShouldTransform201InWords()
        {
            double twoHundredAndOne = 201;
            string actual = converter.ToWords(twoHundredAndOne);
            Assert.AreEqual("two hundred and one", actual);
        }

        [TestMethod]
        public void ShouldTransform999InWords()
        {
            double nineHundredNinetyNine = 999;
            string actual = converter.ToWords(nineHundredNinetyNine);
            Assert.AreEqual("nine hundred and ninety-nine", actual);
        }

        [TestMethod]
        public void ShouldTransformThousandInWords()
        {
            double thousand = 1000;
            string actual = converter.ToWords(thousand);
            Assert.AreEqual("one thousand", actual);
        }

        [TestMethod]
        public void ShouldTransform1001InWords()
        {
            double oneThousandAndOne = 1001;
            string actual = converter.ToWords(oneThousandAndOne);
            Assert.AreEqual("one thousand and one", actual);
        }

        [TestMethod]
        public void ShouldTransformThousandInWordsUsingAnd()
        {
            double thousand = 1031;
            string actual = converter.ToWords(thousand);
            Assert.AreEqual("one thousand and thirty-one", actual);
        }

        [TestMethod]
        public void ShouldTransformAMillionIntoNumberInWordsUsingSingular()
        {
            double oneMillion = 1000000;
            string actual = converter.ToWords(oneMillion);
            Assert.AreEqual("one million", actual);
        }

        [TestMethod]
        public void ShouldTransformAMillionIntoNumberInWordsUsingAnd()
        {
            double twoHundred = 1000150.99;
            string actual = converter.ToWords(twoHundred);
            Assert.AreEqual("one million and one hundred and fifty-one", actual);
        }

        [TestMethod]
        public void ShouldTransformAMillionAndThousandIntoNumberInWordsUsingAnd()
        {
            double twoHundred = 1023850;
            string actual = converter.ToWords(twoHundred);
            Assert.AreEqual("one million, twenty-three thousand and eight hundred and fifty", actual);
        }

        [TestMethod]
        public void ShouldTransformTwoMillionUsingPlural()
        {
            double twoHundred = 2e6;
            string actual = converter.ToWords(twoHundred);
            Assert.AreEqual("two million", actual);
        }

        [TestMethod]
        public void ShouldTransformANumberInWordsUsingFraction()
        {
            double twoHundred = 222;
            string actual = converter.ToWords(twoHundred);
            Assert.AreEqual("two hundred and twenty-two", actual);
        }

        [TestMethod]
        public void ShouldTransform1E21()
        {
            double number = 1E21;
            string actual = converter.ToWords(number);
            Assert.AreEqual("one sextillion", actual);
        }

        [TestMethod]
        public void ShouldTransform2E21()
        {
            double number = 2E21;
            string actual = converter.ToWords(number);
            Assert.AreEqual("two sextillion", actual);
        }

        [TestMethod]
        public void ShouldTransform1E24()
        {
            double number = 1E24;
            string actual = converter.ToWords(number);
            Assert.AreEqual("one septillion", actual);
        }

        [TestMethod]
        public void ShouldTransform2E24()
        {
            double number = 2E24;
            string actual = converter.ToWords(number);
            Assert.AreEqual("two septillion", actual);
        }

        [TestMethod]
        public void ShouldTransform1E27()
        {
            double number = 1E27;
            string actual = converter.ToWords(number);
            Assert.AreEqual("one octillion", actual);
        }

        [TestMethod]
        public void ShouldTransform2E27()
        {
            double number = 2E27;
            string actual = converter.ToWords(number);
            Assert.AreEqual("two octillion", actual);
        }

        [TestMethod]
        public void ShouldTransform1E30()
        {
            double number = 1E30;
            string actual = converter.ToWords(number);
            Assert.AreEqual("one nonillion", actual);
        }

        [TestMethod]
        public void ShouldTransform2E30()
        {
            double number = 2E30;
            string actual = converter.ToWords(number);
            Assert.AreEqual("two nonillion", actual);
        }

        [TestMethod]
        public void ShouldTransform1E33()
        {
            double number = 1E33;
            string actual = converter.ToWords(number);
            Assert.AreEqual("one decillion", actual);
        }

        [TestMethod]
        public void ShouldTransform2E33()
        {
            double number = 2E33;
            string actual = converter.ToWords(number);
            Assert.AreEqual("two decillion", actual);
        }

        [TestMethod]
        public void ShouldTransform1E36()
        {
            double number = 1E36;
            string actual = converter.ToWords(number);
            Assert.AreEqual("one undecillion", actual);
        }

        [TestMethod]
        public void ShouldTransform2E36()
        {
            double number = 2E36;
            string actual = converter.ToWords(number);
            Assert.AreEqual("two undecillion", actual);
        }

        [TestMethod]
        public void ShouldTransform1E39()
        {
            double number = 1E39;
            string actual = converter.ToWords(number);
            Assert.AreEqual("one duodecillion", actual);
        }

        [TestMethod]
        public void ShouldTransform2E39()
        {
            double number = 2E39;
            string actual = converter.ToWords(number);
            Assert.AreEqual("two duodecillion", actual);
        }

        [TestMethod]
        public void ShouldTransform1E42()
        {
            double number = 1E42;
            string actual = converter.ToWords(number);
            Assert.AreEqual("one tredecillion", actual);
        }

        [TestMethod]
        public void ShouldTransform2E42()
        {
            double number = 2E42;
            string actual = converter.ToWords(number);
            Assert.AreEqual("two tredecillion", actual);
        }
    }
}