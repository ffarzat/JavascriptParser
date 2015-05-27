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
        private string _nome;

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
        /// Nome da Função
        /// </summary>
        public string Nome
        {
            get { return Argumentos.Count == 0 ? _nome : _nome + "_" + Argumentos.Count; }
            set { _nome = value; }
        }

        /// <summary>
        /// Retorna o nome sem considerar quantos argumentos a Funcao tem
        /// </summary>
        public string NomeSemArgumentos
        {
            get
            {
                return _nome;
            }
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
