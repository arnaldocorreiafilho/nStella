using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace nStella.Core.Format
{
    public class CEPFormatter : IFormatter
    {
        public static readonly Regex FORMATED = new Regex("(\\d{5})-(\\d{3})");
        public static readonly Regex UNFORMATED = new Regex("(\\d{5})(\\d{3})");
        private readonly BaseFormatter baseFormatter;

        public bool CanBeFormatted(string value)
        {
            throw new NotImplementedException();
        }

        public string Format(string value)
        {
            throw new NotImplementedException();
        }

        public bool IsFormatted(string value)
        {
            throw new NotImplementedException();
        }

        public string UnFormat(string value)
        {
            throw new NotImplementedException();
        }
    }
}
