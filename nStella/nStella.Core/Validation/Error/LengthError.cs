using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Validation.Error
{
    public class LengthError : IInvalidValue
    {
        private readonly int validLength;

        public LengthError(int validLength)
        {
            this.validLength = validLength;
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = prime * result + validLength;
            return result;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;

            if (obj == null)
                return false;

            if (!(obj is LengthError))
                return false;

            LengthError other = (LengthError)obj;

            if (validLength != other.validLength)
                return false;


            return true;
        }

        public int ValidLength
        {
            get { return validLength; }
        }

        public string Name()
        {
            return "INVALID_LENGTH";
        }
    }
}
