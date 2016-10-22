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
    public class MultiLocaleNumericToWordsConverterTest
    {
        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void SupportMultipleCountriesWithSameLanguageWhenConvertingToWords()
        {
            CultureInfo[]
            cultures = new CultureInfo[] { new CultureInfo("en"), new CultureInfo("en-CA"), new CultureInfo("en-US"), new CultureInfo("en-GB") };

            foreach (CultureInfo culture in cultures)
            {
                NumericToWordsConverter converter = new NumericToWordsConverter(new InteiroSemFormato(), culture);
                string result = converter.ToWords(13L);
                Assert.AreEqual(result, "thirteen");
            }
        }



        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public void ThrowExceptionWhenConvertingToWordsWithUnsupportedLanguage()
        {
            NumericToWordsConverter converter = new NumericToWordsConverter(new InteiroSemFormato(), new CultureInfo("jp-JP"));
            converter.ToWords(13L);
        }

        [TestMethod]        
        public void UseBrazilianLocaleWhenConvertingToWordsWithoutLocale()
        {
            CultureInfo defaultLocale = CultureInfo.DefaultThreadCurrentCulture;
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

            try
            {
                NumericToWordsConverter converter = new NumericToWordsConverter(new InteiroSemFormato());

                string result = converter.ToWords(13L);
                Assert.AreEqual(result, "treze");
            }
            finally
            {
                CultureInfo.DefaultThreadCurrentCulture = defaultLocale;
            }
        }
    }
}
