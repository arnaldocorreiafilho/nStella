using nStella.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Format
{
    public class CNPJFormatter : IFormatter
    {
        private readonly BaseFormatter baseFormatter;
        public CNPJFormatter()
        {
            baseFormatter = new BaseFormatter(CNPJValidator.FORMATED, "$1.$2.$3/$4-$5", CNPJValidator.UNFORMATED, "$1$2$3$4$5");
        }

        public bool CanBeFormatted(string value)
        {
            return baseFormatter.CanBeFormatted(value);
        }
        public string Format(string value)
        {
            return baseFormatter.Format(value);
        }
        public bool IsFormatted(string value)
        {
            return baseFormatter.IsFormatted(value);
        }
        public string UnFormat(string value)
        {
            return baseFormatter.UnFormat(value);
        }
    }
}
