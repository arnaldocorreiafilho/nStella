using System;
using System.Text.RegularExpressions;

namespace nStella.Core.Format
{
    public class LeftSideZerosFormatter : IFormatter
    {
        private readonly int formattedLength;

        public LeftSideZerosFormatter(int formattedLength)
        {
            this.formattedLength = formattedLength;
        }

        public bool CanBeFormatted(string value)
        {
            Match match = Regex.Match(value, "\\d{0," + formattedLength + "}");
            return match.Success && match.Length == value.Length;            
        }

        public string Format(string value)
        {
            if (!CanBeFormatted(value))
                throw new ArgumentException("Argument value must have only " + formattedLength + " digits at most.");

            string formated = value;
            while (formated.Length < formattedLength)
                formated = "0" + formated;

            return formated;
         }

        public bool IsFormatted(string value)
        {
            Match match = Regex.Match(value, "\\d{" + formattedLength + "}");
            return match.Success && match.Length == value.Length;            
        }

        public string UnFormat(string value)
        {
            int integer = Convert.ToInt32(value);
            string formated = integer.ToString();
            return formated;
        }
    }
}
