using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Validation.Error
{
    public class TituloEleitoralError : IInvalidValue
    {
        private readonly TituloEleitoralErrorEnum errorEnum;
        public TituloEleitoralError(TituloEleitoralErrorEnum errorEnum)
        {
            this.errorEnum = errorEnum;
        }
        public string Name()
        {
            return errorEnum.ToString();
        }
    }
    public enum TituloEleitoralErrorEnum
    {
        INVALID_CHECK_DIGITS, INVALID_FORMAT, INVALID_DIGITS, INVALID_CODIGO_DE_ESTADO
    }
}