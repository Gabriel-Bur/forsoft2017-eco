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
        [StringLength(9, MinimumLength = 9, ErrorMessage = "RG deve estar no formato: XXXXXXXXX")]
        [RegularExpression(@"(^\d{2}\.?\d{3}\.?\d{3}-?\d{1}$)", ErrorMessage = "RG deve estar no formato: XXXXXXXXX")]

        public string RG { get; set; }

        [Required(ErrorMessage = "Digite seu CPF")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve estar no formato: XXXXXXXXXXX")]
        [RegularExpression(@"(^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$)",ErrorMessage = "CPF deve estar no formato: XXXXXXXXXXX")]
        public string CPF { get; set; }


    }
}