using nStella.Core.Format;

namespace nStella.Core.Tinytype
{
    public sealed class CEP
    {
        private string numero;
        private string numeroFormatado;

        public CEP(string numero)
        {
            CEPFormatter formatador = new CEPFormatter();
            if (formatador.IsFormatted(numero))
            {
                this.numero = formatador.UnFormat(numero);
                this.numeroFormatado = numero;
            }
            else if (formatador.CanBeFormatted(numero))
            {
                this.numero = numero;
                this.numeroFormatado = formatador.Format(numero);
            }
            else
            {
                this.numero = this.numeroFormatado = numero;
            }
        }

        public string GetNumero()
        {
            return numero;
        }

        public string GetNumeroFormatado()
        {
            return numeroFormatado;
        }


        public override int GetHashCode()
        {

            int prime = 31;
            int result = 1;
            result = prime * result + ((numero == null) ? 0 : numero.GetHashCode());
            return result;
        }

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
            CEP other = (CEP)obj;
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


        public override string ToString()
        {
            return GetNumeroFormatado();
        }

    }
}