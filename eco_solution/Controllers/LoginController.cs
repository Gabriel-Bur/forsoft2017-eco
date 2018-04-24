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
    public class LoginController : Controller
    {
        private Conexao c;


        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login/Details/5
        [HttpGet]
        public ActionResult Perfil(int id)
        {
            ModelViewPessoa pessoa = new ModelViewPessoa();


            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand(String.Format("SELECT * FROM pessoa where IDPessoa = {0}",id), c.con);
            c.rd = c.query.ExecuteReader();

            while (c.rd.Read())
            {


                pessoa.IDPessoa = Convert.ToInt32(c.rd["IDPessoa"].ToString());
                pessoa.Telefone = c.rd["Telefone"].ToString();
                pessoa.Nome = c.rd["Nome"].ToString();
                pessoa.Imagem = c.rd["Imagem"].ToString();
                pessoa.Descricao = c.rd["Descricao"].ToString();

            }


            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand(String.Format("SELECT * FROM projeto where IDPessoa = {0}", id), c.con);
            c.rd = c.query.ExecuteReader();

            while (c.rd.Read())
            {

                ModelViewProjeto projeto = new ModelViewProjeto();

                projeto.IDProjeto = Convert.ToInt32(c.rd["IDProjeto"].ToString());
                projeto.Nome = c.rd["Nome"].ToString();

                pessoa.Projetos.Add(projeto);
            }

            return View(pessoa);
        }



        // POST: Login
        [HttpPost]
        public ActionResult Index(ModelViewPessoa pessoa)
        {
            if (ModelState.IsValid)
            {

                c = new Conexao();

                string email = Convert.ToString(pessoa.Email);
                string senha = Convert.ToString(pessoa.Senha);


                c.con.Open();
                c.query = new MySqlCommand("SELECT * FROM pessoa", c.con);
                c.rd = c.query.ExecuteReader();

                while (c.rd.Read())
                {
                    string e = c.rd["Email"].ToString();
                    string s = c.rd["Senha"].ToString();
                    if (e == email)
                    {
                        if (s == senha)
                        {
                            HttpContext.Session["auth"] = true;
                            HttpContext.Session["id"] = c.rd["IDPessoa"];
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                    }
                }
                c.con.Close();
            }
            return View();
        }


        public ActionResult Sair()
        {
            HttpContext.Session["auth"] = null;

            return RedirectToAction("Index", "Home");
        }


        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(ModelViewPessoa pessoa)
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

        // GET: Login/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ModelViewPessoa pessoa = new ModelViewPessoa();

            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand(String.Format("SELECT * FROM pessoa where IDPessoa = {0}", id), c.con);
            c.rd = c.query.ExecuteReader();

            while (c.rd.Read())
            {


                pessoa.IDPessoa = Convert.ToInt32(c.rd["IDPessoa"].ToString());
                pessoa.Telefone = c.rd["Telefone"].ToString();
                pessoa.Nome = c.rd["Nome"].ToString();
                pessoa.Imagem = c.rd["Imagem"].ToString();
                pessoa.Descricao = c.rd["Descricao"].ToString();

            }


            return View(pessoa);
        }

        // POST: Login/Edit/5
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

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
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
