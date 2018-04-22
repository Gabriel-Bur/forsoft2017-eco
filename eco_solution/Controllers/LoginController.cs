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
        public ActionResult Details(int id)
        {
            return View();
        }



        // POST: Login/Create
        public ActionResult Logar(FormCollection usuario)
        {
            c = new Conexao();

            string email = Convert.ToString(usuario["Email"]);
            string senha = Convert.ToString(usuario["Senha"]);

            //MySqlParameter param = new MySqlParameter();
            //param.ParameterName = "@Email";
            //param.Value = email;



            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand("", c.con);
            c.query.CommandText = String.Format("SELECT * FROM usuario where Email={0}", email);
            c.rd = c.query.ExecuteReader();


            while (c.rd.Read())
            {
                ModelViewUsuario user = new ModelViewUsuario();
                user.Email = c.rd["Email"].ToString();
                user.Senha = c.rd["Senha"].ToString();

            }

            c.con.Clone();

            return View();

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
