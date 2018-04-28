using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eco_solution.ModelView
{
    public class ModelViewAvaliacao
    {
        /// <summary>
        /// Avaliação
        /// </summary>
        /// 


        public ModelViewAvaliacao()
        {
            this.Pessoa = new ModelViewPessoa();
        }


        public int IDAvaliacao { get; set; }


        public int IDProjeto { get; set; }


        [Required(ErrorMessage = "Escolha uma nota de 1 à 10")]
        [Range(1,10,ErrorMessage = "Atribua uma nota de 1 à 10")]
        public int Nota { get; set; }

        [Required(ErrorMessage = "Digite oque você achou do projeto")]
        [StringLength(100,MinimumLength = 10,ErrorMessage = "No minimo 10 e no maximo 100 caracteres")]
        [DataType(DataType.MultilineText)]
        public string Comentario { get; set; }

        public ModelViewPessoa Pessoa { get; set; }


    }
}