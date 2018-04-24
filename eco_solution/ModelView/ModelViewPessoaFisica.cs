using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eco_solution.ModelView
{
    public class ModelViewPessoaFisica
    {
        public int IDPessoaFisica { get; set; }

        public ModelViewPessoa Pessoa { get; set; }

        [Required(ErrorMessage = "Digite seu RG")]
        public string RG { get; set; }

        [Required(ErrorMessage = "Digite seu CPF")]
        public string CPF { get; set; }


    }
}