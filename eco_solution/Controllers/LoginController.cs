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
        public ActionResult Index()
        {


            return View();
        }

        // GET: Login/Details/5
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


            return View(pessoa);
        }



        // POST: Login/Create
        public ActionResult Logar(ModelViewUsuario usuario)
        {
            if (ModelState.IsValid)
            {

                c = new Conexao();

                string email = Convert.ToString(usuario.Email);
                string senha = Convert.ToString(usuario.Senha);


                c.con.Open();
                c.query = new MySqlCommand("SELECT * FROM usuario", c.con);
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
                            HttpContext.Session["id"] = c.rd["IDUsuario"];
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
            return View("Index");

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
        public ActionResult Create(ModelViewUsuario usuario)
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
        public ActionResult Edit(int id)
        {
            return View();
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
