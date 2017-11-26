using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Estoque_Pessoal.Controllers
{
    internal class Singleton
    {

        private static readonly Singleton instance = new Singleton();
        private readonly ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["ModelosADO"];
        private readonly SqlConnection connection;
        //private readonly Entities entities;

        private Singleton()
        {
            //entities = new Entities();
            connection = new SqlConnection(connectionString.ConnectionString);
        }

        public static Singleton Instance
        {
            get
            {
                return instance;
            }
        }

        public SqlConnection Connection
        {
            get
            {
                return connection;
            }
        }

        //public Entities Entities
        //{
        //    get
        //    {
        //        return entities;
        //    }
        //}
    }
}
