using eco_solution.DAO;
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
    public class PerfilController : Controller
    {

        Conexao c;



        // GET: Perfil
        public ActionResult Index()
        {
            if (Session["id"].Equals(null))
            {
                return View("Index", "Login");
            }


            var id = Session["id"];

            //Pegar perfil do usuario

            ModelViewPessoa pessoa = new ModelViewPessoa();


            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand(String.Format("SELECT * FROM Pessoa where IDPessoa = {0}", id), c.con);
            c.rd = c.query.ExecuteReader();

            while (c.rd.Read())
            {

                pessoa.IDPessoa = Convert.ToInt32(c.rd["IDPessoa"].ToString());
                pessoa.Telefone = c.rd["Telefone"].ToString();
                pessoa.Nome = c.rd["Nome"].ToString();
                pessoa.Imagem = c.rd["Imagem"].ToString();
                pessoa.Descricao = c.rd["Descricao"].ToString();

            }
            c.con.Close();

            ///////////////


            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand(String.Format("SELECT * FROM Projeto where IDPessoa = {0}", id), c.con);
            c.rd = c.query.ExecuteReader();

            while (c.rd.Read())
            {

                ModelViewProjeto projeto = new ModelViewProjeto();

                projeto.IDProjeto = Convert.ToInt32(c.rd["IDProjeto"].ToString());
                projeto.Nome = c.rd["Nome"].ToString();

                pessoa.Projetos.Add(projeto);
            }

            c.con.Close();

            return View(pessoa);
        }






        // GET: Perfil/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }






        // GET: Perfil/Create
        public ActionResult Create()
        {
            return View();
        }







        // POST: Perfil/Create
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











        // Post: Perfil/Edit/5
        [HttpPost]
        public ActionResult Edit(ModelViewPessoa person)
        {
            if (ModelState.IsValid)
            {

                //recupera o id do usuario
                var idlogado = Session["id"];

                //recupera o objeto arquivo
                HttpPostedFileBase foto = Request.Files["Imagem"];

                // pega o nome do arquivo
                var nomeArquivo = Path.GetFileName(foto.FileName);
                //cria o caminho final da imagem
                var caminho = Path.Combine(Server.MapPath(Url.Content("~/assets/perfil/")), nomeArquivo);
                //salva a foto no caminho
                foto.SaveAs(caminho);
                //imagem da pessoafisica criado recebe o caminho da imagem salva
                person.Imagem = Path.Combine(Url.Content("/assets/perfil/"), nomeArquivo);


                c = new Conexao();
                c.con.Open();
                c.query = c.con.CreateCommand();
                c.query.CommandText = "Update Pessoa set " +
                "IDPessoa=@id," +
                "Email=@email," +
                "Senha=@senha," +
                "Telefone=@telefone," +
                "Nome=@nome," +
                "Descricao=@descricao," +
                "Imagem=@imagem" +
                " where IDPessoa = @id";
                c.query.Parameters.AddWithValue("@id", Convert.ToInt32(idlogado));
                c.query.Parameters.AddWithValue("@email", person.Email);
                c.query.Parameters.AddWithValue("@senha", person.Senha);
                c.query.Parameters.AddWithValue("@nome", person.Nome);
                c.query.Parameters.AddWithValue("@telefone", person.Telefone);
                c.query.Parameters.AddWithValue("@descricao", person.Descricao);
                c.query.Parameters.AddWithValue("@imagem", person.Imagem);
                c.query.ExecuteNonQuery();
                c.con.Close();

                return RedirectToAction("Index","Perfil");

            }

            return View();
        }







        // POST: Perfil/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {

            if (Session["id"].Equals(null))
            {
                return RedirectToAction("Index", "Login");
            }


            //Pegar perfil do usuario

            ModelViewPessoa pessoa = new ModelViewPessoa();


            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand(String.Format("SELECT * FROM Pessoa where IDPessoa = {0}", id), c.con);
            c.rd = c.query.ExecuteReader();

            while (c.rd.Read())
            {
                pessoa.IDPessoa = Convert.ToInt32(c.rd["IDPessoa"].ToString());
                pessoa.Telefone = c.rd["Telefone"].ToString();
                pessoa.Email = c.rd["Email"].ToString();
                pessoa.Nome = c.rd["Nome"].ToString();
                pessoa.Imagem = c.rd["Imagem"].ToString();
                pessoa.Descricao = c.rd["Descricao"].ToString();

            }
            c.con.Close();

            return View(pessoa);
        }




        // GET: Perfil/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }


    }
}
