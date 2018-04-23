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
            c.query = new MySqlCommand("SELECT * FROM ecossistema.pessoa p " +
                                        "inner join ecossistema.pessoafisica pf " +
                                        "on p.IDPessoa = pf.IDPessoaFisica ",c.con);
            c.rd = c.query.ExecuteReader();

            while (c.rd.Read())
            {
                ModelViewPessoaFisica person = new ModelViewPessoaFisica();
                person.IDPessoaFisica = Convert.ToInt32(c.rd["IDPessoaFisica"].ToString());
                person.Nome = c.rd["Nome"].ToString();
                person.Descricao = c.rd["Descricao"].ToString();
                person.Imagem = c.rd["Imagem"].ToString();

                lista.Add(person);
            }
            c.con.Close();


            return View(lista);
        }

        // GET: PessoaFisica/Details/5
        public ActionResult Details(int id)
        {
            ModelViewPessoa person = new ModelViewPessoa();



            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand(String.Format("SELECT * FROM usuario where IDPessoa = {0}", id), c.con);
            c.rd = c.query.ExecuteReader();

            while (c.rd.Read())
            {

                person.Email = c.rd["Email"].ToString();

            }
            c.con.Close();


            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand(String.Format("SELECT * FROM pessoa where IDPessoa = {0}", id), c.con);
            c.rd = c.query.ExecuteReader();

            while (c.rd.Read())
            {

                person.Nome = c.rd["Nome"].ToString();
                person.Telefone = c.rd["Telefone"].ToString();
                person.Descricao = c.rd["Descricao"].ToString();
                person.Imagem = c.rd["Imagem"].ToString();

            }
            c.con.Close();


            return View(person);
        }

        // GET: PessoaFisica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PessoaFisica/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
