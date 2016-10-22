using nStella.Core.Format;
using nStella.Core.Validation.Error;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace nStella.Core.Validation
{
    public class CNPJValidator : IValidator<string>
    {
        public static readonly Regex FORMATED = new Regex("(\\d{2})[.](\\d{3})[.](\\d{3})/(\\d{4})-(\\d{2})");
        public static readonly Regex UNFORMATED = new Regex("(\\d{2})(\\d{3})(\\d{3})(\\d{4})(\\d{2})", RegexOptions.Compiled);

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
                    unformatedCNPJ = new CNPJFormatter().UnFormat(cnpj);
                }
                catch (ArgumentException)
                {
                    errors.Add(messageProducer.GetMessage(new CNPJError(CNPJErrorEnum.INVALID_DIGITS)));
                    return errors;
                }

                var regxUnFormatedCNPJ = new Regex("[0-9]*");

                if (unformatedCNPJ.Length != 14 || !regxUnFormatedCNPJ.IsMatch(unformatedCNPJ))
                {
                    errors.Add(messageProducer.GetMessage(new CNPJError(CNPJErrorEnum.INVALID_DIGITS)));
                }

                string cnpjSemDigito = unformatedCNPJ.Substring(0, unformatedCNPJ.Length - 2);
                string digitos = unformatedCNPJ.Substring(unformatedCNPJ.Length - 2);

                string digitosCalculados = CalculaDigitos(cnpjSemDigito);

                if (!digitos.Equals(digitosCalculados))
                {
                    errors.Add(messageProducer.GetMessage(new CNPJError(CNPJErrorEnum.INVALID_CHECK_DIGITS)));
                }

            }
            return errors;
        }

        private string CalculaDigitos(string cnpjSemDigito)
        {
            DigitoPara digitoPara = new DigitoPara(cnpjSemDigito);
            digitoPara.ComplementarAoModulo().TrocandoPorSeEncontrar("0", 10, 11).Mod(11);
            string digito1 = digitoPara.Calcula();
            digitoPara.AddDigito(digito1);
            string digito2 = digitoPara.Calcula();

            return digito1 + digito2;

        }

        public void AssertValid(string cnpj)
        {
            IList<IValidationMessage> errors = GetInvalidValues(cnpj);
            if (!(errors.Count == 0))
                throw new InvalidStateException(errors);
        }     
        public string GenerateRandomValid()
        {
            string cnpjSemDigito = new DigitoGenerator().Generate(12);
            string cnpjComDigito = cnpjSemDigito + CalculaDigitos(cnpjSemDigito);

            if (isFormatted)
            {
                return new CNPJFormatter().Format(cnpjComDigito);
            }
            return cnpjComDigito;
        }

        public IList<IValidationMessage> InvalidMessageFor(string cnpj)
        {
            return GetInvalidValues(cnpj);
        }

        public bool IsEligible(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            bool result;
            if (isFormatted)
                result = FORMATED.IsMatch(value);
            else
                result = UNFORMATED.IsMatch(value);

            return result;
        }
    }
}
