using Microsoft.VisualStudio.TestTools.UnitTesting;
using nStella.Core.Tinytype;

namespace nStella.Core.Tests.Tinytype
{
    [TestClass]
    public class CPFTest
    {

        [TestMethod]
        public void ShouldAcceptValidFormattedCPF()
        {
            CPF cpf = new CPF("111.111.111-11");
            Assert.AreEqual("11111111111", cpf.GetNumero());
            Assert.AreEqual("111.111.111-11", cpf.GetNumeroFormatado());
        }

        [TestMethod]
        public void ShouldAcceptValidUnformattedCPF()
        {
            CPF cpf = new CPF("11144477735");
            Assert.AreEqual("11144477735", cpf.GetNumero());
            Assert.AreEqual("111.444.777-35", cpf.GetNumeroFormatado());
        }

        [TestMethod]
        public void ShouldAcceptInvalidCPF()
        {
            CPF cpf = new CPF("843.843.131-84");
            Assert.AreEqual("84384313184", cpf.GetNumero());

            cpf = new CPF("111.111.1a1-11");
            Assert.AreEqual("111.111.1a1-11", cpf.GetNumero());
            Assert.AreEqual("111.111.1a1-11", cpf.GetNumeroFormatado());

            cpf = new CPF("111.1111.111-11");
            Assert.AreEqual("111.1111.111-11", cpf.GetNumero());
            Assert.AreEqual("111.1111.111-11", cpf.GetNumeroFormatado());

            cpf = new CPF("111.1.111-11");
            Assert.AreEqual("111.1.111-11", cpf.GetNumero());
            Assert.AreEqual("111.1.111-11", cpf.GetNumeroFormatado());
        }

        [TestMethod]
        public void ShouldReturnIfIsValid()
        {
            CPF cpfValido = new CPF("111.444.777-35");
            CPF cpfInvalido = new CPF("843.843.131-85");


            Assert.IsTrue(cpfValido.IsValido());

            Assert.IsFalse(cpfInvalido.IsValido());
        }

        [TestMethod]
        public void ShouldHaveDomainDrivenEquals()
        {
            CPF a = new CPF("333.333.333-33");
            CPF b = new CPF("333.333.333-33");
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(a));
        }
    }
}
