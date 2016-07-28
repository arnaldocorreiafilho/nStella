using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core
{
    public class SimpleValidationMessage : IValidationMessage
    {
        private readonly string message;
        public SimpleValidationMessage(string message)
        {
            this.message = message;
        }

        public string GetMessage()
        {
            return this.message;
        }
        public override string ToString()
        {
            return GetMessage();
        }
    }
}
