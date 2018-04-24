using eco_solution.DAO;
using eco_solution.ModelView;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eco_solution.Controllers
{
    public class PessoaFisicaController : Controller
    {
        private Conexao c;

        // GET: PessoaFisica
        public ActionResult Index()
        {
            List<ModelViewPessoaFisica> lista = new List<ModelViewPessoaFisica>();


            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand("SELECT * FROM pessoa inner join pessoafisica on IDPessoa = IDPessoaFisica", c.con);
            c.rd = c.query.ExecuteReader();

            while (c.rd.Read())
            {
                ModelViewPessoaFisica pf = new ModelViewPessoaFisica();
                ModelViewPessoa person = new ModelViewPessoa();


                pf.IDPessoaFisica = Convert.ToInt32(c.rd["IDPessoaFisica"].ToString());
                pf.CPF = c.rd["CPF"].ToString();
                pf.RG = c.rd["RG"].ToString();

                person.Nome = c.rd["Nome"].ToString();
                person.Imagem = c.rd["Imagem"].ToString();
                person.Descricao = c.rd["Descricao"].ToString();
                person.Telefone = c.rd["Telefone"].ToString();

                pf.Pessoa = person;

                lista.Add(pf);
            }
            c.con.Close();


            return View(lista);
        }

        // GET: PessoaFisica/Details/5
        public ActionResult Details(int id)
        {
            ModelViewPessoaFisica pf = new ModelViewPessoaFisica();
            ModelViewPessoa person = new ModelViewPessoa();

            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand(String.Format("SELECT * FROM pessoa inner join pessoafisica on IDPessoa = IDPessoaFisica where IDPessoa = {0}", id), c.con);
            c.rd = c.query.ExecuteReader();

            while (c.rd.Read())
            {
                person.Email = c.rd["Email"].ToString();
                person.Nome = c.rd["Nome"].ToString();
                person.Telefone = c.rd["Telefone"].ToString();
                person.Descricao = c.rd["Descricao"].ToString();
                person.Imagem = c.rd["Imagem"].ToString();

                pf.RG = c.rd["RG"].ToString();
                pf.CPF = c.rd["CPF"].ToString();


                pf.Pessoa = person;



            }
            c.con.Close();

            return View(pf);
        }

        // GET: PessoaFisica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PessoaFisica/Create
        [HttpPost]
        public ActionResult Create(ModelViewPessoaFisica pf)
        {
            if (ModelState.IsValid)
            {

            }

            return View();
        }

        // GET: PessoaFisica/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PessoaFisica/Edit/5
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

        // GET: PessoaFisica/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PessoaFisica/Delete/5
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
