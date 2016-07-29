using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace nStella.Core.Format
{
    public class BaseFormatter : IFormatter
    {
        private readonly Regex formatted;
        private readonly string formattedReplacement;
        private readonly Regex unformatted;
        private readonly string unformattedReplacement;

        public bool CanBeFormatted(string value)
        {
            return unformatted.Match(value).Success;
        }

        public string Format(string value)
        {
            string result;
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value may to be null.");

            Match matcher = unformatted.Match(value);
            result = MatchAndReplace(matcher, formattedReplacement);
            return result;
        }

        public bool IsFormatted(string value)
        {
            return formatted.Match(value).Success;
        }

        public string UnFormat(string value)
        {
            string result;
            if (value == null)
            {
                throw new ArgumentException("Value may not be null.");
            }

            Match unformattedMatcher = unformatted.Match(value);
            if (unformattedMatcher.Success && value == unformattedMatcher.Value)
            {
                return value;
            }

            Match matcher = formatted.Match(value);
            result = MatchAndReplace(matcher, unformattedReplacement);
            return result;
        }

        private string MatchAndReplace(Match matcher, string replacement)
        {
            string result = null;
            if (matcher.Success)
            {
                result = matcher.Result(replacement);
            }
            else
            {
                throw new ArgumentException("Value is not properly formatted.");
            }
            return result;
        }

        public BaseFormatter(Regex formatted, string formattedReplacement, Regex unformatted, string unformattedReplacement)
        {
            this.formatted = formatted;
            this.formattedReplacement = formattedReplacement;
            this.unformatted = unformatted;
            this.unformattedReplacement = unformattedReplacement;
        }
    }
}
