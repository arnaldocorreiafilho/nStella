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
    public class CPFValidator : IValidator<string>
    {
        public static readonly Regex FORMATED = new Regex("(\\d{3})[.](\\d{3})[.](\\d{3})-(\\d{2})");
        public static readonly Regex UNFORMATED = new Regex("(\\d{3})(\\d{3})(\\d{3})(\\d{2})");

        private readonly bool isFormatted;
        private readonly bool isIgnoringRepeatedDigits;
        private readonly IMessageProducer messageProducer;

        public CPFValidator() :
            this(new SimpleMessageProducer(), false, false)
        {

        }
        public CPFValidator(bool isFormatted)
            : this(new SimpleMessageProducer(), isFormatted, false)
        {

        }
        public CPFValidator(bool isFormatted, bool isIgnoringRepeatedDigits)
            : this(new SimpleMessageProducer(), isFormatted, isIgnoringRepeatedDigits)
        {

        }
        public CPFValidator(IMessageProducer messageProducer, bool isFormatted)
            : this(messageProducer, isFormatted, false)
        {

        }
        public CPFValidator(IMessageProducer messageProducer, bool isFormatted, bool isIgnoringRepeatedDigits)
        {
            this.messageProducer = messageProducer;
            this.isFormatted = isFormatted;
            this.isIgnoringRepeatedDigits = isIgnoringRepeatedDigits;
        }

        private IList<IValidationMessage> GetInvalidValues(string cpf)
        {
            IList<IValidationMessage> errors = new List <IValidationMessage>();

            if (!string.IsNullOrEmpty(cpf))
            {
                if (isFormatted != FORMATED.Match(cpf).Success)                
                    errors.Add(messageProducer.GetMessage(new CPFError(CPFErrorEnum.INVALID_FORMAT)));

                string unformatedCpf = string.Empty;
                try
                {
                    unformatedCpf = new CPFFormatter().UnFormat(cpf);
                }
                catch (ArgumentException)
                {
                    errors.Add(messageProducer.GetMessage(new CPFError(CPFErrorEnum.INVALID_DIGITS)));
                    return errors;
                }

                var cpfRegex = new Regex("[0-9]*");
                if (unformatedCpf.Length != 11 || !cpfRegex.IsMatch("[0-9]*"))
                    errors.Add(messageProducer.GetMessage(new CPFError(CPFErrorEnum.INVALID_DIGITS)));

                if ((!isIgnoringRepeatedDigits) && hasAllRepeatedDigits(unformatedCpf))
                    errors.Add(messageProducer.GetMessage(new CPFError(CPFErrorEnum.REPEATED_DIGITS)));

                string cpfSemDigito = unformatedCpf.Substring(0, unformatedCpf.Length - 2);
                string digitos = unformatedCpf.Substring(unformatedCpf.Length - 2);

                string digitosCalculados = calculaDigitos(cpfSemDigito);

                if (!digitos.Equals(digitosCalculados))
                    errors.Add(messageProducer.GetMessage(new CPFError(CPFErrorEnum.INVALID_CHECK_DIGITS)));

            }
            return errors;
        }

        private string calculaDigitos(string cpfSemDigito)
        {
            DigitoPara digitoPara = new DigitoPara(cpfSemDigito);
            digitoPara.ComMultiplicadoresDeAte(2, 11).ComplementarAoModulo().TrocandoPorSeEncontrar("0", 10, 11).Mod(11);

            string digito1 = digitoPara.Calcula();
            digitoPara.AddDigito(digito1);
            string digito2 = digitoPara.Calcula();

            return digito1 + digito2;
        }

        private bool hasAllRepeatedDigits(string cpf)
        {
            for (int i = 1; i < cpf.Length; i++)
            {
                if (cpf[i] != cpf[0])
                {
                    return false;
                }
            }
            return true;
        }

        public void AssertValid(string cpf)
        {
            IList<IValidationMessage> errors = GetInvalidValues(cpf);
            if (errors.Count() != 0)
                throw new InvalidStateException(errors);

        }

        public string GenerateRandomValid()
        {
            string cpfSemDigitos = new DigitoGenerator().Generate(9);
            string cpfComDigitos = cpfSemDigitos + calculaDigitos(cpfSemDigitos);
            if (isFormatted)
            {
                return new CPFFormatter().Format(cpfComDigitos);
            }
            return cpfComDigitos;
        }

        public IList<IValidationMessage> InvalidMessageFor(string cpf)
        {
            return GetInvalidValues(cpf);
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