using nStella.Core.Format;
using nStella.Core.Validation.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace nStella.Core.Validation
{
    public class NITValidator : IValidator<string>
    {
        public static readonly Regex FORMATED = new Regex("(\\d{3})[.](\\d{5})[.](\\d{2})-(\\d{1})");
        public static readonly Regex UNFORMATED = new Regex("(\\d{3})(\\d{5})(\\d{2})(\\d{1})");

        private bool isFormatted = false;
        private IMessageProducer messageProducer;

        public NITValidator()
        {
            messageProducer = new SimpleMessageProducer();
        }

        public NITValidator(bool isFormatted)
        {
            messageProducer = new SimpleMessageProducer();
            this.isFormatted = isFormatted;
        }
        public NITValidator(IMessageProducer messageProducer, bool isFormatted)
        {
            this.messageProducer = messageProducer;
            this.isFormatted = isFormatted;
        }
        private IList<IValidationMessage> GetInvalidValues(string nit)
        {
            IList<IValidationMessage> errors = new List<IValidationMessage>();
            if (!string.IsNullOrEmpty(nit))
            {
                if (isFormatted && FORMATED.IsMatch(nit))
                    errors.Add(messageProducer.GetMessage(new NITError(NITErrorEnum.INVALID_FORMAT)));

                string unformatedNIT = string.Empty;
                try
                {
                    unformatedNIT = new NITFormatter().UnFormat(nit);
                }
                catch (ArgumentException)
                {
                    errors.Add(messageProducer.GetMessage(new NITError(NITErrorEnum.INVALID_DIGITS)));
                    return errors;
                }
                var regexUnfNIT = new Regex("[0-9]*");
                if (unformatedNIT.Length != 11 || !regexUnfNIT.IsMatch(unformatedNIT))
                    errors.Add(messageProducer.GetMessage(new NITError(NITErrorEnum.INVALID_DIGITS)));

                string nitSemDigito = unformatedNIT.Substring(0, unformatedNIT.Length - 1);
                string digito = unformatedNIT.Substring(unformatedNIT.Length - 1);
                string digitoCalculados = calculaDigitos(nitSemDigito);

                if (!digito.Equals(digitoCalculados))
                    errors.Add(messageProducer.GetMessage(new NITError(NITErrorEnum.INVALID_CHECK_DIGITS)));
            }

            return errors;
        }

        private string calculaDigitos(string nitSemDigito)
        {
            DigitoPara digitoPara = new DigitoPara(nitSemDigito);
            return digitoPara.ComplementarAoModulo().TrocandoPorSeEncontrar("0", 10, 11).Mod(11).Calcula();
        }

        public void AssertValid(string nit)
        {
            IList<IValidationMessage> errors = GetInvalidValues(nit);
            if (errors.Count != 0)            
                throw new InvalidStateException(errors);
            
        }
        public string GenerateRandomValid()
        {
            string nitSemDigito = new DigitoGenerator().Generate(10);
            string nitComDigito = nitSemDigito + calculaDigitos(nitSemDigito);
            if (isFormatted)
            {
                return new NITFormatter().Format(nitComDigito);
            }
            return nitComDigito;
        }
        public IList<IValidationMessage> InvalidMessageFor(string nit)
        {
            return GetInvalidValues(nit);
        }
        public bool IsEligible(string value)
        {
            bool result;
            if (isFormatted)
            {
                result = FORMATED.IsMatch(value);
            }
            else
            {
                result = UNFORMATED.IsMatch(value);
            }
            return result;
        }
    }
}
