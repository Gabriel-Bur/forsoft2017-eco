using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eco_solution.ModelView
{
    public class ModelViewPessoaJuridica
    {
        public int IDPessoaJuridica { get; set; }

        public ModelViewPessoa Pessoa { get; set; }

        [Required(ErrorMessage = "Digite a Razão Social de sua instituição")]
        [StringLength(60,MinimumLength=3,ErrorMessage ="Razao social deve possuir pelo menos 3 e no maximo 60 caracteres")]
        [DataType(DataType.Text)]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "Digite seu CNPJ")]
        [RegularExpression(@"(\d{2}\.?\d{3}\.?\d{3}\/?\d{4}-?\d{2})", ErrorMessage ="CNPJ inválido")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "CNPJ deve conter 14 digitos")]
        public string CNPJ { get; set; }


        [Required(ErrorMessage = "Digite o CEP")]
        [RegularExpression(@"[0-9]{5}-?[0-9]{3}", ErrorMessage = "CEP inválido")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "CEP inválido")]
        public string CEP { get; set; }


        [Required(ErrorMessage = "Digita o nome de sua cidade")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Cidade deve possuir pelo menos 2 e no maximo 25 caracteres")]
        [DataType(DataType.Text)]
        public string Cidade { get; set; }


        [Required(ErrorMessage = "Digite seu Bairro")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Cidade deve possuir pelo menos 2 e no maximo 50 caracteres")]
        [DataType(DataType.Text)]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Digite o nome da Rua")]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "Logradouro deve possuir pelo menos 5 e no maximo 40 caracteres")]
        [DataType(DataType.Text)]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Digite o numero")]
        [Range(8,int.MaxValue,ErrorMessage = "Digite um numero válido")]
        [StringLength(8, MinimumLength = 1, ErrorMessage = "Digite um numero válido")]
        public string Numero { get; set; }


        [Required(ErrorMessage = "Área de Atuação da instituição")]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "Digite em poucas palavras sua área de atuação")]
        [DataType(DataType.Text)]
        public string AreaDeAtuacao { get; set; }

        [DataType(DataType.Text)]
        public string Complemento { get; set; }
    }
}