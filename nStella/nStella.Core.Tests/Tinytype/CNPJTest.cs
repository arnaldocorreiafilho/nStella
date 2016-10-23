using Microsoft.VisualStudio.TestTools.UnitTesting;
using nStella.Core.Tinytype;

namespace nStella.Core.Tests.Tinytype
{
    [TestClass]
    public class CNPJTest
    {

        [TestMethod]
        public void ShouldAcceptValidFormattedCNPJ()
        {
            CNPJ cnpj = new CNPJ("23.121.367/0001-69");
            Assert.AreEqual("23121367000169", cnpj.GetNumero());
            Assert.AreEqual("23.121.367/0001-69", cnpj.GetNumeroFormatado());
        }

        [TestMethod]
        public void ShouldAcceptValidUnformattedCNPJ()
        {
            CNPJ cnpj = new CNPJ("23121367000169");
            Assert.AreEqual("23121367000169", cnpj.GetNumero());
            Assert.AreEqual("23.121.367/0001-69", cnpj.GetNumeroFormatado());
        }

        [TestMethod]        
        public void ShouldAcceptInvalidCNPJ()
        {
            CNPJ cnpj = new CNPJ("12.345.678/9012-34");

            Assert.AreEqual("12345678901234", cnpj.GetNumero());
            Assert.AreEqual("12.345.678/9012-34", cnpj.GetNumeroFormatado());

            cnpj = new CNPJ("12.3w5.678/9012-34");
            Assert.AreEqual("12.3w5.678/9012-34", cnpj.GetNumero());
            Assert.AreEqual("12.3w5.678/9012-34", cnpj.GetNumeroFormatado());

            cnpj = new CNPJ("12.3453.678/9012-34");

            Assert.AreEqual("12.3453.678/9012-34", cnpj.GetNumero());
            Assert.AreEqual("12.3453.678/9012-34", cnpj.GetNumeroFormatado());

            cnpj = new CNPJ("12.345.8/9012-34");

            Assert.AreEqual("12.345.8/9012-34", cnpj.GetNumero());
            Assert.AreEqual("12.345.8/9012-34", cnpj.GetNumeroFormatado());
        }

        [TestMethod]        
        public void ShouldReturnIfIsValid()
        {
            CNPJ cnpjValido = new CNPJ("23.121.367/0001-69");
            CNPJ cnpjInvalido = new CNPJ("12.345.678/9012-34");

            Assert.IsTrue(cnpjValido.IsValid());
            Assert.IsFalse(cnpjInvalido.IsValid());
        }

        [TestMethod]
        public void ShouldHaveDomainDrivenEquals()
        {
            CNPJ a = new CNPJ("23.121.367/0001-69");
            CNPJ b = new CNPJ("23.121.367/0001-69");
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(a));
        }

    }
}