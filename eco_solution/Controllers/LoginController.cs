﻿using eco_solution.DAO;
using eco_solution.ModelView;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eco_solution.Controllers
{
    public class LoginController : Controller
    {
        private Conexao c;


        // GET: Login
        //pagina login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login
        //faz a comparação no login e retorna pagina principal
        [HttpPost]
        public ActionResult Index(ModelViewLogin user)
        {
            if (ModelState.IsValid)
            {

                c = new Conexao();

                string email = Convert.ToString(user.Email);
                string senha = Convert.ToString(user.Senha);


                c.con.Open();
                c.query = new MySqlCommand("SELECT * FROM Pessoa", c.con);
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

        ///sair
        public ActionResult Sair()
        {
            HttpContext.Session["auth"] = null;

            return RedirectToAction("Index", "Home");
        }


    }
}
