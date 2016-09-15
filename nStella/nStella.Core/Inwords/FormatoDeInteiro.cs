using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Inwords
{
    public class FormatoDeInteiro : IFormatoDeExtenso
    {
        public int GetCasasDecimais()
        {
            return 3;
        }

        public string GetUnidadeDecimalNoPlural()
        {
            return "milésimos";
        }

        public string GetUnidadeDecimalNoSingular()
        {
            return "milésimo";
        }

        public string GetUnidadeInteiraNoPlural()
        {
            return "inteiros";
        }

        public string GetUnidadeInteiraNoSingular()
        {
            return "inteiro";
        }
    }
}
