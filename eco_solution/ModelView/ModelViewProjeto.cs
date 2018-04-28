using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eco_solution.ModelView
{
    public class ModelViewProjeto
    {


        public ModelViewProjeto() {
            this.Avaliacoes = new List<ModelViewAvaliacao>();
            this.Pessoa = new ModelViewPessoa();
        }


        /// <summary>
        /// Projeto
        /// </summary>
        /// 
    
        public int IDProjeto { get; set; }

        public ModelViewPessoa Pessoa { get; set; }

        [Required(ErrorMessage = "Digite o nome do projeto")]
        [StringLength(60,MinimumLength= 5,ErrorMessage = "O nome do projeto deve conter no minimo 5 e no maximo 60 caracteres")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }


        [Required(ErrorMessage = "Descreve com sua palavras o seu projeto")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "A descrição do projeto deve conter no minimo 5 e no maximo 200 caracteres")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }



        [Required(ErrorMessage = "Escolha uma imagem para seu projeto")]
        public string Imagem { get; set; }


        /// <summary>
        /// Pessoa
        /// </summary>




        //Lista de avaliações de um projeto
        public List<ModelViewAvaliacao> Avaliacoes { get; set; }



    }
}