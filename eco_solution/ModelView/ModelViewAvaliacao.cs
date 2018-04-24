﻿using System;
using System.Collections.Generic;
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

        public int Nota { get; set; }

        public string Comentario { get; set; }

        public ModelViewPessoa Pessoa { get; set; }


    }
}