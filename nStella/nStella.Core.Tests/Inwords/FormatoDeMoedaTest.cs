using Microsoft.VisualStudio.TestTools.UnitTesting;
using nStella.Core.Inwords;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Tests.Inwords
{
    [TestClass]
    public class FormatoDeMoedaTest
    {

        [TestMethod]       
        public void UseDolarWhenCreatingFormatoDeMoedaForLocaleUS()
        {
            FormatoDeMoeda formato = new FormatoDeMoeda(new CultureInfo("en"));

            Assert.AreEqual(formato.GetUnidadeDecimalNoSingular(), "cent");
            Assert.AreEqual(formato.GetUnidadeDecimalNoPlural(), "cents");
            Assert.AreEqual(formato.GetUnidadeInteiraNoSingular(), "dollar");
            Assert.AreEqual(formato.GetUnidadeInteiraNoPlural(), "dollars");
            Assert.AreEqual(formato.GetCasasDecimais(), 2);
        }

        [TestMethod]        
        public void UseRealWhenCreatingFormatoDeMoedaForLocalePT_BR()
        {
            FormatoDeMoeda formato = new FormatoDeMoeda(Messages.LOCALE_PT_BR);

            Assert.AreEqual(formato.GetUnidadeDecimalNoSingular(), "centavo");
            Assert.AreEqual(formato.GetUnidadeDecimalNoPlural(), "centavos");
            Assert.AreEqual(formato.GetUnidadeInteiraNoSingular(), "real");
            Assert.AreEqual(formato.GetUnidadeInteiraNoPlural(), "reais");
            Assert.AreEqual(formato.GetCasasDecimais(), 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Não foi possível determinar a moeda para o país Japão")]
        public void ThrowExceptionWhenCreatingFormatoDeMoedaForUnknownLocale()
        {
            new FormatoDeMoeda(new CultureInfo("jp-JP"));
        }
    }
}