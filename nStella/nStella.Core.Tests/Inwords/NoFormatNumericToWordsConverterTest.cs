using Microsoft.VisualStudio.TestTools.UnitTesting;
using nStella.Core.Inwords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Tests.Inwords
{
    [TestClass]
    public class NoFormatNumericToWordsConverterTest
    {
        private readonly NumericToWordsConverter converter = new NumericToWordsConverter(new InteiroSemFormato());

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
            Assert.AreEqual("nove quintilhões, duzentos e vinte e três quatrilhões,"
                    + " trezentos e setenta e dois trilhões, trinta e seis bilhões,"
                    + " oitocentos e cinquenta e quatro milhões, setecentos e setenta"
                    + " e cinco mil e oitocentos e sete", actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldNotTransformAMissingResource()
        {
            double max = double.MaxValue;
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
            Assert.AreEqual("um", actual);
        }

        [TestMethod]
        public void ShouldTransform2InWords()
        {
            double two = 2;
            string actual = converter.ToWords(two);
            Assert.AreEqual("dois", actual);
        }

        [TestMethod]
        public void ShouldTransform14InWords()
        {
            double fourteen = 14;
            string actual = converter.ToWords(fourteen);
            Assert.AreEqual("quatorze", actual);
        }

        [TestMethod]
        public void ShouldTransform53InWordsUsingAnd()
        {
            double fiftyThree = 53;
            string actual = converter.ToWords(fiftyThree);
            Assert.AreEqual("cinquenta e três", actual);
        }

        [TestMethod]
        public void ShouldTransform99InWordsUsingAnd()
        {
            double ninetyNine = 99;
            string actual = converter.ToWords(ninetyNine);
            Assert.AreEqual("noventa e nove", actual);
        }

        [TestMethod]
        public void ShouldTransformOneHundredInWords()
        {
            double oneHundred = 100;
            string actual = converter.ToWords(oneHundred);
            Assert.AreEqual("cem", actual);
        }

        [TestMethod]
        public void ShouldTransform101InWordsUsingAnd()
        {
            double oneHundredAndOne = 101;
            string actual = converter.ToWords(oneHundredAndOne);
            Assert.AreEqual("cento e um", actual);
        }

        [TestMethod]
        public void ShouldTransform199InWordsUsingAnd()
        {
            double oneHundredAndNinetyNine = 199;
            string actual = converter.ToWords(oneHundredAndNinetyNine);
            Assert.AreEqual("cento e noventa e nove", actual);
        }

        [TestMethod]
        public void ShouldTransform200InWords()
        {
            double twoHundred = 200;
            string actual = converter.ToWords(twoHundred);
            Assert.AreEqual("duzentos", actual);
        }

        [TestMethod]
        public void ShouldTransform201InWords()
        {
            double twoHundredAndOne = 201;
            string actual = converter.ToWords(twoHundredAndOne);
            Assert.AreEqual("duzentos e um", actual);
        }

        [TestMethod]
        public void ShouldTransform999InWords()
        {
            double nineHundredNinetyNine = 999;
            string actual = converter.ToWords(nineHundredNinetyNine);
            Assert.AreEqual("novecentos e noventa e nove", actual);
        }

        [TestMethod]
        public void ShouldTransformThousandInWords()
        {
            double thousand = 1000;
            string actual = converter.ToWords(thousand);
            Assert.AreEqual("um mil", actual);
        }

        [TestMethod]
        public void ShouldTransform1001InWords()
        {
            double oneThousandAndOne = 1001;
            string actual = converter.ToWords(oneThousandAndOne);
            Assert.AreEqual("um mil e um", actual);
        }

        [TestMethod]
        public void ShouldTransformThousandInWordsUsingAnd()
        {
            double thousand = 1031;
            string actual = converter.ToWords(thousand);
            Assert.AreEqual("um mil e trinta e um", actual);
        }

        [TestMethod]
        public void ShouldTransformAMillionIntoNumberInWordsUsingSingular()
        {
            double oneMillion = 1000000;
            string actual = converter.ToWords(oneMillion);
            Assert.AreEqual("um milhão", actual);
        }

        [TestMethod]
        public void ShouldTransformAMillionIntoNumberInWordsUsingAnd()
        {
            double twoHundred = 1000150.99;
            string actual = converter.ToWords(twoHundred);
            Assert.AreEqual("um milhão e cento e cinquenta e um", actual);
        }

        [TestMethod]
        public void ShouldTransformAMillionAndThousandIntoNumberInWordsUsingAnd()
        {
            double twoHundred = 1023850;
            string actual = converter.ToWords(twoHundred);
            Assert.AreEqual("um milhão, vinte e três mil e oitocentos e cinquenta", actual);
        }

        [TestMethod]
        public void ShouldTransformTwoMillionUsingPlural()
        {
            double twoHundred = 2e6;
            string actual = converter.ToWords(twoHundred);
            Assert.AreEqual("dois milhões", actual);
        }

        [TestMethod]
        public void ShouldTransformANumberInWordsUsingFraction()
        {
            double twoHundred = 222;
            string actual = converter.ToWords(twoHundred);
            Assert.AreEqual("duzentos e vinte e dois", actual);
        }

        [TestMethod]
        public void ShouldTransform1E21()
        {
            double number = 1E21;
            string actual = converter.ToWords(number);
            Assert.AreEqual("um sextilhão", actual);
        }

        [TestMethod]
        public void ShouldTransform2E21()
        {
            double number = 2E21;
            string actual = converter.ToWords(number);
            Assert.AreEqual("dois sextilhões", actual);
        }

        [TestMethod]
        public void ShouldTransform1E24()
        {
            double number = 1E24;
            string actual = converter.ToWords(number);
            Assert.AreEqual("um septilhão", actual);
        }

        [TestMethod]
        public void ShouldTransform2E24()
        {
            double number = 2E24;
            string actual = converter.ToWords(number);
            Assert.AreEqual("dois septilhões", actual);
        }

        [TestMethod]
        public void ShouldTransform1E27()
        {
            double number = 1E27;
            string actual = converter.ToWords(number);
            Assert.AreEqual("um octilhão", actual);
        }

        [TestMethod]
        public void ShouldTransform2E27()
        {
            double number = 2E27;
            string actual = converter.ToWords(number);
            Assert.AreEqual("dois octilhões", actual);
        }

        [TestMethod]
        public void ShouldTransform1E30()
        {
            double number = 1E30;
            string actual = converter.ToWords(number);
            Assert.AreEqual("um nonilhão", actual);
        }

        [TestMethod]
        public void ShouldTransform2E30()
        {
            double number = 2E30;
            string actual = converter.ToWords(number);
            Assert.AreEqual("dois nonilhões", actual);
        }

        [TestMethod]
        public void ShouldTransform1E33()
        {
            double number = 1E33;
            string actual = converter.ToWords(number);
            Assert.AreEqual("um decilhão", actual);
        }

        [TestMethod]
        public void ShouldTransform2E33()
        {
            double number = 2E33;
            string actual = converter.ToWords(number);
            Assert.AreEqual("dois decilhões", actual);
        }

        [TestMethod]
        public void ShouldTransform1E36()
        {
            double number = 1E36;
            string actual = converter.ToWords(number);
            Assert.AreEqual("um undecilhão", actual);
        }

        [TestMethod]
        public void ShouldTransform2E36()
        {
            double number = 2E36;
            string actual = converter.ToWords(number);
            Assert.AreEqual("dois undecilhões", actual);
        }

        [TestMethod]
        public void ShouldTransform1E39()
        {
            double number = 1E39;
            string actual = converter.ToWords(number);
            Assert.AreEqual("um doudecilhão", actual);
        }

        [TestMethod]
        public void ShouldTransform2E39()
        {
            double number = 2E39;
            string actual = converter.ToWords(number);
            Assert.AreEqual("dois doudecilhões", actual);
        }

        [TestMethod]
        public void ShouldTransform1E42()
        {
            double number = 1E42;
            string actual = converter.ToWords(number);
            Assert.AreEqual("um tredecilhão", actual);
        }

        [TestMethod]
        public void ShouldTransform2E42()
        {
            double number = 2E42;
            string actual = converter.ToWords(number);
            Assert.AreEqual("dois tredecilhões", actual);
        }
    }
}
