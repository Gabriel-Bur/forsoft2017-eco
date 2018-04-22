using eco_solution.DAO;
using eco_solution.ModelView;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eco_solution.Controllers
{
    public class ProjetoController : Controller
    {
        private Conexao c;

        // GET: Projeto
        public ActionResult Index()
        {

            List<ModelViewProjeto> lista = new List<ModelViewProjeto>();

            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand("SELECT * FROM projeto",c.con);
            c.rd = c.query.ExecuteReader();

            while (c.rd.Read())
            {
                ModelViewProjeto project = new ModelViewProjeto();
                project.IDProjeto = Convert.ToInt32(c.rd["IDProjeto"].ToString());
                project.Nome = c.rd["Nome"].ToString();
                project.Descricao = c.rd["Descricao"].ToString();
                project.Imagem = c.rd["Imagem"].ToString();

                lista.Add(project);
            }

            c.con.Close();

            return View(lista);
        }

        // GET: Projeto/Details/5
        public ActionResult Details(int id)
        {
            ModelViewProjeto project = new ModelViewProjeto();
            //Recupera o projeto
            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand(String.Format("SELECT * FROM projeto where IDProjeto = {0}",id), c.con);
            c.rd = c.query.ExecuteReader();
            while (c.rd.Read())
            {
                project.IDProjeto = Convert.ToInt32(c.rd["IDProjeto"].ToString());
                project.IDPessoa = Convert.ToInt32(c.rd["IDPessoa"].ToString());
                project.Nome = c.rd["Nome"].ToString();
                project.Descricao = c.rd["Descricao"].ToString();
                project.Imagem = c.rd["Imagem"].ToString();
            }
            c.con.Close();

            //recupera a pessoa ligada ao projeto
            c.con.Open();
            c.query = new MySqlCommand(String.Format("SELECT * FROM pessoa where IDPessoa = {0}", project.IDPessoa), c.con);
            c.rd = c.query.ExecuteReader();
            while (c.rd.Read())
            {
                project.PessoaNome = c.rd["Nome"].ToString();
                project.PessoaTelefone = c.rd["Telefone"].ToString();
                project.PessoaImagem = c.rd["Imagem"].ToString();
            }
            c.con.Close();




            return View(project);
        }

        // GET: Projeto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projeto/Create
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

        // GET: Projeto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Projeto/Edit/5
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

        // GET: Projeto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Projeto/Delete/5
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
