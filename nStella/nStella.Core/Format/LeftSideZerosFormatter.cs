using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            Regex regx = new Regex("\\d{" + formattedLength + "}");
            return regx.IsMatch(value);
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
            Regex regx = new Regex("\\d{" + formattedLength + "}");
            return regx.IsMatch(value);
        }

        public string UnFormat(string value)
        {
            int integer = Convert.ToInt32(value);
            string formated = integer.ToString();
            return formated;
        }
    }
}
