using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Inwords
{
    public class FormatoDeMoeda : IFormatoDeExtenso
    {
        private readonly IFormatoDeExtenso formato;

        public FormatoDeMoeda(CultureInfo cultureInfo)
        {
            if ("en".Equals(cultureInfo.Name))
                formato = new FormatoDeDolar();
            else if ("pt-BR".Equals(cultureInfo.Name))
                formato = new FormatoDeReal();
            else
            {
                var rInfor = new RegionInfo(cultureInfo.Name);
                string pais = rInfor.DisplayName;

                throw new ArgumentException("Não foi possível determinar a moeda para o país " + pais);
            }
        }
        public int GetCasasDecimais()
        {
            return formato.GetCasasDecimais();
        }

        public string GetUnidadeDecimalNoPlural()
        {
            return formato.GetUnidadeDecimalNoPlural();
        }

        public string GetUnidadeDecimalNoSingular()
        {
            return formato.GetUnidadeDecimalNoSingular();
        }

        public string GetUnidadeInteiraNoPlural()
        {
            return formato.GetUnidadeInteiraNoPlural();
        }

        public string GetUnidadeInteiraNoSingular()
        {
            return formato.GetUnidadeInteiraNoSingular();
        }
    }
}