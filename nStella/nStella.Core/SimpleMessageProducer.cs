using nStella.Core.Validation;
using System.Text.RegularExpressions;

namespace nStella.Core
{
    /// <summary>
    /// SimpleMessageProducer é responsável pela geração de mensagens de erro. Estas                           
    ///mensagens são geradas atraves dos nomes das anotoções que representam os
    ///erros.
    ///</p>
    ///<p>                                                                                                    
    ///A messagem de erro é composta do seguinte modo:                                                        
    ///</p>                                                                                                   
    ///<code>                                                                                                 
    ///String message = (simpleName + "." + errorName).replaceFirst("[.]", " : ").replaceAll("_", " ");       
    ///</code>                                                                                                                                                                                                       
    ///<p>                                                                                                    
    ///Veja o exemplo:                                                                                        
    ///</p>                                                                                                   
    ///<p>                                                                                                    
    ///A mesagem do erro representado por CPFError.INVALID_DIGITS é : <br>                                      
    ///CPFError : INVALID DIGITS .                                                                            
    ///</p>                                                                                                   
    /// </summary>
    public class SimpleMessageProducer : IMessageProducer
    {
        public IValidationMessage GetMessage(IInvalidValue error)
        {
            string simpleName = error.GetType().Name;
            string errorName = error.Name();
            string key = simpleName + "." + errorName;
            string message;            
            message = key.Replace("."," : ").Replace("_", " ").ToString();

            return new SimpleValidationMessage(message);
        }
    }
}
