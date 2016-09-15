using Microsoft.VisualStudio.TestTools.UnitTesting;
using nStella.Core.Validation;
using System.Collections.Generic;

namespace nStella.Core.Tests.Validation
{
    [TestClass]
    public class RenavamValidatorTest
    {

        private readonly string validUnformattedRenavam1 = "00639884962";
        private readonly string validUnformattedRenavam2 = "00811443620";
        private readonly string validFormattedRenavam1 = "0073.640767-7";
        private readonly string validFormattedRenavam2 = "0096.525195-0";
        private readonly string renavamUnformattedWithInvalidCheckDigit = "00639884969";
        private readonly string renavamFormattedWithInvalidCheckDigit = "0096.525195-2";
        private readonly string renavamWithLessThenElevenDigits = "9999999999";
        private readonly string renavamWithMoreThenElevenDigits = "999999999999";
        private readonly string renavamWithNineDigits = "639884962";

        [TestMethod]
        public void ShouldValidateValidUnformatedRenavam()
        {
            RenavamValidator validator = new RenavamValidator();
            validator.AssertValid(validUnformattedRenavam1);
            validator.AssertValid(validUnformattedRenavam2);

            IList<IValidationMessage> errorMessages = validator.InvalidMessageFor(validUnformattedRenavam1);
            Assert.IsTrue(errorMessages.Count == 0);
        }

        [TestMethod]
        public void ShouldValidateFormattedValidRenavam()
        {
            RenavamValidator validator = new RenavamValidator(true);
            validator.AssertValid(validFormattedRenavam1);
            validator.AssertValid(validFormattedRenavam2);

            IList<IValidationMessage> errorMessages = validator.InvalidMessageFor(validFormattedRenavam1);
            Assert.IsTrue(errorMessages.Count == 0);
        }

        [TestMethod]
        public void ShouldConsiderAValidFormattedRenavamAsEligible()
        {
            RenavamValidator validator = new RenavamValidator(true);
            Assert.IsTrue(validator.IsEligible(validFormattedRenavam1));
            Assert.IsTrue(validator.IsEligible(validFormattedRenavam2));
            Assert.IsTrue(validator.IsEligible(renavamFormattedWithInvalidCheckDigit));
        }

        [TestMethod]
        public void ShouldConsiderAValidUnformattedRenavamAsEligible()
        {
            RenavamValidator validator = new RenavamValidator();
            Assert.IsTrue(validator.IsEligible(validUnformattedRenavam1), "Renamvam " + validUnformattedRenavam1 + " must be eligible.");
            Assert.IsTrue(validator.IsEligible(validUnformattedRenavam2));
            Assert.IsTrue(validator.IsEligible(renavamUnformattedWithInvalidCheckDigit));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidStateException))]
        public void ShouldNotValidadeUnformattedRenavamWithInvalidCheckDigit()
        {
            RenavamValidator validator = new RenavamValidator();
            validator.AssertValid(renavamUnformattedWithInvalidCheckDigit);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidStateException))]
        public void ShouldNotValidadeFormattedRenavamWithInvalidCheckDigit()
        {
            RenavamValidator validator = new RenavamValidator(true);
            validator.AssertValid(renavamFormattedWithInvalidCheckDigit);
        }

        [TestMethod]
        public void OnlyRenavamWithNineOrElevenDigitsAreEligible()
        {
            RenavamValidator validator = new RenavamValidator();
            Assert.IsTrue(validator.IsEligible(renavamWithNineDigits));
            Assert.IsTrue(validator.IsEligible(renavamWithLessThenElevenDigits));
            Assert.IsFalse(validator.IsEligible(renavamWithMoreThenElevenDigits));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidStateException))]
        public void ShouldNotValidateARenavamWithLessThenElevenDigits()
        {
            RenavamValidator validator = new RenavamValidator();
            validator.AssertValid(renavamWithLessThenElevenDigits);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidStateException))]
        public void ShouldNotValidateARenavamWithMoreThenElevenDigits()
        {
            RenavamValidator validator = new RenavamValidator();
            validator.AssertValid(renavamWithMoreThenElevenDigits);
        }

        [TestMethod]
        public void RenavamMustHaveOnlyNumbers()
        {
            RenavamValidator validator = new RenavamValidator();
            Assert.IsFalse(validator.IsEligible("99999999x"));
            Assert.IsFalse(validator.IsEligible("9999999 9"));
        }

        [TestMethod]
        public void ShouldGenerateExplanatoryErrorMessagesForUnformattedRenavam()
        {
            RenavamValidator validator = new RenavamValidator();
            IList<IValidationMessage> invalidMessagesFor = null;

            invalidMessagesFor = validator.InvalidMessageFor("999");
            Assert.IsTrue(invalidMessagesFor.Count == 1);
            Assert.AreEqual("RenavamError : INVALID DIGITS", invalidMessagesFor[0].GetMessage());

            invalidMessagesFor = validator.InvalidMessageFor(renavamUnformattedWithInvalidCheckDigit);
            Assert.IsTrue(invalidMessagesFor.Count == 1);
            Assert.AreEqual("RenavamError : INVALID CHECK DIGIT", invalidMessagesFor[0].GetMessage());
        }

        [TestMethod]
        public void ShouldGenerateExplanatoryErrorMessagesForFormattedRenavam()
        {
            RenavamValidator validator = new RenavamValidator(true);
            IList<IValidationMessage> invalidMessagesFor = null;

            invalidMessagesFor = validator.InvalidMessageFor("999");
            Assert.IsTrue(invalidMessagesFor.Count == 2);
            Assert.AreEqual("RenavamError : INVALID FORMAT", invalidMessagesFor[0].GetMessage());
            Assert.AreEqual("RenavamError : INVALID DIGITS", invalidMessagesFor[1].GetMessage());

            invalidMessagesFor = validator.InvalidMessageFor(renavamFormattedWithInvalidCheckDigit);
            Assert.IsTrue(invalidMessagesFor.Count == 1);
            Assert.AreEqual("RenavamError : INVALID CHECK DIGIT", invalidMessagesFor[0].GetMessage());
        }

        [TestMethod]
        public void ShouldUseAnSimpleMessageProducerAsDefault()
        {
            RenavamValidator validator = new RenavamValidator();
            try
            {
                validator.AssertValid(renavamFormattedWithInvalidCheckDigit);
                Assert.Fail();
            }
            catch (InvalidStateException e)
            {
                IList<IValidationMessage> errors = e.GetInvalidMessages();
                Assert.IsTrue(errors.Count == 1);
                Assert.AreEqual("RenavamError : INVALID CHECK DIGIT", errors[0].GetMessage());
            }
        }

        [TestMethod]
        public void ShouldValidateValidRenavamWithNineDigits()
        {
            RenavamValidator validator = new RenavamValidator();
            validator.AssertValid(renavamWithNineDigits);

            IList<IValidationMessage> errorMessages = validator.InvalidMessageFor(renavamWithNineDigits);
            Assert.IsTrue(errorMessages.Count == 0);
        }

        [TestMethod]
        public void ShouldGenerateValidFormattedRenavam()
        {
            RenavamValidator renavamValidator = new RenavamValidator(true);
            string generated = renavamValidator.GenerateRandomValid();
            renavamValidator.AssertValid(generated);
        }

        [TestMethod]
        public void ShouldGenerateValidUnformattedRenavam()
        {
            RenavamValidator renavamValidator = new RenavamValidator();
            string generated = renavamValidator.GenerateRandomValid();
            renavamValidator.AssertValid(generated);
        }
    }
}