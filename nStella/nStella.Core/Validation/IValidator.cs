using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core.Validation
{
    public interface IValidator<T>
    {
        void AssertValid(T Object);
        List<IValidationMessage> InvalidMessageFor(T Object);
        bool IsEligible(T Object);
        T GenerateRandomValid();
    }
}
