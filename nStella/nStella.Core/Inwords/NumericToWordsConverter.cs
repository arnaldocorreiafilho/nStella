using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Resources;
using System.Threading.Tasks;

namespace nStella.Core.Inwords
{
    public class NumericToWordsConverter
    {
        private readonly IFormatoDeExtenso formato;
        private readonly CultureInfo cultureInfo;

        public NumericToWordsConverter(IFormatoDeExtenso formato)
        {
            this.formato = formato;
            cultureInfo = Messages.LOCALE_PT_BR;
        }

        public NumericToWordsConverter(IFormatoDeExtenso formato,CultureInfo cultureInfo)
        {
            this.formato = formato;
            this.cultureInfo = cultureInfo;
        }

        public string ToWords(long number)
        {
            try
            {
                if (number < 0)
                    throw new ArgumentException("Não é possível transformar números negativos.");

                StringBuilder result = new StringBuilder();

                if (number == 0)
                    result.Append(GetNumber(0));
                else
                {
                    NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
                    numberFormatInfo.NumberGroupSeparator = ",";
                    numberFormatInfo.NumberDecimalSeparator = ".";
                    decimal decimalNumber = Convert.ToDecimal(number);                    
                    string formattedInt = decimalNumber.ToString("###,###", numberFormatInfo);
                    string[] ints = formattedInt.Split(',');

                    ThousandBlock[] blocks = new ThousandBlock[ints.Length];

                    for (int i = 0; i < blocks.Length; i++)
                    {
                        string block = ints[i];
                        blocks[i] = new ThousandBlock(this, block);
                    }

                    AppendIntegers(result, blocks);
                    AppendIntegersUnits(number, result, blocks);
                }
                return result.ToString();
            }
            catch (MissingManifestResourceException)
            {
                throw new ArgumentException("Número muito grande para ser transformado em extenso.");
            }
        }
        /// <summary>
        /// Incompleto.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string ToWords(double number)
        {
            try
            {
                if (number < 0)
                {
                    throw new ArgumentException("Não é possível transformar números negativos.");
                }
                StringBuilder result = new StringBuilder();
                if (number == 0)
                {
                    result.Append(GetNumber(0));
                }
                else
                {

                    String[] parts = split(number);
                    String formattedInt = parts[0];
                    String[] ints = formattedInt.split("[,]");
                    ThousandBlock[] blocks = new ThousandBlock[ints.length];
                    for (int i = 0; i < blocks.length; i++)
                    {
                        String block = ints[i];
                        blocks[i] = new ThousandBlock(block);
                    }
                    String formattedMod = parts[1];
                    ThousandBlock modBlock = new ThousandBlock(formattedMod);

                    boolean hasMod = !modBlock.isZero();
                    boolean hasInteger = (blocks.length > 1) || (!blocks[blocks.length - 1].isZero());

                    if (hasInteger)
                    {
                        appendIntegers(result, blocks);
                        appendIntegersUnits(number, result, blocks);
                    }
                    if (hasInteger && hasMod)
                    {
                        result.append(getAndSeparator());
                    }
                    if (hasMod)
                    {
                        appendIntegers(result, modBlock);
                        appendDecimalUnits(result, modBlock);
                    }
                }
                return result.toString();
            }
            catch (MissingResourceException e)
            {
                throw new IllegalArgumentException("Número muito grande para ser transformado em extenso.");
            }
        }

        private void AppendIntegersUnits(long number, StringBuilder result, ThousandBlock[] blocks)
        {
            if (blocks.Length != 1 || !blocks[0].IsZero())
            {
                if (number >= 2)
                {
                    string unit = formato.GetUnidadeInteiraNoPlural();
                    if (!string.IsNullOrEmpty(unit))
                    {
                        result.Append(" ");
                        int length = blocks.Length;
                        if (length > 2 && blocks[length - 1].IsZero() && blocks[length - 2].IsZero())
                        {
                            result.Append(GetFormatSeparator());
                        }
                        result.Append(unit);
                    }
                }
                else
                {
                    string unit = formato.GetUnidadeInteiraNoSingular();
                    if (!string.IsNullOrEmpty(unit))
                    {
                        result.Append(" ").Append(unit);
                    }
                }
            }
        }

        private string GetFormatSeparator()
        {
            return GetString("sep.formato");
        }

        private void AppendIntegers(StringBuilder result, params ThousandBlock[] blocks)
        {
            bool hasStarted = false;
            for (int i = 0; i < blocks.Length; i++)
            {
                ThousandBlock thousandBlock = blocks[i];
                if(!(hasStarted && thousandBlock.IsZero()))
                {
                    int thousandPower = (blocks.Length - i - 1);
                    if (hasStarted)
                    {
                        if (thousandBlock.IsUnitary())                        
                            result.Append(GetAndSeparator());                        
                        else
                        {
                            if (thousandPower < 1)
                                result.Append(GetAndSeparator());
                            else
                                result.Append(GetThousandSeparator());
                        }
                    }

                    result.Append(thousandBlock.ToWords());
                    if (thousandPower > 0)
                    {
                        result.Append(" ");
                        result.Append(GetString("1e" + 3 * thousandPower + "."
                            + (thousandBlock.IsUnitary() ? "singular" : "plural")));
                    }
                    hasStarted = true;
                }                
            }
        }

        private string GetNumber(int v)
        {
            throw new NotImplementedException();
        }

        private sealed class ThousandBlock
        {
            private int numberValue;
            private readonly NumericToWordsConverter NumericToWordsConverter;
            public ThousandBlock(NumericToWordsConverter NumericToWordsConverter, string number)
            {
                if (number.Length > 3)
                    throw new ArgumentException("ThousandBlock deve conter numeros de no maximo 3 digitos.");

                numberValue = int.Parse(number);

                this.NumericToWordsConverter = NumericToWordsConverter;
            }

            public bool IsZero()
            {
                return numberValue == 0;
            }

            public bool IsUnitary()
            {
                return numberValue == 1;
            }

            public string ToWords()
            {
                string result;

                if (numberValue <= 20)
                    result = NumericToWordsConverter.GetNumber(numberValue);
                else if (numberValue <= 99)
                {
                    result = GetNumberUnder100(numberValue);
                }
                else if (numberValue == 100)
                {
                    result = NumericToWordsConverter.GetNumber(100);
                }
                else
                {
                    int c = (numberValue / 100) * 100;
                    string centena;
                    if (c == 100)
                    {
                        centena = NumericToWordsConverter.GetString("100+?");
                    }
                    else
                    {
                        centena = NumericToWordsConverter.GetNumber(c);
                    }
                    int resto = numberValue % 100;
                    if (resto == 0)
                    {
                        result = centena;
                    }
                    else
                    {
                        result = centena + NumericToWordsConverter.GetAndSeparator() + GetNumberUnder100(resto);
                    }
                }
                return result;
            }         

            private string GetNumberUnder100(int number)
            {
                string result = null;
                if (number <= 20)
                {
                    result = NumericToWordsConverter.GetNumber(number);
                }
                else if (number <= 99)
                {
                    int d = number / 10;
                    int u = number % 10;
                    string dezena = NumericToWordsConverter.GetNumber(d * 10);
                    if (u == 0)
                    {
                        result = dezena;
                    }
                    else
                    {
                        string unidade = NumericToWordsConverter.GetNumber(u);
                        result = dezena + NumericToWordsConverter.GetTensSeparator() + unidade;
                    }
                }
                return result;
            }
        }

        private string GetAndSeparator()
        {
            return GetString("sep");
        }
        private string GetTensSeparator()
        {
            return GetString("sep.dezena");
        }

        private string GetString(string paramMessage)
        {
            return Messages.GetString("Extenso." + paramMessage, cultureInfo);
        }

        private string GetThousandSeparator()
        {
            return GetString("sep.mil");
        }
    }

    
}
