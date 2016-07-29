/**
 * Formatter é responsável por transfomar cadeias sem formatação em cadeias
 * formatadas e vice-versa.
 * 
 */
namespace nStella.Core.Format
{
    public interface IFormatter
    {
        /// <summary>
        /// Formata uma cadeia.
        /// </summary>
        /// <param name="value">cadeia sem formatado</param>
        /// <returns></returns>
        string Format(string value);
        string UnFormat(string value);
        bool IsFormatted(string value);
        bool CanBeFormatted(string value);        
    }
}
