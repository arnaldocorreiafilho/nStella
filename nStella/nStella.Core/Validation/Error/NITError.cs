using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Validation.Error
{
    public class NITError : IInvalidValue
    {
        private readonly NITErrorEnum nitEnum;
        public NITError(NITErrorEnum nitEnum)
        {
            this.nitEnum = nitEnum;
        }
        public string Name()
        {
            return nitEnum.ToString();
        }       
    }
    public enum NITErrorEnum
    {
        INVALID_FORMAT, INVALID_DIGITS, INVALID_CHECK_DIGITS
    }
}
