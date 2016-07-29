using nStella.Core.Validation.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace nStella.Core.Validation
{
    public class CNPJValidator : IValidator<string>
    {
        public static readonly Regex FORMATED = new Regex("(\\d{2})[.](\\d{3})[.](\\d{3})/(\\d{4})-(\\d{2})");
        public static readonly Regex UNFORMATED = new Regex("(\\d{2})(\\d{3})(\\d{3})(\\d{4})(\\d{2})");

        private bool isFormatted = false;
        private IMessageProducer messageProducer;

        public CNPJValidator()
        {
            messageProducer = new SimpleMessageProducer();
        }

        public CNPJValidator(bool isFormatted)
        {
            this.isFormatted = isFormatted;
            messageProducer = new SimpleMessageProducer();
        }

        public CNPJValidator(IMessageProducer messageProducer, bool isFormatted)
        {
            this.messageProducer = messageProducer;
            this.isFormatted = isFormatted;
        }

        public CNPJValidator(IMessageProducer messageProducer)
        {
            this.messageProducer = messageProducer;
        }

        private IList<IValidationMessage> GetInvalidValues(string cnpj)
        {

            IList<IValidationMessage> errors = new List<IValidationMessage>();

            if (cnpj != null)
            {
                if (isFormatted != FORMATED.Match(cnpj).Success)
                {
                    errors.Add(messageProducer.GetMessage(new CNPJError(CNPJErrorEnum.INVALID_FORMAT)));
                }

                string unformatedCNPJ = null;
                try
                {
                    unformatedCNPJ = new CNPJFormatter().unformat(cnpj);
                }
                catch (ArgumentException e)
                {
                    errors.Add(messageProducer.GetMessage(new CNPJError(CNPJErrorEnum.INVALID_DIGITS)));
                    return errors;
                }

                if (unformatedCNPJ.length() != 14 || !unformatedCNPJ.matches("[0-9]*"))
                {
                    errors.add(messageProducer.getMessage(CNPJError.INVALID_DIGITS));
                }

                String cnpjSemDigito = unformatedCNPJ.substring(0, unformatedCNPJ.length() - 2);
                String digitos = unformatedCNPJ.substring(unformatedCNPJ.length() - 2);

                String digitosCalculados = calculaDigitos(cnpjSemDigito);

                if (!digitos.equals(digitosCalculados))
                {
                    errors.add(messageProducer.getMessage(CNPJError.INVALID_CHECK_DIGITS));
                }

            }
            return errors;
        }


        public void AssertValid(string Object)
        {
            throw new NotImplementedException();
        }

        public string GenerateRandomValid()
        {
            throw new NotImplementedException();
        }

        public List<IValidationMessage> InvalidMessageFor(string Object)
        {
            throw new NotImplementedException();
        }

        public bool IsEligible(string Object)
        {
            throw new NotImplementedException();
        }
    }
}
