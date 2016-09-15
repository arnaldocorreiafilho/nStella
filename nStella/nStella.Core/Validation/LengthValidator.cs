using nStella.Core.Validation.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Validation
{
    public class LengthValidator : IValidator<object>
    {
        private readonly int validLength;
        private readonly BaseValidator baseValidator;

        public LengthValidator(int validLength)
        {
            this.validLength = validLength;
            baseValidator = new BaseValidator();
        }

        public LengthValidator(IMessageProducer messageProducer, int validLength)
        {
            baseValidator = new BaseValidator(messageProducer);
            this.validLength = validLength;
        }

        private IList<IInvalidValue> GetInvalidValuesFor(object obj)
        {
            IList<IInvalidValue> messages = new List<IInvalidValue>();
            if (obj.ToString().Length != validLength)
            {
                messages.Add(new LengthError(validLength));
            }
            return messages;
        }

        public void AssertValid(object Object)
        {
            baseValidator.AssertValid(GetInvalidValuesFor(Object));
        }

        public object GenerateRandomValid()
        {
            throw new NotSupportedException("Operação não suportada por este validador.");
        }

        public IList<IValidationMessage> InvalidMessageFor(object Object)
        {
            List<IValidationMessage> messages = new List<IValidationMessage>();
            IList<IValidationMessage> msgs = baseValidator.GenerateValidationMessages(GetInvalidValuesFor(Object));
            messages.AddRange(msgs);

            return messages;
        }

        public bool IsEligible(object Object)
        {
            return true;
        }
    }
}
