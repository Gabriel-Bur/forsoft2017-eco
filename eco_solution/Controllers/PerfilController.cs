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
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
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






        //Redireciona para Conta PF ou PJ
        public ActionResult EditConta(int id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }


            ModelViewPessoaFisica pf = new ModelViewPessoaFisica();
            ModelViewPessoaJuridica pj = new ModelViewPessoaJuridica();




            ///Pessoa Fisica
            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand(String.Format("SELECT * FROM PessoaFisica where IDPessoaFisica = {0}", id), c.con);
            c.rd = c.query.ExecuteReader();
            while (c.rd.Read())
            {
                pf.IDPessoaFisica = Convert.ToInt32(c.rd["IDPessoaFIsica"].ToString());
                pf.CPF = c.rd["CPF"].ToString();
                pf.RG = c.rd["RG"].ToString();
            }
            c.con.Close();


            ///Pessoa Juridica 
            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand(String.Format("SELECT * FROM PessoaJuridica where IDPessoaJuridica = {0}", id), c.con);
            c.rd = c.query.ExecuteReader();
            while (c.rd.Read())
            {
                pj.IDPessoaJuridica = Convert.ToInt32(c.rd["IDPessoaJuridica"].ToString());
                pj.RazaoSocial = c.rd["RazaoSocial"].ToString();
                pj.CNPJ = c.rd["CNPJ"].ToString();
                pj.Logradouro = c.rd["Logradouro"].ToString();
                pj.CEP = c.rd["CEP"].ToString();
                pj.Cidade = c.rd["Cidade"].ToString();
                pj.Bairro = c.rd["Bairro"].ToString();
                pj.Numero = c.rd["Numero"].ToString();
                pj.Complemento = c.rd["Complemento"].ToString();
                pj.AreaDeAtuacao = c.rd["AreaDeAtuacao"].ToString();


            }
            c.con.Close();



            if (pf.IDPessoaFisica != 0)
            {
                return RedirectToAction("ContaPF", "Perfil");
            }
            if (pj.IDPessoaJuridica != 0)
            {

                return RedirectToAction("ContaPJ", "Perfil");
            }



            return RedirectToAction("Index", "Perfil");

        }














        //get pagina EditContaPF
        [HttpGet]
        public ActionResult ContaPF()
        {
            ModelViewPessoaFisica pf = new ModelViewPessoaFisica();
            var id = Session["id"];

            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand(String.Format("SELECT * FROM PessoaFisica where IDPessoaFisica = {0}", id), c.con);
            c.rd = c.query.ExecuteReader();
            while (c.rd.Read())
            {
                pf.IDPessoaFisica = Convert.ToInt32(c.rd["IDPessoaFIsica"].ToString());
                pf.CPF = c.rd["CPF"].ToString();
                pf.RG = c.rd["RG"].ToString();
            }
            c.con.Close();

            return View(pf);
        }












        //get pagina EditContaPJ
        [HttpGet]
        public ActionResult ContaPJ()
        {
            ModelViewPessoaJuridica pj = new ModelViewPessoaJuridica();
            var id = Session["id"];

            c = new Conexao();
            c.con.Open();
            c.query = new MySqlCommand(String.Format("SELECT * FROM PessoaJuridica where IDPessoaJuridica = {0}", id), c.con);
            c.rd = c.query.ExecuteReader();
            while (c.rd.Read())
            {
                pj.IDPessoaJuridica = Convert.ToInt32(c.rd["IDPessoaJuridica"].ToString());
                pj.RazaoSocial = c.rd["RazaoSocial"].ToString();
                pj.CNPJ = c.rd["CNPJ"].ToString();
                pj.Logradouro = c.rd["Logradouro"].ToString();
                pj.CEP = c.rd["CEP"].ToString();
                pj.Cidade = c.rd["Cidade"].ToString();
                pj.Bairro = c.rd["Bairro"].ToString();
                pj.Numero = c.rd["Numero"].ToString();
                pj.AreaDeAtuacao = c.rd["AreaDeAtuacao"].ToString();
                pj.Complemento = c.rd["Complemento"].ToString();

            }
            c.con.Close();

            return View(pj);
        }

        //edita pessoa juridica
        [HttpPost]
        public ActionResult ContaPJ(ModelViewPessoaJuridica pj)
        {


            try
            {
                pj.IDPessoaJuridica = Convert.ToInt32(Session["id"]);


                c = new Conexao();
                c.con.Open();
                c.query = c.con.CreateCommand();
                c.query.CommandText = "Update PessoaJuridica set " +
                "Logradouro=@logradouro," +
                "CEP=@cep," +
                "Cidade=@cidade," +
                "Bairro=@bairro," +
                "Numero=@numero," +
                "AreaDeAtuacao=@area," +
                "Complemento=@complemento" +
                " where IDPessoaJuridica = @id";
                c.query.Parameters.AddWithValue("@id", pj.IDPessoaJuridica);
                c.query.Parameters.AddWithValue("@logradouro", pj.Logradouro);
                c.query.Parameters.AddWithValue("@cep", pj.CEP);
                c.query.Parameters.AddWithValue("@cidade", pj.Cidade);
                c.query.Parameters.AddWithValue("@bairro", pj.Bairro);
                c.query.Parameters.AddWithValue("@numero", pj.Numero);
                c.query.Parameters.AddWithValue("@area", pj.AreaDeAtuacao);
                c.query.Parameters.AddWithValue("@complemento", pj.Complemento);
                c.query.ExecuteNonQuery();
                c.con.Close();


                return RedirectToAction("Index", "Perfil");

            }
            catch (Exception e)
            {
                return RedirectToAction("ContaPJ", "Perfil");
            }


        }









        // Post: Perfil/Edit/5
        /// EDIT PERFIl
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

                return RedirectToAction("Index", "Perfil");

            }

            return View();
        }


        // POST: Perfil/Edit/5
        /// EDIT PERFIl
        [HttpGet]
        public ActionResult Edit(int id)
        {

            if (Session["id"] == null)
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
