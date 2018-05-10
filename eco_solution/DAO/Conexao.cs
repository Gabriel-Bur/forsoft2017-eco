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
            // CentOS
             con = new MySqlConnection("server=10.74.0.4;port=3306;database=Ecossistema;uid=forsoft;password=E2A3C8F4;SslMode=none;Allow User Variables=True");


            //local
            //con = new MySqlConnection("server=192.168.0.45;port=3306;database=Ecossistema;uid=forsoft;password=f0rs0ft;SslMode=none;Allow User Variables=True");
        }


    }
}