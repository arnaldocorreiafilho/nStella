using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Validation
{
    public class BaseValidator
    {
        private readonly IMessageProducer messageProducer;

        public BaseValidator()
        {
            messageProducer = new SimpleMessageProducer();
        }

        public BaseValidator(IMessageProducer messageProducer)
        {
            this.messageProducer = messageProducer;
        }

        public IList<IValidationMessage> GenerateValidationMessages(IList<IInvalidValue> invalidValues)
        {
            IList<IValidationMessage> messages = new List<IValidationMessage>();
            foreach (IInvalidValue invalidValue in invalidValues)
            {
                IValidationMessage message = messageProducer.GetMessage(invalidValue);
                messages.Add(message);
            }

            return messages;
        }

        public void AssertValid(IList<IInvalidValue> invalidValues)
        {
            if (!(invalidValues.Count == 0))
                throw new InvalidStateException(GenerateValidationMessages(invalidValues));
        }
    }
}
