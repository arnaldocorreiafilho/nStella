using nStella.Core.Format;
using nStella.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Tinytype
{
    public sealed class CNPJ
    {
        /**
    * Número do CNPJ. Possui apenas os caracteres numéricos.
    */
        private readonly String numero;

        /**
         * Número do CNPJ. No formato dd.ddd.ddd/dddd-dd .
         */
        private string numeroFormatado;

        /**
         * Constrói um CPF com o número especificado. Se o número contiver 
         * apenas caracteres numéricos ou estiver no formato dd.ddd.ddd/dddd-dd,
         * ele é guardado com e sem formatação nos respectivos atributos.
         * Caso contrário, ele guarda o parâmetro como passado em ambos os atributos.
         * 
         * @param numero número do CPF.
         */
        public CNPJ(string numero)
        {
            CNPJFormatter formatador = new CNPJFormatter();
            if (formatador.IsFormatted(numero))
            {
                this.numero = formatador.UnFormat(numero);
                this.numeroFormatado = numero;
            }
            else if (formatador.CanBeFormatted(numero))
            {
                this.numero = numero;
                numeroFormatado = formatador.Format(numero);
            }
            else
            {
                this.numero = this.numeroFormatado = numero;
            }
        }

        /**
         * Retorna o número do CNPJ apenas com os caracteres numéricos.
         * 
         * @return número do CNPJ.
         */
        public string GetNumero()
        {
            return numero;
        }

        /**
         * Retorna o número do CNPJ no formato dd.ddd.ddd/dddd-dd .
         * 
         * @return número do CNPJ no formato dd.ddd.ddd/dddd-dd .
         */
        public string GetNumeroFormatado()
        {
            return numeroFormatado;
        }

        /**
         * Retorna se o número do CNPJ é válido. O resultado é <code>true</code>
         * se os dígitos verificadores estão de acordo com a regra de cálculo.
         * 
         * @return se o número do CNPJ é valido.
         * 
         * @see CNPJValidator
         */
        public bool IsValid()
        {
            return new CNPJValidator().InvalidMessageFor(numero).Count == 0;
        }

        /**
         * Retorna uma representação em string desse CNPJ. A intenção desse método
         * é ser usado para impressão e retorna o número no formato dd.ddd.ddd/dddd-dd .
         * 
         * @return número do CNPJ no formato dd.ddd.ddd/dddd-dd .
         */

        public override string ToString()
        {
            return GetNumeroFormatado();
        }

        /**
         * Retorna um hash code para esse CPNJ.
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
         * Compara esse <code>CNPJ</code> ao <code>Object</code> 
         * especificado.  O resultado é <code>true</code> se e só se
         * o argumento é um objeto <code>CNPJ</code> com o mesmo número.
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
            CNPJ other = (CNPJ)obj;
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
