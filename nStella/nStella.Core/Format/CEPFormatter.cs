using System.Text.RegularExpressions;

namespace nStella.Core.Format
{
    public class CEPFormatter : IFormatter
    {
        public static readonly Regex FORMATED = new Regex("(\\d{5})-(\\d{3})");
        public static readonly Regex UNFORMATED = new Regex("(\\d{5})(\\d{3})");
        private readonly BaseFormatter baseFormatter;

        public CEPFormatter()
        {
            baseFormatter = new BaseFormatter(FORMATED, "$1-$2", UNFORMATED, "$1$2");
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
