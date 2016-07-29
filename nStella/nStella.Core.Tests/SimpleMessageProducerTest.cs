using Microsoft.VisualStudio.TestTools.UnitTesting;
using nStella.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Tests
{
    class DocumentoError : IInvalidValue
    {
        private DocumentoErrorEnum erroEnum;
        public DocumentoError(DocumentoErrorEnum documentEnumError)
        {
            erroEnum = documentEnumError;
        }
        public enum DocumentoErrorEnum
        {
            INVALID_DIGITS, INVALID_CHECK_DIGITS

        }
        public string Name()
        {
            return erroEnum.ToString();
        }
    }
    [TestClass]
    public class SimpleMessageProducerTest
    {
        [TestMethod]
        public void TestGetMessage()
        {
            SimpleMessageProducer messageProducer = new SimpleMessageProducer();
            IValidationMessage message = messageProducer.GetMessage(new DocumentoError(DocumentoError.DocumentoErrorEnum.INVALID_CHECK_DIGITS));
            Assert.AreEqual("DocumentoError : INVALID CHECK DIGITS", message.GetMessage());
        }
    }
}
