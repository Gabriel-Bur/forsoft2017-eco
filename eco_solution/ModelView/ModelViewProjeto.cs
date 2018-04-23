using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eco_solution.ModelView
{
    public class ModelViewProjeto
    {


        public ModelViewProjeto() {
            this.Avaliacoes = new List<ModelViewAvaliacao>();
        }


        /// <summary>
        /// Projeto
        /// </summary>
        public int IDProjeto { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string Imagem { get; set; }

        /// <summary>
        /// Pessoa
        /// </summary>
        public int IDPessoa { get; set; }

        public string PessoaTelefone{ get; set; }

        public string PessoaNome { get; set; }

        public string PessoaImagem { get; set; }


        //Lista de avaliações de um projeto
        public List<ModelViewAvaliacao> Avaliacoes { get; set; }



    }
}