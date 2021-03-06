﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eco_solution.ModelView
{
    public class ModelViewPessoa
    {

        public ModelViewPessoa()
        {

            this.Projetos = new List<ModelViewProjeto>();
        }


        public int IDPessoa { get; set; }


        [Required(ErrorMessage = "Digite seu Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite sua Senha")]
        [StringLength(20,MinimumLength = 6,ErrorMessage = "A senha deve possuir no minimo 6 e no maximo 20 caracteres" )]
        [DataType(DataType.Password)]     
        public string Senha { get; set; }


        [Required(ErrorMessage = "Confirme a Senha")]
        [DataType(DataType.Password)]
        [Display(Name ="Confirmar Senha")]
        [Compare("Senha")]
        public string ConfirmarSenha { get; set; }


        [Required(ErrorMessage = "Digite seu Nome")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite seu telefone para contato")]
        [StringLength(11,MinimumLength = 11, ErrorMessage = "Digite o numero com DDD, ex: 21990012345" )]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Descreve um pouco suas ideias e propostas")]
        [DataType(DataType.MultilineText)]
        [Display(Name ="Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Escolha uma imagem para seu perfil")]
        public string Imagem { get; set; }

        public List<ModelViewProjeto> Projetos { get; set; }

    }
}