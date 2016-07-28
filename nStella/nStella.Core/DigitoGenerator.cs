using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core
{
    public class DigitoGenerator
    {
        private static readonly Random RANDOM = new Random();
        public string generate(int quantidade)
        {
            StringBuilder digitos = new StringBuilder();

            for (int i = 0; i < quantidade; i++)
            {
                digitos.Append(RANDOM.Next());

            }
            return digitos.ToString();
        }
    }
}
