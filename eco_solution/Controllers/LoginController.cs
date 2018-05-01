using eco_solution.DAO;
using eco_solution.ModelView;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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


                //////////conexão com o banco
                c.con.Open();
                c.query = new MySqlCommand("SELECT * FROM Pessoa", c.con);
                c.rd = c.query.ExecuteReader();
                while (c.rd.Read())
                {
                    string e = c.rd["Email"].ToString();
                    string s = c.rd["Senha"].ToString();

                    ///// Compara o login se esta correto
                    if (e == email & s == senha)

                    {
                        //login ok
                        HttpContext.Session["auth"] = true;
                        HttpContext.Session["id"] = c.rd["IDPessoa"];
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                    }

                }

                c.con.Close();
            }

            //caso o login esteja incorreto 
            ModelState.AddModelError("", "Acesso negado");


            return View();
        }

        [HttpGet]
        public ActionResult Recuperar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Recuperar(ModelViewRecuperarLogin user)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    /// fazer select para comparar os emails e saber se existe
                    c = new Conexao();
                    c.con.Open();
                    c.query = c.con.CreateCommand();
                    c.query.CommandText = "Select * from Pessoa where Email = @email";
                    c.query.Parameters.AddWithValue("@email", user.Email.ToString());
                    c.rd = c.query.ExecuteReader();

                    while (c.rd.Read())
                    {
                        string email = c.rd["Email"].ToString();
                        string senha = c.rd["Senha"].ToString();

                        //verifica se o email que o usuário digitou existe no banco
                        if (email == user.Email.ToString())
                        {
                            //envia email de recuperação
                            enviaSMTP(user.Email.ToString(), senha.ToString());
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("Email", "Email não cadastrado.");
                            return View();
                        }


                    }
                }
                catch
                {
                    ModelState.AddModelError("Email", "Aconteceu um erro, tente novamente mais tarde.");
                    return View();
                }


            }

            ModelState.AddModelError("Email", "Digite um Email válido.");
            return View();
        }



        ///envia email 
        protected Boolean enviaSMTP(string email, string senha)
        {


            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");


                //smtp
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("gabriel.sampaio@al.forsoft.org.br", "ab12cd1995");





                //email
                mail.From = new MailAddress("gabriel.sampaio@al.forsoft.org.br", "Ecossistema de Inovação");
                mail.To.Add(email);
                mail.Subject = "Recuperação de Senha";
                mail.Body = "Sua senha é: " + senha.ToString();

                smtp.Send(mail);

                return true;

            }
            catch
            {
                return false;
            }
        }


        ///logout
        public ActionResult Sair()
        {
            HttpContext.Session["auth"] = null;

            return RedirectToAction("Index", "Home");
        }


    }
}
