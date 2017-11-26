using Estoque_Pessoal.Controllers;
using Estoque_Pessoal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using System.Configuration;

namespace Estoque_Pessoal.DAL
{
    public class EstoqueDAO
    {

        public bool Add(Estoque t)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ModelosADO"].ConnectionString);

            string sql = "INSERT INTO dbo.EstoqueSet (EstoqueItem_Id, Cliente_Id) " +
                "VALUES (@EstoqueItem_Id, @Cliente_Id);";
            
            using (connection)
            {
                SqlCommand command = new SqlCommand(sql, connection);
                try
                {
                    command.Parameters.Add("@EstoqueItem_Id", SqlDbType.Int).Value = t.EstoqueItem.Id;
                    command.Parameters.Add("@Cliente_Id", SqlDbType.Int).Value = t.Cliente.Id;
                    
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    connection.Close();
                    return false;
                }
            }

        }
    }
}