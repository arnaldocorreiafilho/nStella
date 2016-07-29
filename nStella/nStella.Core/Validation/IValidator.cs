using System.Collections.Generic;

namespace nStella.Core.Validation
{
    public interface IValidator<T>
    {
        void AssertValid(T Object);
        IList<IValidationMessage> InvalidMessageFor(T Object);
        bool IsEligible(T Object);
        T GenerateRandomValid();
    }
}
