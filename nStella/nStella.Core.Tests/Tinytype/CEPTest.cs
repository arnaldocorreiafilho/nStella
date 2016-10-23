using Microsoft.VisualStudio.TestTools.UnitTesting;
using nStella.Core.Tinytype;

namespace nStella.Core.Tests.Tinytype
{
    [TestClass]  
    public class CEPTest
    {
        [TestMethod]
        public void ShouldAcceptValidFormattedCEP()
        {
            CEP cep = new CEP("12345-678");
            Assert.AreEqual("12345678", cep.GetNumero());
            Assert.AreEqual("12345-678", cep.GetNumeroFormatado());
        }

        [TestMethod]
        public void ShouldAcceptValidUnformattedCEP()
        {
            CEP cep = new CEP("12345678");
            Assert.AreEqual("12345678", cep.GetNumero());
            Assert.AreEqual("12345-678", cep.GetNumeroFormatado());
        }

        [TestMethod]
        public void ShouldAcceptInvalidCEP()
        {
            CEP cep = new CEP("12345-678");
            Assert.AreEqual("12345678", cep.GetNumero());
            
            cep = new CEP("12345-a78");
            Assert.AreEqual("12345-a78", cep.GetNumero());
            Assert.AreEqual("12345-a78", cep.GetNumeroFormatado());

            cep = new CEP("12345-678");
            Assert.AreEqual("12345678", cep.GetNumero());
            Assert.AreEqual("12345-678", cep.GetNumeroFormatado());

            cep = new CEP("1234-678");
            Assert.AreEqual("1234-678", cep.GetNumero());
            Assert.AreEqual("1234-678", cep.GetNumeroFormatado());
        }

        [TestMethod]
        public void ShouldHaveDomainDrivenEquals()
        {
            CEP a = new CEP("12345-678");
            CEP b = new CEP("12345-678");
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(a));
        }

    }
}
