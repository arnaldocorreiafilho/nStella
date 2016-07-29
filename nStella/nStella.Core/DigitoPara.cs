using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nStella.Core
{
    public class DigitoPara
    {
        private LinkedList<int> numero;
        private IList<int> multiplicadores = new List<int>();
        private bool complementar;
        private int modulo;
        private bool somarIndividual;
        private IDictionary<int, string> substituicoes;

        /**
         *Cria o objeto a ser preenchido com interface fluente e armazena o trecho numérico     
         *em uma lista de algarismos. Isso é necessário porque a linha digitada pode ser        
         *muito maior do que um int suporta.                                                    
         *
         * trecho Refere-se à linha numérica sobre a qual o dígito deve ser calculado     
         */
        public DigitoPara(string trecho)
        {
            ComMultiplicadoresDeAte(2, 9);
            Mod(11);
            substituicoes = new Dictionary<int, string>();
            numero = new LinkedList<int>();
            char[] digitos = trecho.ToCharArray();
            foreach (char digito in digitos)
            {                
                numero.AddFirst((int)char.GetNumericValue(digito));
            }

            numero.Reverse();
        }
        /**                                                                                           
         * Para multiplicadores (ou pesos) sequenciais e em ordem crescente, esse método permite      
         * criar a lista de multiplicadores que será usada ciclicamente, caso o número base seja      
         * maior do que a sequência de multiplicadores. Por padrão os multiplicadores são iniciados   
         * de 2 a 9. No momento em que você inserir outro valor este default será sobrescrito.        
         *                                                                                            
         * @param inicio Primeiro número do intervalo sequencial de multiplicadores
         * @param fim Último número do intervalo sequencial de multiplicadores
         * @return this                                                                               
         */
        public DigitoPara ComMultiplicadoresDeAte(int inicio, int fim)
        {
            multiplicadores.Clear();
            for (int i = inicio; i <= fim; i++)
            {
                multiplicadores.Add(i);
            }
            return this;
        }
        /**                                                                                     
         * Há documentos em que os multiplicadores não usam todos os números de um intervalo    
         * ou alteram sua ordem. Nesses casos, a lista de multiplicadores pode ser passada      
         * através de varargs.                                                                  
         *                                                                                      
         * @param multiplicadoresEmOrdem Sequência de inteiros com os multiplicadores em ordem  
         * @return this                                                                         
         */
        public DigitoPara ComMultiplicadores(params int[] multiplicadoresEmOrdem)
        {
            multiplicadores = multiplicadoresEmOrdem.ToList();
            return this;
        }
        /**                                                                                
         * @param modulo Inteiro pelo qual o resto será tirado e também seu complementar.  
         * 			O valor padrão é 11.                                                   
         *                                                                                 
         * @return this                                                                    
         */
        public DigitoPara Mod(int modulo)
        {
            this.modulo = modulo;
            return this;
        }

        /**                                                                              
         * É comum que os geradores de dígito precisem do complementar do módulo em vez  
         * do módulo em sí. Então, a chamada desse método habilita a flag que é usada    
         * no método mod para decidir se o resultado devolvido é o módulo puro ou seu    
         * complementar.                                                                 
         *                                                                               
         * @return this                                                                  
         */
        public DigitoPara ComplementarAoModulo()
        {
            complementar = true;
            return this;
        }

        public DigitoPara TrocandoPorSeEncontrar(string substituto, params int[] i)
        {
            foreach (int integer in i)
            {
                substituicoes.Add(integer, substituto);
            }
            return this;
        }

        /// <summary>
        /// Indica se ao calcular o módulo, se a soma dos resultados da multiplicação deve ser  
        /// considerado digito a dígito.
        /// Ex: 2 X 9 = 18, irá somar 9 (1 + 8) invés de 18 ao total.
        /// </summary>
        /// <returns>this</returns>
        public DigitoPara SomandoIndividualmente()
        {
            somarIndividual = true;
            return this;
        }
        /// <summary>
        /// Faz a soma geral das multiplicações dos algarismos pelos multiplicadores, tira o       
        /// módulo e devolve seu complementar.        
        /// </summary>
        /// <returns>String o dígito vindo do módulo com o número passado e configurações extra.</returns>
        public string Calcula()
        {
            int soma = 0;
            int multiplicadorDaVez = 0;
            foreach (int algarismo in numero)
            {
                int multiplicador = multiplicadores[multiplicadorDaVez];
                int total = algarismo * multiplicador;
                soma += somarIndividual ? SomaDigitos(total) : total;

                multiplicadorDaVez = ProximoMultiplicador(multiplicadorDaVez);
            }
            int resultado = soma % modulo;
            if (complementar)
                resultado = modulo - resultado;

            if (substituicoes.ContainsKey(resultado))
            {
                return substituicoes[resultado];
            }

            return Convert.ToString(resultado);

        }
        /// <summary>
        /// Devolve o próximo multiplicador a ser usado, isto é, a próxima posição da lista de
        /// multiplicadores ou, se chegar ao fim da lista, a primeira posição, novamente.
        /// </summary>
        /// <param name="multiplicadorDaVez">multiplicadorDaVez Essa é a posição do último multiplicador usado.</param>
        /// <returns>próximo multiplicador</returns>
        private int ProximoMultiplicador(int multiplicadorDaVez)
        {
            multiplicadorDaVez++;
            if (multiplicadorDaVez == multiplicadores.Count)
                multiplicadorDaVez = 0;
            return multiplicadorDaVez;

        }
        /// <summary>
        /// soma os dígitos do número (até 2) 
        /// Ex: 18 => 9 (1+8), 12 => 3 (1+2)                                    
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        private int SomaDigitos(int total)
        {
            return (total / 10) + (total % 10);
        }       
        /// <summary>
        /// Adiciona um dígito no final do trecho numérico.
        /// </summary>
        /// <param name="digito"> digito É o dígito a ser adicionado.</param>
        /// <returns>this</returns>
        public DigitoPara AddDigito(string digito)
        {
            numero.AddFirst(Convert.ToInt32(digito));
            return this;
        }
    }
}
