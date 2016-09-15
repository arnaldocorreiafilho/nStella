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
    public class TituloEleitoralValidator : IValidator<string>
    {
        public static readonly Regex FORMATED = new Regex("(\\d{10})/(\\d{2})");
        public static readonly Regex UNFORMATED = new Regex("(\\d{10})(\\d{2})");

        private bool isFormatted = false;
        private IMessageProducer messageProducer;


        public TituloEleitoralValidator()
        {
            messageProducer = new SimpleMessageProducer();
        }
        public TituloEleitoralValidator(bool isFormatted)
        {
            messageProducer = new SimpleMessageProducer();
            this.isFormatted = isFormatted;
        }

        public TituloEleitoralValidator(IMessageProducer messageProducer, bool isFormatted)
        {
            this.messageProducer = messageProducer;
            this.isFormatted = isFormatted;
        }

        public TituloEleitoralValidator(IMessageProducer messageProducer)
        {
            this.messageProducer = messageProducer;
        }

        private IList<IValidationMessage> GetInvalidValues(string tituloDeEleitor)
        {
            IList<IValidationMessage> errors = new List<IValidationMessage>();
            if (tituloDeEleitor != null)
            {

                if (isFormatted && !FORMATED.IsMatch(tituloDeEleitor))
                    errors.Add(messageProducer.GetMessage(new TituloEleitoralError(TituloEleitoralErrorEnum.INVALID_FORMAT)));

                string unformatedTitulo = null;
                try
                {
                    unformatedTitulo = new TituloEleitoralFormatter().UnFormat(tituloDeEleitor);
                }
                catch (ArgumentException)
                {
                    errors.Add(messageProducer.GetMessage(new TituloEleitoralError(TituloEleitoralErrorEnum.INVALID_DIGITS)));
                    return errors;
                }
                Regex tituloSemFormatacao = new Regex("[0-9]*");

                if (unformatedTitulo.Length != 12 || !tituloSemFormatacao.IsMatch("[0-9]*"))
                {
                    errors.Add(messageProducer.GetMessage(new TituloEleitoralError(TituloEleitoralErrorEnum.INVALID_DIGITS)));
                }

                if (HasCodigoDeEstadoInvalido(unformatedTitulo))
                {
                    errors.Add(messageProducer.GetMessage(new TituloEleitoralError(TituloEleitoralErrorEnum.INVALID_CODIGO_DE_ESTADO)));
                }
                else
                {

                    string tituloSemDigito = unformatedTitulo.Substring(0, unformatedTitulo.Length - 2);
                    string digitos = unformatedTitulo.Substring(unformatedTitulo.Length - 2);

                    string digitosCalculados = CalculaDigitos(tituloSemDigito);

                    if (!digitos.Equals(digitosCalculados))
                    {
                        errors.Add(messageProducer.GetMessage(new TituloEleitoralError(TituloEleitoralErrorEnum.INVALID_CHECK_DIGITS)));
                    }
                }
            }
            return errors;
        }

        private string CalculaDigitos(string tituloSemDigito)
        {
            int length = tituloSemDigito.Length;

            string sequencial = tituloSemDigito.Substring(0, length - 2);
            string digito1 = new DigitoPara(sequencial).ComplementarAoModulo().TrocandoPorSeEncontrar("0", 10, 11).Mod(11).Calcula();

            string codigoEstado = tituloSemDigito.Substring(length - 2, length);
            string digito2 = new DigitoPara(codigoEstado + digito1).ComplementarAoModulo().TrocandoPorSeEncontrar("0", 10, 11).Mod(11).Calcula();

            return digito1 + digito2;
        }

        private bool HasCodigoDeEstadoInvalido(string tituloDeEleitor)
        {
            int codigo = int.Parse(tituloDeEleitor.Substring(tituloDeEleitor.Length - 4, tituloDeEleitor.Length - 2));
            return !(codigo >= 01 && codigo <= 28);
        }

        public bool IsEligible(string tituloDeEleitor)
        {
            bool result;
            if (isFormatted)
            {
                result = FORMATED.IsMatch(tituloDeEleitor);
            }
            else
            {
                result = UNFORMATED.IsMatch(tituloDeEleitor);
            }
            return result;
        }

        public void AssertValid(string tituloDeEleitor)
        {
            IList<IValidationMessage> errors = GetInvalidValues(tituloDeEleitor);
            if (!(errors.Count == 0))
            {
                throw new InvalidStateException(errors);
            }
        }
       
        public string GenerateRandomValid()
        {
            string digitosSequenciais = new DigitoGenerator().Generate(8);
            string digitosEstado = string.Format("%02d", new Random().Next(28) + 1);
            string tituloSemDigito = digitosSequenciais + digitosEstado;
            string tituloComDigito = tituloSemDigito + CalculaDigitos(tituloSemDigito);
            if (isFormatted)
            {
                return new TituloEleitoralFormatter().Format(tituloComDigito);
            }
            return tituloComDigito;
        }
        
        public IList<IValidationMessage> InvalidMessageFor(string tituloDeEleitor)
        {
            return GetInvalidValues(tituloDeEleitor);
        }
    }
}