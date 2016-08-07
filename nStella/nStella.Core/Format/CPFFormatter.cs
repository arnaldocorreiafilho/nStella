using nStella.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Format
{
    public class CPFFormatter : IFormatter
    {
        private readonly BaseFormatter formatter;
        public CPFFormatter()
        {
            formatter = new BaseFormatter(CPFValidator.FORMATED, "$1.$2.$3-$4", CPFValidator.UNFORMATED, "$1$2$3$4");
        }
        public bool CanBeFormatted(string value)
        {
            return formatter.CanBeFormatted(value);
        }

        public string Format(string value)
        {
            return formatter.Format(value);
        }

        public bool IsFormatted(string value)
        {
            return formatter.IsFormatted(value);
        }

        public string UnFormat(string value)
        {
            return formatter.UnFormat(value);
        }
    }
}
