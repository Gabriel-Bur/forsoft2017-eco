using eco_solution.DAO;
using eco_solution.ModelView;
using eco_solution.Prevent;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eco_solution.Controllers
{
    public class PessoaJuridicaController : Controller
    {
        private Conexao c;


        // GET: PessoaJuridica
        [NoDirect]
        public ActionResult Index()
        {

            try
            {
                //list de PessoaJuridica
                List<ModelViewPessoaJuridica> lista = new List<ModelViewPessoaJuridica>();


                c = new Conexao();
                c.con.Open();
                c.query = new MySqlCommand("SELECT * FROM Pessoa inner join PessoaJuridica on IDPessoa = IDPessoaJuridica", c.con);
                c.rd = c.query.ExecuteReader();

                while (c.rd.Read())
                {
                    ModelViewPessoaJuridica pj = new ModelViewPessoaJuridica();
                    ModelViewPessoa person = new ModelViewPessoa();


                    pj.IDPessoaJuridica = Convert.ToInt32(c.rd["IDPessoaJuridica"].ToString());
                    pj.RazaoSocial = c.rd["RazaoSocial"].ToString();
                    pj.AreaDeAtuacao = c.rd["AreaDeAtuacao"].ToString();

                    person.Nome = c.rd["Nome"].ToString();
                    person.Imagem = c.rd["Imagem"].ToString();
                    person.Descricao = c.rd["Descricao"].ToString();
                    person.Telefone = c.rd["Telefone"].ToString();



                    //faz a uniao das informaçoes de pessoaJuridica e Pessoa pra view
                    pj.Pessoa = person;

                    lista.Add(pj);
                }
                c.con.Close();

                return View(lista);
            }
            catch
            {
                return View(new List<ModelViewPessoaJuridica>());
            }


        }





        // GET: PessoaJuridica/Details/5
        [NoDirect]
        public ActionResult Details(int id)
        {
            try
            {
                ModelViewPessoaJuridica pj = new ModelViewPessoaJuridica();
                ModelViewPessoa person = new ModelViewPessoa();

                c = new Conexao();
                c.con.Open();
                c.query = new MySqlCommand(String.Format("SELECT * FROM Pessoa inner join PessoaJuridica on IDPessoa = IDPessoaJuridica where IDPessoa = {0}", id), c.con);
                c.rd = c.query.ExecuteReader();

                while (c.rd.Read())
                {
                    person.Email = c.rd["Email"].ToString();
                    person.Telefone = c.rd["Telefone"].ToString();
                    person.Nome = c.rd["Nome"].ToString();
                    person.Descricao = c.rd["Descricao"].ToString();
                    person.Imagem = c.rd["Imagem"].ToString();

                    pj.RazaoSocial = c.rd["RazaoSocial"].ToString();
                    pj.CNPJ = c.rd["CNPJ"].ToString();
                    pj.AreaDeAtuacao = c.rd["AreaDeAtuacao"].ToString();
                    pj.Logradouro = c.rd["Logradouro"].ToString();
                    pj.Bairro = c.rd["Bairro"].ToString();
                    pj.Numero = c.rd["Numero"].ToString();
                    pj.Complemento = c.rd["Complemento"].ToString();

                    pj.Pessoa = person;



                }
                c.con.Close();

                return View(pj);
            }
            catch
            {
                return View();
            }


        }






        // GET: PessoaJuridica/Create
        [NoDirect]
        public ActionResult Create()
        {
            return View();
        }




        // POST: PessoaJuridica/Create
        [NoDirect]
        [HttpPost]
        public ActionResult Create(ModelViewPessoaJuridica pj)
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
                    var caminho = Path.Combine(Server.MapPath(Url.Content("~/assets/perfiljuridico/")), nomeArquivo);
                    //salva a foto no caminho
                    foto.SaveAs(caminho);
                    //imagem da pessoafisica criado recebe o caminho da imagem salva
                    pj.Pessoa.Imagem = Path.Combine(Url.Content("/assets/perfiljuridico/"), nomeArquivo);




                    c = new Conexao();
                    c.con.Open();
                    c.query = c.con.CreateCommand();
                    c.query.CommandText = "INSERT INTO Pessoa (Email,Senha,Telefone,Nome,Descricao,Imagem) VALUES (@email,@senha,@telefone,@nome,@descricao,@imagem)";
                    c.query.Parameters.AddWithValue("@email", pj.Pessoa.Email);
                    c.query.Parameters.AddWithValue("@senha", pj.Pessoa.Senha);
                    c.query.Parameters.AddWithValue("@nome", pj.Pessoa.Nome);
                    c.query.Parameters.AddWithValue("@telefone", pj.Pessoa.Telefone);
                    c.query.Parameters.AddWithValue("@descricao", pj.Pessoa.Descricao);
                    c.query.Parameters.AddWithValue("@imagem", pj.Pessoa.Imagem);
                    c.query.ExecuteNonQuery();


                    //pega o id da pessoa que foi inserida e atribui à pessoajuridica
                    pj.Pessoa.IDPessoa = Convert.ToInt32(c.query.LastInsertedId);
                    c.con.Close();



                    c.con.Open();
                    c.query.CommandText = "INSERT INTO PessoaJuridica " +
                                            "(IDPessoaJuridica,RazaoSocial,CNPJ,Logradouro,CEP,Cidade,Bairro,Numero,AreaDeAtuacao,Complemento) " +
                                            "VALUES (@idpessoajuridica, @razaosocial, @cnpj,@logradouro,@cep,@cidade,@bairro,@numero,@areadeatuacao,@complemento)";

                    c.query.Parameters.AddWithValue("@idpessoajuridica", pj.Pessoa.IDPessoa);
                    c.query.Parameters.AddWithValue("@razaosocial", pj.RazaoSocial);
                    c.query.Parameters.AddWithValue("@cnpj", pj.CNPJ);
                    c.query.Parameters.AddWithValue("@logradouro", pj.Logradouro);
                    c.query.Parameters.AddWithValue("@cep", pj.CEP);
                    c.query.Parameters.AddWithValue("@cidade", pj.Cidade);
                    c.query.Parameters.AddWithValue("@bairro", pj.Bairro);
                    c.query.Parameters.AddWithValue("@numero", pj.Numero);
                    c.query.Parameters.AddWithValue("@areadeatuacao", pj.AreaDeAtuacao);
                    c.query.Parameters.AddWithValue("@complemento", pj.Complemento);
                    c.query.ExecuteNonQuery();

                    c.con.Close();


                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }







        // GET: PessoaJuridica/Edit/5
        [NoDirect]
        public ActionResult Edit(int id)
        {
            return View();
        }









        // POST: PessoaJuridica/Edit/5
        [NoDirect]
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




        // GET: PessoaJuridica/Delete/5
        [NoDirect]
        public ActionResult Delete(int id)
        {
            return View();
        }





        // POST: PessoaJuridica/Delete/5
        [NoDirect]
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
