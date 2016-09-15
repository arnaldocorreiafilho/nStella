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
    public class CNPJValidatorTest
    {
        private static readonly string INVALID_FORMAT = "INVALID FORMAT";

        private static readonly string INVALID_CHECK_DIGITS = "INVALID CHECK DIGITS";

        private static readonly string INVALID_DIGITS = "INVALID DIGITS";

        private readonly string validString = "26.637.142/0001-58";
        private readonly string validStringNotFormatted = "26637142000158";

        private readonly string firstCheckDigitWrongNotFormatted = "26637142000168";

        [TestMethod]
        public void ShouldHaveDefaultConstructorThatUsesSimpleMessageProducerAndAssumesThatStringIsNotFormatted()
        {

            new CNPJValidator().AssertValid(validStringNotFormatted);
            try
            {
                new CNPJValidator().AssertValid(firstCheckDigitWrongNotFormatted);
                Assert.Fail("Test expected to throw exception");
            }
            catch (InvalidStateException e)
            {
                InvalidStateException invalidStateException = e;
                AssertMessage(invalidStateException, INVALID_CHECK_DIGITS);
            }
        }
        
        private void AssertMessage(InvalidStateException invalidStateException, string expected)
        {
            Assert.IsTrue(invalidStateException.GetInvalidMessages()[0].GetMessage().Contains(expected)); 
        }

        [TestMethod]
        public void ShouldNotValidateCNPJWithLessDigitsThanAllowed()
        {
            CNPJValidator validator = new CNPJValidator();
            try
            {
                string value = "1234567890123";
                validator.AssertValid(value);
                Assert.Fail();
            }
            catch (InvalidStateException e)
            {
                Assert.IsTrue(e.GetInvalidMessages().Count == 1);
                AssertMessage(e, INVALID_DIGITS);
            }
        }

        [TestMethod]
        public void ShouldNotValidateCNPJWithMoreDigitsThanAllowed()
        {
            CNPJValidator validator = new CNPJValidator();
            try
            {
                string value = "123456789012345";
                validator.AssertValid(value);
                Assert.Fail();
            }
            catch (InvalidStateException e)
            {
                Assert.IsTrue(e.GetInvalidMessages().Count == 1);
                AssertMessage(e, INVALID_DIGITS);
            }
        }

        [TestMethod]
        public void ShouldNotValidateCNPJWithInvalidCharacter()
        {
            CNPJValidator validator = new CNPJValidator(false);
            try
            {
                string value = "1111111a111111";
                validator.AssertValid(value);
                Assert.Fail();
            }
            catch (InvalidStateException e)
            {
                Assert.IsTrue(e.GetInvalidMessages().Count == 1);
                AssertMessage(e, INVALID_DIGITS);
            }
        }

        [TestMethod]
        public void ShouldValidateValidCNPJ()
        {
            CNPJValidator validator = new CNPJValidator();

            validator.AssertValid("11222333000181");
            validator.AssertValid("63025530002409");
            validator.AssertValid("61519128000150");
            validator.AssertValid("68745386000102");
        }

        [TestMethod]
        public void ShoulValidateNullCNPJ()
        {
            CNPJValidator validator = new CNPJValidator();
            string value = null;
            validator.AssertValid(value);
        }

        [TestMethod]
        public void ShouldNotValidateCNPJCheckDigitsWithFirstCheckDigitWrong()
        {
            CNPJValidator validator = new CNPJValidator();
            // VALID CNPJ = 742213250001-30
            try
            {
                string value = "74221325000160";
                validator.AssertValid(value);
                Assert.Fail();
            }
            catch (InvalidStateException e)
            {
                Assert.IsTrue(e.GetInvalidMessages().Count == 1);
                AssertMessage(e, INVALID_CHECK_DIGITS);
            }
        }

        [TestMethod]
        public void ShouldNotValidateCNPJCheckDigitsWithSecondCheckDigitWrong()
        {
            CNPJValidator validator = new CNPJValidator();

            // VALID CNPJ = 266371420001-58
            try
            {
                string value = "26637142000154";
                validator.AssertValid(value);
                Assert.Fail();
            }
            catch (InvalidStateException e)
            {
                Assert.IsTrue(e.GetInvalidMessages().Count == 1);
                AssertMessage(e, INVALID_CHECK_DIGITS);
            }
        }

        [TestMethod]
        public void ShouldValidateValidFormattedCNPJ()
        {
            CNPJValidator validator = new CNPJValidator(true);
            string value = validString;
            validator.AssertValid(value);
        }

        [TestMethod]
        public void ShouldNotValidateValidUnformattedCNPJWhenExplicity()
        {
            CNPJValidator validator = new CNPJValidator(true);

            // VALID CNPJ = 26.637.142/0001-58
            try
            {
                string value = "26637142000158";
                validator.AssertValid(value);
                Assert.Fail();
            }
            catch (InvalidStateException e)
            {
                Assert.IsTrue(e.GetInvalidMessages().Count == 1);
                AssertMessage(e, INVALID_FORMAT);
            }
        }

        [TestMethod]
        public void ShouldNotBeEligibleWithNullCNPJ()
        {
            CNPJValidator cnpjValidator = new CNPJValidator();
            Assert.IsFalse(cnpjValidator.IsEligible(null));
        }

        [TestMethod]
        public void ShouldBeEligibleDefaultConstructor()
        {
            CNPJValidator cnpjValidator = new CNPJValidator();
            Assert.IsTrue(cnpjValidator.IsEligible(validStringNotFormatted));
            Assert.IsFalse(cnpjValidator.IsEligible(validString));
        }

        [TestMethod]
        public void ShouldBeEligibleConstructorNotFormatted()
        {
            CNPJValidator cnpjValidator = new CNPJValidator(false);
            Assert.IsTrue(cnpjValidator.IsEligible(validStringNotFormatted));
            Assert.IsFalse(cnpjValidator.IsEligible(validString));
        }

        [TestMethod]
        public void ShouldBeEligibleConstructorFormatted()
        {
            CNPJValidator cnpjValidator = new CNPJValidator(true);
            Assert.IsFalse(cnpjValidator.IsEligible(validStringNotFormatted));
            Assert.IsTrue(cnpjValidator.IsEligible(validString));
        }

        [TestMethod]
        public void ShouldGenerateValidFormattedCNPJ()
        {
            CNPJValidator cnpjValidator = new CNPJValidator(true);
            string generated = cnpjValidator.GenerateRandomValid();
            cnpjValidator.AssertValid(generated);
        }

        [TestMethod]
        public void ShouldGenerateValidUnformattedCPF()
        {
            CNPJValidator cnpjValidator = new CNPJValidator();
            string generated = cnpjValidator.GenerateRandomValid();
            cnpjValidator.AssertValid(generated);
        }
    }
}