using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Validation.Error
{
    public class CNPJError : IInvalidValue
    {
        private readonly CNPJErrorEnum error;
        public CNPJError(CNPJErrorEnum error)
        {
            this.error = error;
        }

        public string Name()
        {
            return error.ToString();
        }
    }

    public enum CNPJErrorEnum
    {
        INVALID_CHECK_DIGITS, INVALID_DIGITS, INVALID_FORMAT;
    }
}
