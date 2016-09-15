using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nStella.Core.Validation;

namespace nStella.Core.Tests.Validation
{
    [TestClass]
    public class NITValidatorTest
    {
        private static readonly string INVALID_FORMAT = "INVALID FORMAT";
        private static readonly string INVALID_DIGITS = "INVALID DIGITS";
        private static readonly string INVALID_CHECK_DIGITS = "INVALID CHECK DIGITS";

        [TestMethod]
        public void ShouldNotValidateNITWithInvalidCharacter()
        {
            NITValidator validator = new NITValidator();
            string value = "1111111a111";
            try
            {
                validator.AssertValid(value);
                Assert.Fail();
            }
            catch (InvalidStateException e)
            {
                Assert.IsTrue(e.GetInvalidMessages().Count == 1);                
                AssertMessage(e, INVALID_DIGITS);
            }
        }

        private void AssertMessage(InvalidStateException invalidStateException, string expected)
        {
            Assert.IsTrue(invalidStateException.GetInvalidMessages()[0].GetMessage().Contains(expected));
        }

        [TestMethod]
        public void ShouldNotValidateNITWithLessDigitsThanAllowed()
        {
            NITValidator validator = new NITValidator();
            string value = "1234567890";
            try
            {
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
        public void ShouldNotValidateNITWithMoreDigitsThanAlowed()
        {
            NITValidator validator = new NITValidator();
            string value = "123456789012";
            try
            {
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
        public void ShouldNotValidateNITCheckDigitsWithCheckDigitWrong()
        {
            NITValidator validator = new NITValidator();
            // VALID NIT = 24.84380.348-0
            string value = "24843803470";
            try
            {
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
        public void ShouldValidateValidNIT()
        {
            NITValidator validator = new NITValidator();

            validator.AssertValid("12345678919");
            validator.AssertValid("34608514300");
            validator.AssertValid("47393545608");
        }

        [TestMethod]
        public void shouldValidateNullNIT()
        {
            NITValidator validator = new NITValidator();
            string value = null;
            validator.AssertValid(value);
        }

        [TestMethod]        
        public void ShouldValidateValidFormattedNIT()
        {
            NITValidator validator = new NITValidator(true);
            // VALID NIT = 123.45678.91-9
            string value = "123.45678.91-9";
            validator.AssertValid(value);
        }

        [TestMethod]
        public void ShouldNotValidateValidUnformattedNIT()
        {
            NITValidator validator = new NITValidator(true);
            // VALID NIT = 12.34567.891-9
            string value = "12345678919";
            try
            {
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
        public void ShouldGenerateRandomValidUnformattedNIT()
        {
            NITValidator validator = new NITValidator(false);
            string value = validator.GenerateRandomValid();
            validator.AssertValid(value);
        }

        [TestMethod]
        public void ShouldGenerateRandomValidFormattedNIT()
        {
            NITValidator validator = new NITValidator(true);
            string value = validator.GenerateRandomValid();
            validator.AssertValid(value);
        }
    }
}