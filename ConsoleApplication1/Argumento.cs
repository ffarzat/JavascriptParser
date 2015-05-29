using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    /// <summary>
    /// Representa um argumento de função do JS
    /// </summary>
    public class Argumento
    {
        /// <summary>
        /// Nome do Argumento
        /// </summary>
        public string Nome {
            get { return DeterminarNome(); }
            set { _nome = value; }
        }

        private string _nome;

        /// <summary>
        /// Determina o nome da função
        /// </summary>
        /// <returns></returns>
        private string DeterminarNome()
        {
            string nomeDaFuncao = _nome;


            switch (_nome)
            {
                case "\"\"":
                    nomeDaFuncao = "empty";
                    break;
                case "12":
                    nomeDaFuncao = "twelve";
                    break;
                case "\"/\"":
                    nomeDaFuncao = "string_bar";
                    break;
            }


            return nomeDaFuncao;
        }
    }
}
