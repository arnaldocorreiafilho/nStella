﻿using nStella.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Format
{
    public class TituloEleitoralFormatter : IFormatter
    {
        private readonly BaseFormatter formatter;

        public TituloEleitoralFormatter()
        {
            formatter = new BaseFormatter(TituloEleitoralValidator.FORMATED, "$1/$2", TituloEleitoralValidator.UNFORMATED, "$1$2");
        }


        public string Format(string value)
        {
            return formatter.Format(value);
        }


        public string UnFormat(string value)
        {
            return formatter.UnFormat(value);
        }


        public bool IsFormatted(string value)
        {
            return formatter.IsFormatted(value);
        }


        public bool CanBeFormatted(string value)
        {
            return formatter.CanBeFormatted(value);
        }
    }
}