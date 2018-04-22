using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace eco_solution.DAO
{
    public class Conexao
    {
        public MySqlConnection con { get; set; }
        public MySqlCommand query { get; set; }

        public MySqlDataReader rd { get; set; }

        public Conexao()
        {
            con = new MySqlConnection("server=localhost;port=3306;database=ecossistema;uid=root;password=root;SslMode=none");
        }


    }
}