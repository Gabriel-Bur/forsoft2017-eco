﻿using eco_solution.DAO;
using eco_solution.ModelView;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eco_solution.Controllers
{
    public class PessoaJuridicaController : Controller
    {
        private Conexao c;


        // GET: PessoaJuridica
        public ActionResult Index()
        {

            //list de PessoaJuridica
            List<ModelViewPessoaJuridica> lista = new List<ModelViewPessoaJuridica>();


            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand("SELECT * FROM pessoa inner join pessoajuridica on IDPessoa = IDPessoaJuridica", c.con);
            c.rd = c.query.ExecuteReader();

            while (c.rd.Read())
            {
                ModelViewPessoaJuridica pj = new ModelViewPessoaJuridica();
                ModelViewPessoa person = new ModelViewPessoa();


                pj.IDPessoaJuridica = Convert.ToInt32(c.rd["IDPessoaJuridica"].ToString());
                pj.RazaoSocial = c.rd["RazaoSocial"].ToString();
                pj.AreaDeAtuacao = c.rd["AreaDeAtuacao"].ToString();

                person.Nome = c.rd["Nome"].ToString();
                person.Imagem = c.rd["Imagem"].ToString();
                person.Descricao = c.rd["Descricao"].ToString();
                person.Telefone = c.rd["Telefone"].ToString();



                //faz a uniao das informaçoes de pessoaJuridica e Pessoa pra view
                pj.Pessoa = person;

                lista.Add(pj);
            }
            c.con.Close();

            return View(lista);
        }

        // GET: PessoaJuridica/Details/5
        public ActionResult Details(int id)
        {
            ModelViewPessoaJuridica pj = new ModelViewPessoaJuridica();
            ModelViewPessoa person = new ModelViewPessoa();

            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand(String.Format("SELECT * FROM pessoa inner join pessoajuridica on IDPessoa = IDPessoaJuridica where IDPessoa = {0}", id), c.con);
            c.rd = c.query.ExecuteReader();

            while (c.rd.Read())
            {
                person.Email = c.rd["Email"].ToString();
                person.Telefone = c.rd["Telefone"].ToString();
                person.Nome = c.rd["Nome"].ToString();
                person.Descricao = c.rd["Descricao"].ToString();
                person.Imagem = c.rd["Imagem"].ToString();

                pj.RazaoSocial = c.rd["RazaoSocial"].ToString();
                pj.CNPJ = c.rd["CNPJ"].ToString();
                pj.AreaDeAtuacao = c.rd["AreaDeAtuacao"].ToString();
                pj.Logradouro = c.rd["Logradouro"].ToString();
                pj.Bairro = c.rd["Bairro"].ToString();
                pj.Numero = c.rd["Numero"].ToString();
                pj.Complemento = c.rd["Complemento"].ToString();

                pj.Pessoa = person;



            }
            c.con.Close();

            return View(pj);
        }
    

        // GET: PessoaJuridica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PessoaJuridica/Create
        [HttpPost]
        public ActionResult Create(ModelViewPessoaJuridica js)
        {
            if (ModelState.IsValid)
            {

            }

            return View();
        }

        // GET: PessoaJuridica/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PessoaJuridica/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PessoaJuridica/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PessoaJuridica/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
