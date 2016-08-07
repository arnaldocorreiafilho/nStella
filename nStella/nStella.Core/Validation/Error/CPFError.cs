using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Validation.Error
{
    public class CPFError : IInvalidValue
    {
        private readonly CPFErrorEnum error;
        public CPFError(CPFErrorEnum error)
        {
            this.error = error;
        }

        public string Name()
        {
            return error.ToString();
        }
    }

    public enum CPFErrorEnum
    {
        INVALID_CHECK_DIGITS, INVALID_DIGITS, INVALID_FORMAT,REPEATED_DIGITS
    }
}
