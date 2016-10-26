using Microsoft.VisualStudio.TestTools.UnitTesting;
using nStella.Core.Inwords;
using System;

namespace nStella.Core.Tests.Inwords
{
    [TestClass]
    public class RealNumericToWordsConverterTest
    {
        private readonly NumericToWordsConverter converter = new NumericToWordsConverter(new FormatoDeReal());

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldTransformNegativeLong()
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
            double one = 1;
            string actual = converter.ToWords(one);
            Assert.AreEqual("um real", actual);
        }

        [TestMethod]
        public void ShouldTransformLongOneInWords()
        {
            long one = 1;
            string actual = converter.ToWords(one);
            Assert.AreEqual("um real", actual);
        }

        [TestMethod]
        public void ShouldTransformDoubleOneWithCentsInWords()
        {
            double val = 1.65;
            string actual = converter.ToWords(val);
            Assert.AreEqual("um real e sessenta e cinco centavos", actual);
        }

        [TestMethod]
        public void ShouldTransform2InWords()
        {
            double two = 2;
            string actual = converter.ToWords(two);
            Assert.AreEqual("dois reais", actual);
        }

        [TestMethod]
        public void ShouldTransform14InWords()
        {
            double fourteen = 14;
            string actual = converter.ToWords(fourteen);
            Assert.AreEqual("quatorze reais", actual);
        }

        [TestMethod]
        public void ShouldTransformLong15InWords()
        {
            long fifteen = 15;
            string actual = converter.ToWords(fifteen);
            Assert.AreEqual("quinze reais", actual);
        }

        [TestMethod]
        public void ShouldTransform53InWordsUsingAnd()
        {
            double fiftyThree = 53;
            string actual = converter.ToWords(fiftyThree);
            Assert.AreEqual("cinquenta e três reais", actual);
        }

        [TestMethod]
        public void shouldTransformOneHundredInWords()
        {
            double oneHundred = 100;
            string actual = converter.ToWords(oneHundred);
            Assert.AreEqual("cem reais", actual);
        }

        [TestMethod]
        public void shouldTransformOneHundredInWordsUsingAnd()
        {
            double oneHundredAndNine = 109;
            string actual = converter.ToWords(oneHundredAndNine);
            Assert.AreEqual("cento e nove reais", actual);
        }

        [TestMethod]
        public void shouldTransformTwoHundredInWords()
        {
            double twoHundred = 200;
            string actual = converter.ToWords(twoHundred);
            Assert.AreEqual("duzentos reais", actual);
        }

        [TestMethod]
        public void shouldTransformThousandInWords()
        {
            double thousand = 1000;
            string actual = converter.ToWords(thousand);
            Assert.AreEqual("um mil reais", actual);
        }

        [TestMethod]
        public void shouldTransformThousandInWordsUsingAnd()
        {
            double thousand = 1031;
            string actual = converter.ToWords(thousand);
            Assert.AreEqual("um mil e trinta e um reais", actual);
        }

        [TestMethod]
        public void ShouldTransformAMillionIntoNumberInWords()
        {
            double oneMillion = 1000000;
            string actual = converter.ToWords(oneMillion);
            Assert.AreEqual("um milhão de reais", actual);
        }

        [TestMethod]
        public void ShouldTransformAMillionIntoNumberInWordsUsingAnd()
        {
            double twoHundred = 1000150.99;
            string actual = converter.ToWords(twoHundred);
            Assert.AreEqual("um milhão e cento e cinquenta reais e noventa e nove centavos", actual);
        }

        [TestMethod]
        public void ShouldTransformAMillionAndThousandIntoNumberInWordsUsingAnd()
        {
            double twoHundred = 1023850;
            string actual = converter.ToWords(twoHundred);
            Assert.AreEqual("um milhão, vinte e três mil e oitocentos e cinquenta reais", actual);
        }

        [TestMethod]
        public void ShouldTransformANumberInWordsUsingFraction()
        {
            double twoHundred = 0.22;
            string actual = converter.ToWords(twoHundred);
            Assert.AreEqual("vinte e dois centavos", actual);
        }

        [TestMethod]
        public void ShouldTransformOneDecimalWords()
        {
            double oneDecimal = 0.1;
            string actual = converter.ToWords(oneDecimal);
            Assert.AreEqual("dez centavos", actual);
        }

        [TestMethod]
        public void ShouldRoundAndTransformANumberInWordsUsingFraction()
        {
            double twoHundred = 0.229;
            string words = converter.ToWords(twoHundred);
            Assert.AreEqual("vinte e três centavos", words);
        }

        [TestMethod]
        public void ShouldTransformAThousandAndOne()
        {
            double number = 1001;
            string words = converter.ToWords(number);
            Assert.AreEqual("um mil e um reais", words);
        }

        [TestMethod]
        public void ShouldTransformAMillionAndOne()
        {
            double number = 1000001;
            string words = converter.ToWords(number);
            Assert.AreEqual("um milhão e um reais", words);
        }

        [TestMethod]
        public void ShouldTransformABillion()
        {
            double number = 1E9;
            string words = converter.ToWords(number);
            Assert.AreEqual("um bilhão de reais", words);
        }

        [TestMethod]
        public void ShouldTransform1E12()
        {
            double number = 1E12;
            string words = converter.ToWords(number);
            Assert.AreEqual("um trilhão de reais", words);
        }

        [TestMethod]
        public void ShouldTransform1E15()
        {
            double number = 1E15;
            string words = converter.ToWords(number);
            Assert.AreEqual("um quatrilhão de reais", words);
        }

        [TestMethod]
        public void ShouldTransform1000000000000000001AsLong()
        {
            long number = (1000000000000000001L);
            string words = converter.ToWords(number);
            Assert.AreEqual("um quintilhão e um reais", words);
        }

        [TestMethod]
        public void ShouldTransform999999999999999L()
        {
            long number = 999999999999999L;
            string words = converter.ToWords(number);
            Assert.AreEqual("novecentos e noventa e nove trilhões, " + "novecentos e noventa e nove bilhões, "
                    + "novecentos e noventa e nove milhões, " + "novecentos e noventa e nove mil e "
                    + "novecentos e noventa e nove reais", words);
        }
    }
}