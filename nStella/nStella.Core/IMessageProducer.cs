using nStella.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core
{
    public interface IMessageProducer
    {
        /// <param name="invalidValue">Valor inválido ao qual se procura a mensagem associada.</param>
        /// <returns>Retorna uma mensagem de validação associada ao erro.</returns>
        IValidationMessage GetMessage(IInvalidValue invalidValue);
    }
}
