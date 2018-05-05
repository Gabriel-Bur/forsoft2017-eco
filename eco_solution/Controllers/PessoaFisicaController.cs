using eco_solution.DAO;
using eco_solution.ModelView;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace eco_solution.Controllers
{
    public class PessoaFisicaController : Controller
    {
        private Conexao c;

        // GET: PessoaFisica
        public ActionResult Index()
        {
            List<ModelViewPessoaFisica> lista = new List<ModelViewPessoaFisica>();


            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand("SELECT * FROM Pessoa inner join PessoaFisica on IDPessoa = IDPessoaFisica", c.con);
            c.rd = c.query.ExecuteReader();

            while (c.rd.Read())
            {
                ModelViewPessoaFisica pf = new ModelViewPessoaFisica();
                ModelViewPessoa person = new ModelViewPessoa();


                pf.IDPessoaFisica = Convert.ToInt32(c.rd["IDPessoaFisica"].ToString());
                pf.CPF = c.rd["CPF"].ToString();
                pf.RG = c.rd["RG"].ToString();

                person.Nome = c.rd["Nome"].ToString();
                person.Imagem = c.rd["Imagem"].ToString();
                person.Descricao = c.rd["Descricao"].ToString();
                person.Telefone = c.rd["Telefone"].ToString();

                pf.Pessoa = person;

                lista.Add(pf);
            }
            c.con.Close();


            return View(lista);
        }



        // GET: PessoaFisica/Details/5
        public ActionResult Details(int id)
        {
            ModelViewPessoaFisica pf = new ModelViewPessoaFisica();
            ModelViewPessoa person = new ModelViewPessoa();

            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand(String.Format("SELECT * FROM Pessoa inner join PessoaFisica on IDPessoa = IDPessoaFisica where IDPessoa = {0}", id), c.con);
            c.rd = c.query.ExecuteReader();

            while (c.rd.Read())
            {
                person.Email = c.rd["Email"].ToString();
                person.Nome = c.rd["Nome"].ToString();
                person.Telefone = c.rd["Telefone"].ToString();
                person.Descricao = c.rd["Descricao"].ToString();
                person.Imagem = c.rd["Imagem"].ToString();

                pf.RG = c.rd["RG"].ToString();
                pf.CPF = c.rd["CPF"].ToString();


                pf.Pessoa = person;



            }
            c.con.Close();

            return View(pf);
        }

        // GET: PessoaFisica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PessoaFisica/Create
        [HttpPost]
        public ActionResult Create(ModelViewPessoaFisica pf)
        {
            if (ModelState.IsValid)
            {

                try
                {



                    //pega o objeto imagem do input
                    HttpPostedFileBase foto = Request.Files["Pessoa.Imagem"];


                    // pega o nome do arquivo
                    var nomeArquivo = Path.GetFileName(foto.FileName);
                    //cria o caminho final da imagem
                    var caminho = Path.Combine(Server.MapPath(Url.Content("~/assets/perfil/")), nomeArquivo);
                    //salva a foto no caminho
                    foto.SaveAs(caminho);
                    //imagem da pessoafisica criado recebe o caminho da imagem salva
                    pf.Pessoa.Imagem = Path.Combine(Url.Content("/assets/perfil/"), nomeArquivo);




                    c = new Conexao();
                    c.con.Open();
                    c.query = c.con.CreateCommand();
                    c.query.CommandText = "INSERT INTO Pessoa (Email,Senha,Telefone,Nome,Descricao,Imagem) VALUES (@email,@senha,@telefone,@nome,@descricao,@imagem)";
                    c.query.Parameters.AddWithValue("@email", pf.Pessoa.Email);
                    c.query.Parameters.AddWithValue("@senha", pf.Pessoa.Senha);
                    c.query.Parameters.AddWithValue("@nome", pf.Pessoa.Nome);
                    c.query.Parameters.AddWithValue("@telefone", pf.Pessoa.Telefone);
                    c.query.Parameters.AddWithValue("@descricao", pf.Pessoa.Descricao);
                    c.query.Parameters.AddWithValue("@imagem", pf.Pessoa.Imagem);
                    c.query.ExecuteNonQuery();


                    //pega o id da pessoa que foi inserida e atribui à pessoafisica
                    pf.Pessoa.IDPessoa = Convert.ToInt32(c.query.LastInsertedId);
                    c.con.Close();



                    c.con.Open();
                    c.query.CommandText = "INSERT INTO PessoaFisica (IDPessoaFisica,RG,CPF) VALUES (@idpessoafisica, @rg, @cpf)";
                    c.query.Parameters.AddWithValue("@idpessoafisica", pf.Pessoa.IDPessoa);
                    c.query.Parameters.AddWithValue("@rg", pf.RG);
                    c.query.Parameters.AddWithValue("@cpf", pf.CPF);
                    c.query.ExecuteNonQuery();

                    c.con.Close();


                    return RedirectToAction("Index", "Home");

                }
                catch (Exception e)
                {
                    return View();
                }


            }

            return View();
        }

        // GET: PessoaFisica/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PessoaFisica/Edit/5
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

        // GET: PessoaFisica/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PessoaFisica/Delete/5
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
