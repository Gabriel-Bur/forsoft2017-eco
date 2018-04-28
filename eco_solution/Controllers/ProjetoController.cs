using eco_solution.DAO;
using eco_solution.ModelView;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
            c.query = new MySqlCommand("SELECT * FROM Projeto", c.con);
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
            c = new Conexao();

            //Recupera o projeto
            c.con.Open();
            c.query = new MySqlCommand(String.Format("SELECT * FROM Projeto where IDProjeto = {0}", id), c.con);
            c.rd = c.query.ExecuteReader();
            while (c.rd.Read())
            {
                project.IDProjeto = Convert.ToInt32(c.rd["IDProjeto"].ToString());
                project.Pessoa.IDPessoa = Convert.ToInt32(c.rd["IDPessoa"].ToString());
                project.Nome = c.rd["Nome"].ToString();
                project.Descricao = c.rd["Descricao"].ToString();
                project.Imagem = c.rd["Imagem"].ToString();
            }
            c.con.Close();


            //recupera a pessoa criadora do projeto
            c.con.Open();
            c.query = new MySqlCommand(String.Format("SELECT * FROM Pessoa where IDPessoa = {0}", project.Pessoa.IDPessoa), c.con);
            c.rd = c.query.ExecuteReader();
            while (c.rd.Read())
            {
                project.Pessoa.Nome = c.rd["Nome"].ToString();
                project.Pessoa.Telefone = c.rd["Telefone"].ToString();
                project.Pessoa.Imagem = c.rd["Imagem"].ToString();

            }
            c.con.Close();



            //recupera a pessoa e sua avaliação ligada ao projeto
            c.con.Open();
            c.query = new MySqlCommand(String.Format("select * from Avaliacao inner join Pessoa on Avaliacao.IDPessoa = Pessoa.IDPessoa where IDProjeto = {0}", id, project.IDProjeto), c.con);


            c.rd = c.query.ExecuteReader();
            while (c.rd.Read())
            {

                //Recupera a lista de avaliações do projeto escolhido
                ModelViewAvaliacao avaliacao = new ModelViewAvaliacao();

                avaliacao.IDAvaliacao = Convert.ToInt32(c.rd["IDAvaliacao"].ToString());
                avaliacao.IDProjeto = Convert.ToInt32(c.rd["IDProjeto"].ToString());
                avaliacao.Pessoa.IDPessoa = Convert.ToInt32(c.rd["IDPessoa"].ToString());
                avaliacao.Pessoa.Nome = c.rd["Nome"].ToString();
                avaliacao.Pessoa.Imagem = c.rd["Imagem"].ToString();
                avaliacao.Nota = Convert.ToInt32(c.rd["Nota"].ToString());
                avaliacao.Comentario = c.rd["Comentario"].ToString();

                project.Avaliacoes.Add(avaliacao);


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
        public ActionResult Create(ModelViewProjeto project)
        {
            if (ModelState.IsValid)
            {
                //se nao tiver logado
                if (Session["id"]==null)
                {
                    return RedirectToAction("Index", "Login");
                }
                //se tiver logado
                else
                {
                    //recupera o id do usuario 
                    var idlogado = Session["id"];


                    //pega o objeto imagem do input
                    HttpPostedFileBase foto = Request.Files["Imagem"];

                    // pega o nome do arquivo
                    var nomeArquivo = Path.GetFileName(foto.FileName);
                    //cria o caminho final da imagem
                    var caminho = Path.Combine(Server.MapPath(Url.Content("~/assets/Projeto/")), nomeArquivo);
                    //salva a foto no caminho
                    foto.SaveAs(caminho);
                    //imagem do projeto criado recebe o caminho da imagem salva
                    project.Imagem = Path.Combine(Url.Content("/assets/Projeto/"), nomeArquivo);




                    c = new Conexao();
                    c.con.Open();
                    c.query = c.con.CreateCommand();
                    c.query.CommandText = "INSERT INTO Projeto (IDPessoa,Nome,Descricao,Imagem) VALUES (@idpessoa,@nome,@descricao,@imagem)";

                    c.query.Parameters.AddWithValue("@idpessoa", idlogado);
                    c.query.Parameters.AddWithValue("@nome", project.Nome);
                    c.query.Parameters.AddWithValue("@descricao", project.Descricao);
                    c.query.Parameters.AddWithValue("@imagem", project.Imagem);
                    c.query.ExecuteNonQuery();

                    c.con.Close();


                    return RedirectToAction("Index", "Projeto");
                }


            }


            return View();
        }





        // GET: Projeto/Edit/5
        public ActionResult Edit(int id)
        {
            if (id.Equals(null))
            {
                return View("Index", "Login");
            }


            ModelViewProjeto project = new ModelViewProjeto();

            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand(String.Format("SELECT * FROM Projeto where IDProjeto = {0}", id), c.con);
            c.rd = c.query.ExecuteReader();

            while (c.rd.Read())
            {
                project.IDProjeto = Convert.ToInt32(c.rd["IDProjeto"].ToString());
                project.Nome = c.rd["Nome"].ToString();
                project.Descricao = c.rd["Descricao"].ToString();
                project.Imagem = c.rd["Imagem"].ToString();
            }

            return View(project);
        }





        // POST: Projeto/Edit/5
        [HttpPost]
        public ActionResult Edit(ModelViewProjeto project)
        {
            if (ModelState.IsValid)
            {
                //recupera o objeto arquivo
                HttpPostedFileBase foto = Request.Files["Imagem"];

                // pega o nome do arquivo
                var nomeArquivo = Path.GetFileName(foto.FileName);
                //cria o caminho final da imagem
                var caminho = Path.Combine(Server.MapPath(Url.Content("~/assets/projeto/")), nomeArquivo);
                //salva a foto no caminho
                foto.SaveAs(caminho);
                //imagem da pessoafisica criado recebe o caminho da imagem salva
                project.Imagem = Path.Combine(Url.Content("/assets/projeto/"), nomeArquivo);


                c = new Conexao();
                c.query = c.con.CreateCommand();
                c.query.CommandText = "Update Projeto set " +
                                        "Nome=@nome , Descricao=@descricao, Imagem=@imagem " +
                                        "where IDProjeto = @id";

                c.query.Parameters.AddWithValue("@id", project.IDProjeto);
                c.query.Parameters.AddWithValue("@nome", project.Nome);
                c.query.Parameters.AddWithValue("@Descricao", project.Descricao);
                c.query.Parameters.AddWithValue("@Imagem", project.Imagem);

                return RedirectToAction("Index", "Perfil");

            }


            return View();

        }





        // DELETE:  Projeto/Delete/5
        public ActionResult Delete(int id)
        {
            if (id==null || Session["id"]==null)
            {
                return RedirectToAction("Index", "Login");
            }

            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand(String.Format("Delete from Projeto where IDProjeto = {0}", id), c.con);
            c.query.ExecuteNonQuery();

            c.con.Close();

            return RedirectToAction("Index", "Perfil");
        }

    }
}
