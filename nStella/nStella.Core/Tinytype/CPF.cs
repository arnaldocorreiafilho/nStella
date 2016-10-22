using nStella.Core.Format;
using nStella.Core.Validation;

namespace nStella.Core.Tinytype
{
    public sealed class CPF
    {

        /**
         * Número do CPF. Possui apenas os caracteres numéricos.
         */
        private readonly string numero;

        /**
         * Número do CPF. No formato ddd.ddd.ddd-dd .
         */
        private readonly string numeroFormatado;

        /**
         * Constrói um CPF com o número especificado. Se o número contiver 
         * apenas caracteres numéricos ou estiver no formato ddd.ddd.ddd-dd, 
         * ele é guardado com e sem formatação nos respectivos atributos.
         * Caso contrário, ele guarda o parâmetro como passado em ambos os atributos.
         * 
         * @param numero número do CPF
         */
        public CPF(string numero)
        {
            CPFFormatter formatador = new CPFFormatter();
            if (formatador.IsFormatted(numero))
            {
                this.numero = formatador.UnFormat(numero);
                numeroFormatado = numero;
            }
            else if (formatador.CanBeFormatted(numero))
            {
                this.numero = numero;
                numeroFormatado = formatador.Format(numero);
            }
            else
            {
                this.numero = numeroFormatado = numero;
            }
        }

        /**
         * Retorna o número do CPF apenas com os caracteres numéricos.
         * 
         * @return número do CPF.
         */
        public string GetNumero()
        {
            return numero;
        }

        /**
         * Retorna o número do CPF no formato ddd.ddd.ddd-dd .
         * 
         * @return número do CPF no formato ddd.ddd.ddd-dd .
         */
        public string GetNumeroFormatado()
        {
            return numeroFormatado;
        }

        /**
         * Retorna se o número do CPF é válido. O resultado é <code>true</code>
         * se os dígitos verificadores estão de acordo com a regra de cálculo.
         * 
         * @return se o número do CPF é valido.
         * 
         * @see CPFValidator
         */
        public bool IsValido()
        {
            return new CPFValidator().InvalidMessageFor(numero).Count == 0;
        }

        /**
         * Retorna uma representação em string desse CPF. A intenção desse método
         * é ser usado para impressão e retorna o número no formato ddd.ddd.ddd-dd .
         * 
         * @return número do CPF no formato ddd.ddd.ddd-dd.
         */

        public override string ToString()
        {
            return GetNumeroFormatado();
        }

        /**
         * Retorna um hash code para esse CPF.
         * 
         * @return um valor de hash code para esse objeto.
         */

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = prime * result + ((numero == null) ? 0 : numero.GetHashCode());
            return result;
        }

        /**
         * Compara esse <code>CPF</code> ao <code>Object</code> 
         * especificado.  O resultado é <code>true</code> se e só se
         * o argumento é um objeto <code>CPF</code> com o mesmo número.
         * 
         * @param obj o objeto a ser comparado
         * @return <code>true</code> se esse objeto é igual a <code>obj</code>;
         * <code>false</code> caso contrário.
         */
        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null)
            {
                return false;
            }
            if (GetType() != obj.GetType())
            {
                return false;
            }
            CPF other = (CPF)obj;
            if (numero == null)
            {
                if (other.numero != null)
                {
                    return false;
                }
            }
            else if (!numero.Equals(other.numero))
            {
                return false;
            }
            return true;
        }

    }
}
