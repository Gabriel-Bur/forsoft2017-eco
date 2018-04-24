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
        [DataType(DataType.Text)]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "Digite seu CNPJ")]
        public string CNPJ { get; set; }


        [Required(ErrorMessage = "Digite o CEP")]
        public string CEP { get; set; }


        [Required(ErrorMessage = "Digita o nome de sua cidade")]
        [DataType(DataType.Text)]
        public string Cidade { get; set; }


        [Required(ErrorMessage = "Digite seu Bairro")]
        [DataType(DataType.Text)]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Digite o nome da Rua")]
        [DataType(DataType.Text)]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Telefone para contato")]
        public string Numero { get; set; }


        [Required(ErrorMessage = "Área de Atuação da instituição")]
        [DataType(DataType.Text)]
        public string AreaDeAtuacao { get; set; }

        [DataType(DataType.Text)]
        public string Complemento { get; set; }
    }
}