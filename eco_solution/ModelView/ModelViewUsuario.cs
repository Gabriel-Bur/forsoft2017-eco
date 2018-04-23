using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eco_solution.ModelView
{
    public class ModelViewUsuario
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Email Invalido ou Inexistente")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Senha incorreta")]
        public string Senha { get; set; }
    }
}