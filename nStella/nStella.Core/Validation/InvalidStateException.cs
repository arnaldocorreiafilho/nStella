using System;
using System.Collections.Generic;

namespace nStella.Core.Validation
{
    [Serializable]
    public class InvalidStateException : Exception
    {
        private readonly IList<IValidationMessage> validationMessages;

        private static readonly long serialVersionUID = 1L;

        public InvalidStateException(IValidationMessage validationMessage)
            :this(new List<IValidationMessage>() { validationMessage })
        {                        
        }

        public InvalidStateException(IList<IValidationMessage> validationMessages)
            : base("Validations errors: "+ validationMessages)
        {
            this.validationMessages = validationMessages;
        }
        public IList<IValidationMessage> GetInvalidMessages()
        {
            return validationMessages;
        }
    }
}
