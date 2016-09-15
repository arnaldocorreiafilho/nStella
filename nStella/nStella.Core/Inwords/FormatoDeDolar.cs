using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Inwords
{
    public class FormatoDeDolar : IFormatoDeExtenso
    {
        public int GetCasasDecimais()
        {
            return 2;
        }
        public string GetUnidadeDecimalNoPlural()
        {
            return "cents";
        }
        public string GetUnidadeDecimalNoSingular()
        {
            return "cent";
        }
        public string GetUnidadeInteiraNoPlural()
        {
            return "dollars";
        }
        public string GetUnidadeInteiraNoSingular()
        {
            return "dollar";
        }
    }
}
