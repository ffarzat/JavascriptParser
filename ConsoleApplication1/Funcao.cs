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
        /// Numero da instancia dessa funcao
        /// </summary>
        private int _instancia;

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
        /// Determina se a função é para ser descrita como principal na gramática
        /// </summary>
        public bool Principal { get; set; }

        /// <summary>
        /// Nome da Função
        /// </summary>
        public string Nome
        {
            get { return DeterminarNome(); }
            set { _nome = value; }
        }

        /// <summary>
        /// Número da instancia
        /// </summary>
        public int Instancia
        {
            get { return _instancia; }
            set { _instancia = value; }
        }

        /// <summary>
        /// Determina o nome da função
        /// </summary>
        /// <returns></returns>
        private string DeterminarNome()
        {
            string nomeDaFuncao = TraduzirNome(_nome);

            //return nomeDaFuncao;

            return nomeDaFuncao + "_" + Instancia;
        }

        /// <summary>
        /// Retorna o nome sem considerar quantos argumentos a Funcao tem
        /// </summary>
        public string NomeSemArgumentos
        {
            get
            {
                return Funcao.TraduzirNome(_nome);
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


        /// <summary>
        /// Traduz os nomes de função e argumentos
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        public static string TraduzirNome(string nome)
        {
            string nomeDaFuncao = nome;


            switch (nome)
            {
                case "if":
                    nomeDaFuncao = "se";
                    break;
                case "return":
                    nomeDaFuncao = "retornar";
                    break;
                case "=":
                    nomeDaFuncao = "set";
                    break;
                case "+":
                    nomeDaFuncao = "add";
                    break;
                case "<=":
                    nomeDaFuncao = "lessOrEqualThan";
                    break;
                case "%":
                    nomeDaFuncao = "module";
                    break;
                case "/":
                    nomeDaFuncao = "divide";
                    break;
                case "\"\"":
                    nomeDaFuncao = "empty";
                    break;
                case "12":
                    nomeDaFuncao = "twelve";
                    break;
                case "\"/\"":
                    nomeDaFuncao = "string_bar";
                    break;
                case "":
                    nomeDaFuncao = "empty";
                    break;

            }
            
            return nomeDaFuncao;
        }

    }
}
