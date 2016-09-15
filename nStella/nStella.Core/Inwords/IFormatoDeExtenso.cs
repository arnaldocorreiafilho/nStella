using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Inwords
{
    public interface IFormatoDeExtenso
    {
        /// <returns>A unidade no singular da parte inteira do número.</returns>
        string GetUnidadeInteiraNoSingular();
        /// <returns>A unidade no singular da parte inteira do número.</returns>
        string GetUnidadeInteiraNoPlural();        
        /// <returns>A unidade no singular da parte decimal do número.</returns>
        string GetUnidadeDecimalNoSingular();
        /// <returns>A unidade no plural da parte decimal do número.</returns>
        string GetUnidadeDecimalNoPlural();        
        /// <returns>A quantidade de casas decimais a serem consideradas 
        /// na transfomação em extenso. </returns>
        int GetCasasDecimais();
    }
}
