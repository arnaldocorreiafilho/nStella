using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Inwords
{
    public class InteiroSemFormato : IFormatoDeExtenso
    {
        public int GetCasasDecimais()
        {
            return 1;
        }

        public string GetUnidadeDecimalNoPlural()
        {
            return "";
        }

        public string GetUnidadeDecimalNoSingular()
        {
            return "";
        }

        public string GetUnidadeInteiraNoPlural()
        {
            return "";
        }

        public string GetUnidadeInteiraNoSingular()
        {
            return "";
        }
    }
}
