using eco_solution.DAO;
using eco_solution.ModelView;
using eco_solution.Prevent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eco_solution.Controllers
{
    public class AvaliacaoController : Controller
    {

        Conexao c;


        // GET: Avaliacao
        [HttpGet]
        [NoDirect]
        public ActionResult Index(int id)
        {
            try
            {
                if (Session["id"] == null)
                {
                    return RedirectToAction("Index", "Login");

                }


                Session["projeto"] = id;

                return View();
            }
            catch
            {
                return RedirectToAction("Index", "Login");
            }
        }



        [HttpPost]
        [NoDirect]
        public ActionResult Index(ModelViewAvaliacao a)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    a.IDProjeto = Convert.ToInt32(Session["projeto"]);
                    a.Pessoa.IDPessoa = Convert.ToInt32(Session["id"]);



                    c = new Conexao();
                    c.con.Open();
                    c.query = c.con.CreateCommand();
                    c.query.CommandText = "INSERT INTO Avaliacao (IDPessoa,IDProjeto,Nota,Comentario) VALUES (@idpessoa,@idprojeto,@nota,@comentario)";
                    c.query.Parameters.AddWithValue("@idpessoa", a.Pessoa.IDPessoa);
                    c.query.Parameters.AddWithValue("@idprojeto", a.IDProjeto);
                    c.query.Parameters.AddWithValue("@nota", a.Nota);
                    c.query.Parameters.AddWithValue("@comentario", a.Comentario);
                    c.query.ExecuteNonQuery();
                    c.con.Close();



                    Session["Projeto"] = null;

                    return RedirectToAction("Details", "Projeto", new { id = a.IDProjeto });


                }
                catch (Exception e)
                {
                    return View();
                }

            }




            return View();
        }
    }
}