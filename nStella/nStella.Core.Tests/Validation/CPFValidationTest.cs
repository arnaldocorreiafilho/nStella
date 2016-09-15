using Microsoft.VisualStudio.TestTools.UnitTesting;
using nStella.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Tests.Validation
{
    [TestClass]
    public class CPFValidationTest
    {
        private static readonly string INVALID_FORMAT = "INVALID FORMAT";
        private static readonly string REPEATED_DIGITS = "REPEATED DIGITS";
        private static readonly string INVALID_CHECK_DIGITS = "INVALID CHECK DIGITS";
        private static readonly string INVALID_DIGITS = "INVALID DIGITS";
        private readonly string validString = "248.438.034-80";
        private readonly string validStringNotFormatted = "24843803480";
        private readonly string firstCheckDigitWrongNotFormatted = "24843803470";
        private CPFValidator validator;

        [TestInitialize]
        public void Init()
        {
            validator = new CPFValidator();
        }

        [TestMethod]
        public void shouldHaveDefaultConstructorThatUsesSimpleMessageProducerAndAssumesThatStringIsNotFormatted()
        {
            new CPFValidator().AssertValid(validStringNotFormatted);

            try
            {
                new CPFValidator().AssertValid(firstCheckDigitWrongNotFormatted);
                Assert.Fail("Test expected to throw exception");
            }
            catch (InvalidStateException e)
            {
                InvalidStateException invalidStateException = e;
                assertMessage(invalidStateException, INVALID_CHECK_DIGITS);
            }
        }

        private void assertMessage(InvalidStateException invalidStateException, string expected)
        {
            Assert.IsTrue(invalidStateException.GetInvalidMessages()[0].GetMessage().Contains(expected));
        }

        [TestMethod]
        public void shouldNotValidateCPFWithInvalidCharacter()
        {
            try
            {
                validator.AssertValid("1111111a111");
                Assert.Fail();
            }
            catch (InvalidStateException e)
            {
                Assert.IsTrue(e.GetInvalidMessages().Count == 1);
                assertMessage(e, INVALID_DIGITS);
            }
        }

        [TestMethod]
        public void shouldNotValidateCPFWithLessDigitsThanAllowed()
        {
            try
            {
                validator.AssertValid("1234567890");
                Assert.Fail();
            }
            catch (InvalidStateException e)
            {
                Assert.IsTrue(e.GetInvalidMessages().Count == 1);
                assertMessage(e, INVALID_DIGITS);
            }
        }

        [TestMethod]
        public void shouldNotValidateCPFWithMoreDigitsThanAlowed()
        {
            try
            {
                string value = "123456789012";
                validator.AssertValid(value);
                Assert.Fail();
            }
            catch (InvalidStateException e)
            {
                Assert.IsTrue(e.GetInvalidMessages().Count == 1);
                assertMessage(e, INVALID_DIGITS);
            }
        }

        [TestMethod]
        public void shouldNotValidateCPFCheckDigitsWithFirstCheckDigitWrong()
        {
            // VALID CPF = 248.438.034-80
            try
            {
                string value = "24843803470";
                validator.AssertValid(value);
                Assert.Fail();
            }
            catch (InvalidStateException e)
            {
                Assert.IsTrue(e.GetInvalidMessages().Count == 1);
                assertMessage(e, INVALID_CHECK_DIGITS);
            }
        }

        [TestMethod]
        public void shouldNotValidateCPFCheckDigitsWithSecondCheckDigitWrong()
        {
            // VALID CPF = 099.075.865-60
            try
            {
                string value = "09907586561";
                validator.AssertValid(value);
                Assert.Fail();
            }
            catch (InvalidStateException e)
            {
                Assert.IsTrue(e.GetInvalidMessages().Count == 1);
                assertMessage(e, INVALID_CHECK_DIGITS);
            }
        }

        [TestMethod]
        public void shouldNeverThrowsNPE()
        {
            Assert.IsFalse(validator.IsEligible(null));
        }

        [TestMethod]
        public void shouldValidateValidCPF()
        {
            validator.AssertValid("11144477735");
            validator.AssertValid("88641577947");
            validator.AssertValid("34608514300");
            validator.AssertValid("47393545608");
        }

        [TestMethod]
        public void shouldValidateNullCPF()
        {
            string value = null;
            validator.AssertValid(value);
        }

        [TestMethod]
        public void shouldValidateCPFWithLeadingZeros()
        {
            string value = "01169538452";
            validator.AssertValid(value);
        }

        [TestMethod]
        public void shouldNotValidateCPFWithAllRepeatedDigitsWhenNotIgnoringIt()
        {
            CPFValidator validator = new CPFValidator(false, false);
            try
            {
                string value = "44444444444";
                validator.AssertValid(value);
                Assert.Fail();
            }
            catch (InvalidStateException e)
            {
                Assert.IsTrue(e.GetInvalidMessages().Count == 1);
                assertMessage(e, REPEATED_DIGITS);
            }
        }

        [TestMethod]
        public void shouldNotValidateCPFWithAllRepeatedDigitsByDefault()
        {
            try
            {
                string value = "44444444444";
                validator.AssertValid(value);
                Assert.Fail();
            }
            catch (InvalidStateException e)
            {
                Assert.IsTrue(e.GetInvalidMessages().Count == 1);
                assertMessage(e, REPEATED_DIGITS);
            }
        }

        [TestMethod]
        public void shouldValidateCPFWithAllRepeatedDigitsWhenIgnoringIt()
        {
            CPFValidator validator = new CPFValidator(false, true);
            string value = "44444444444";
            validator.AssertValid(value);
        }

        [TestMethod]
        public void shouldValidateValidFormattedCPF()
        {
            CPFValidator validator = new CPFValidator(true);
            // VALID CPF = 356.296.825-63
            string value = "356.296.825-63";
            validator.AssertValid(value);
        }

        [TestMethod]
        public void shouldNotValidateValidUnformattedCPF()
        {
            CPFValidator validator = new CPFValidator(true);
            // VALID CPF = 332.375.322-40
            try
            {
                string value = "33237532240";
                validator.AssertValid(value);
                Assert.Fail();
            }
            catch (InvalidStateException e)
            {
                Assert.IsTrue(e.GetInvalidMessages().Count == 1);
                assertMessage(e, INVALID_FORMAT);
            }
        }

        [TestMethod]
        public void shouldBeEligibleDefaultConstructor()
        {
            CPFValidator cpfValidator = new CPFValidator();
            Assert.IsTrue(cpfValidator.IsEligible(validStringNotFormatted));
            Assert.IsFalse(cpfValidator.IsEligible(validString));
        }

        [TestMethod]
        public void shouldBeEligibleConstructorNotFormatted()
        {
            CPFValidator cpfValidator = new CPFValidator(false);
            Assert.IsTrue(cpfValidator.IsEligible(validStringNotFormatted));
            Assert.IsFalse(cpfValidator.IsEligible(validString));
        }

        [TestMethod]
        public void shouldBeEligibleConstructorFormatted()
        {
            CPFValidator cpfValidator = new CPFValidator(true);
            Assert.IsFalse(cpfValidator.IsEligible(validStringNotFormatted));
            Assert.IsTrue(cpfValidator.IsEligible(validString));
        }

        [TestMethod]
        public void ShouldGenerateValidFormattedCPF()
        {
            CPFValidator cpfValidator = new CPFValidator(true);
            string generated = cpfValidator.GenerateRandomValid();
            cpfValidator.AssertValid(generated);
        }

        [TestMethod]
        public void shouldGenerateValidUnformattedCPF()
        {
            CPFValidator cpfValidator = new CPFValidator();
            string generated = cpfValidator.GenerateRandomValid();
            cpfValidator.AssertValid(generated);
        }
    }
}
