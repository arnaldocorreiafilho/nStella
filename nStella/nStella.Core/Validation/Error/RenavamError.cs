using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Validation.Error
{
    public class RenavamError : IInvalidValue
    {
        private readonly RenavamErrorEnum error;
        public RenavamError(RenavamErrorEnum error)
        {
            this.error = error;
        }

        public string Name()
        {
            return error.ToString();
        }
    }

    public enum RenavamErrorEnum
    {
        INVALID_FORMAT, INVALID_CHECK_DIGIT, INVALID_DIGITS
    }
}
