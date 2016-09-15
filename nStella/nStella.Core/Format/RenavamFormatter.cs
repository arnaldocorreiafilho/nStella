using System.Text.RegularExpressions;

namespace nStella.Core.Format
{
    public class RenavamFormatter : IFormatter
    {
        public static readonly Regex FORMATED = new Regex("(\\d{2,4}).(\\d{6})-(\\d{1})");
        public static readonly Regex UNFORMATED = new Regex("(^\\d{2,4})(\\d{6})(\\d{1}$)");

        private readonly BaseFormatter formatter;

        public RenavamFormatter()
        {
            formatter = new BaseFormatter(FORMATED, "$1.$2-$3", UNFORMATED, "$1$2$3");
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
