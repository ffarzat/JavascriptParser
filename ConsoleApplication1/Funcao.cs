using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    /// <summary>
    /// Representa uma funcao do JS
    /// </summary>
    public class Funcao
    {
        /// <summary>
        /// Nome da Função
        /// </summary>
        public string Nome;

        /// <summary>
        /// Lista dos argumentos da Função
        /// </summary>
        private readonly List<Argumento> _argumentos = new List<Argumento>();

        /// <summary>
        /// Lista dos argumentos da Função
        /// </summary>
        public List<Argumento> Argumentos
        {
            get { return _argumentos; }
        }


        /// <summary>
        /// Adiciona um Argumento a função
        /// </summary>
        /// <param name="nome">Nome do argumento</param>
        public void AddArgumento(string nome)
        {
            Argumentos.Add(new Argumento {Nome = nome});
        }

    }
}
