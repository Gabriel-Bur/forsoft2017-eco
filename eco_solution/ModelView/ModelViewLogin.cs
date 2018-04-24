using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eco_solution.ModelView
{
    public class ModelViewLogin
    {
        [Required(ErrorMessage = "Digite seu Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite sua Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}