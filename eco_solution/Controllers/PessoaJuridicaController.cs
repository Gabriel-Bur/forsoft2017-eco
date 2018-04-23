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
    public class PessoaJuridicaController : Controller
    {
        private Conexao c;


        // GET: PessoaJuridica
        public ActionResult Index()
        {
            List<ModelViewPessoaJurifica> lista = new List<ModelViewPessoaJurifica>();


            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand("SELECT * FROM ecossistema.pessoa p " +
                                        "inner join ecossistema.pessoajuridica pj " +
                                        "on p.IDPessoa = pj.IDPessoaJuridica", c.con);
            c.rd = c.query.ExecuteReader();

            while (c.rd.Read())
            {
                ModelViewPessoaJurifica person = new ModelViewPessoaJurifica();
                person.IDPessoaJuridica = Convert.ToInt32(c.rd["IDPessoaJuridica"].ToString());
                person.NomeFantasia = c.rd["NomeFantasia"].ToString();
                person.Nome = c.rd["Nome"].ToString();
                person.Descricao = c.rd["Descricao"].ToString();
                person.Imagem = c.rd["Imagem"].ToString();

                lista.Add(person);
            }
            c.con.Close();

            return View(lista);
        }

        // GET: PessoaJuridica/Details/5
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
    

        // GET: PessoaJuridica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PessoaJuridica/Create
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
