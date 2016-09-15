using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Inwords
{
    public class FormatoDeReal : IFormatoDeExtenso
    {
        public int GetCasasDecimais()
        {
            return 2;
        }

        public string GetUnidadeDecimalNoPlural()
        {
            return "centavos";
        }

        public string GetUnidadeDecimalNoSingular()
        {
            return "centavo";
        }

        public string GetUnidadeInteiraNoPlural()
        {
            return "reais";
        }

        public string GetUnidadeInteiraNoSingular()
        {
            return "real";
        }
    }
}
