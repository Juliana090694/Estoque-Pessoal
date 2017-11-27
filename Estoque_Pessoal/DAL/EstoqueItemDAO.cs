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
    public class EstoqueItemDAO
    {

        public int Add(EstoqueItem t)
        {
            string sql = "INSERT INTO dbo.EstoqueItemSet (Quantidade, Item_Id) " +
                "VALUES (@Quantidade, @Item_Id);  SELECT @@IDENTITY;";

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ModelosADO"].ConnectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand(sql, connection);
                try
                {
                    command.Parameters.Add("@Quantidade", SqlDbType.Int).Value = t.Quantidade;
                    command.Parameters.Add("@Item_Id", SqlDbType.Int).Value = t.Id;
                    
                    connection.Open();
                    int id;
                    int.TryParse(command.ExecuteScalar().ToString(), out id);
                    connection.Close();
                    return id;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    connection.Close();
                    return 0;
                }
            }

        }
    }
}