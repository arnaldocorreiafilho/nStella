using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using nStella.Core;

namespace nStella.Core.Tests
{
    [TestClass]
    public class DigitoParaTest
    {
        [TestMethod]
        public void GeracaoDeDigitoVerificadorParaBoleto()
        {
            IDictionary<DigitoPara, string> entradas = new Dictionary<DigitoPara, string>();
            entradas.Add(new DigitoPara("3999100100001200000351202000003910476618602"), "3");
            entradas.Add(new DigitoPara("2379316800000001002949060000000000300065800"), "6");
            entradas.Add(new DigitoPara("0019386000000040000000001207113000900020618"), "5");
            entradas.Add(new DigitoPara("0000039104766"), "3");

            foreach (DigitoPara digitoPara in entradas.Keys)
            {
                string esperado = entradas[digitoPara];
                Assert.AreEqual(esperado, digitoPara.ComMultiplicadoresDeAte(2, 9).ComplementarAoModulo().Mod(11).Calcula());
            }
        }
    }
}
