using nStella.Core.Format;
using nStella.Core.Validation.Error;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace nStella.Core.Validation
{
    public class RenavamValidator : IValidator<string>
    {
        public static readonly Regex FORMATED = new Regex("(\\d{4})[.](\\d{6})-(\\d{1})");
        public static readonly Regex UNFORMATED = new Regex("(\\d{4})(\\d{6})(\\d{1})");

        private bool isFormatted = false;
        private IMessageProducer messageProducer;

        public RenavamValidator()
        {
            messageProducer = new SimpleMessageProducer();
        }

        public RenavamValidator(bool isFormatted)
        {
            this.isFormatted = isFormatted;
            messageProducer = new SimpleMessageProducer();
        }

        public RenavamValidator(IMessageProducer messageProducer, bool isFormatted)
        {
            this.messageProducer = messageProducer;
            this.isFormatted = isFormatted;
        }        
                
        public void AssertValid(string renavam)
        {
            IList<IValidationMessage> errors = GetInvalidValues(renavam);
            if (!(errors.Count == 0))
                throw new InvalidStateException(errors);
        }

        public string GenerateRandomValid()
        {
            string renavamSemDigito = new DigitoGenerator().Generate(10);
            string renavamComDigito = renavamSemDigito + CalculaDigito(renavamSemDigito);

            if (isFormatted)
                return new RenavamFormatter().Format(renavamComDigito);

            return renavamComDigito;
        }

        public IList<IValidationMessage> InvalidMessageFor(string renavam)
        {
            return GetInvalidValues(renavam);
        }

        public bool IsEligible(string renavam)
        {
            bool isEligible;

            if (isFormatted)
                isEligible = RenavamFormatter.FORMATED.IsMatch(renavam);
            else
                isEligible = RenavamFormatter.UNFORMATED.IsMatch(renavam);

            return isEligible;
        }

        private IList<IValidationMessage> GetInvalidValues(string renavam)
        {
            IList<IValidationMessage> errors = new List<IValidationMessage>();

            if (!string.IsNullOrEmpty(renavam))
            {
                renavam = FormataPadraoNovo(renavam);

                if (isFormatted && !FORMATED.IsMatch(renavam))
                    errors.Add(messageProducer.GetMessage(new RenavamError(RenavamErrorEnum.INVALID_FORMAT)));

                string unformatedRenavam = string.Empty;

                try
                {
                    unformatedRenavam = new RenavamFormatter().UnFormat(renavam);
                }
                catch (ArgumentException)
                {
                    errors.Add(messageProducer.GetMessage(new RenavamError(RenavamErrorEnum.INVALID_DIGITS)));
                    return errors;
                }

                Regex unfRenavam = new Regex("[0-9]*");
                if (unformatedRenavam.Length != 11 || !unfRenavam.IsMatch(unformatedRenavam))
                    errors.Add(messageProducer.GetMessage(new RenavamError(RenavamErrorEnum.INVALID_DIGITS)));

                string renavamSemDigito = unformatedRenavam.Substring(0, unformatedRenavam.Length - 1);
                string digito = unformatedRenavam.Substring(unformatedRenavam.Length - 1);

                string digitoCalculado = CalculaDigito(renavamSemDigito);

                if (!digito.Equals(digitoCalculado))
                    errors.Add(messageProducer.GetMessage(new RenavamError(RenavamErrorEnum.INVALID_CHECK_DIGIT)));
            }
            return errors;
        }

        private string CalculaDigito(string renavamSemDigito)
        {
            return new DigitoPara(renavamSemDigito).ComplementarAoModulo().TrocandoPorSeEncontrar("0", 10, 11).Mod(11).Calcula();
        }

        private string FormataPadraoNovo(string renavam)
        {
            if ((isFormatted && renavam.Length == 11) || (!isFormatted && renavam.Length == 9))
            {
                return "00" + renavam;
            }
            return renavam;
        }
    }
}
