using Estoque_Pessoal.Controllers;
using Estoque_Pessoal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

namespace Estoque_Pessoal.DAL
{
    public class ExemploDAO
    {
        SqlConnection connection = Singleton.Instance.Connection;

        /*
         * Exemplo de SQL do ADO.NET normal, sem dapper, mais adequado para inserções e queries que não puxem dados.
         * Desconsidere as queries e os parâmetros já que foram pegos de outro projeto, isso é só um exemplo e não deve ser executado
         * 
         * Caso use em outro projeto, não se esqueça de verificar se há apenas uma instância do banco no Singleton 
         * e que a connection string está no web.config (não se preocupe, já fiz isso).
         */

        public bool Add(Cliente t)
        {
            string sql = "INSERT INTO gerenergia.consumo(DATA_INICIO, PERFIL_ID)" +
                "VALUES (@DATA_INICIO, @PERFIL_ID);  SELECT LAST_INSERT_ID(); select LAST_INSERT_ID();";

            using (connection)
            {
                SqlCommand command = new SqlCommand(sql, connection);
                try
                {
                    command.Parameters.Add("@DATA_INICIO", SqlDbType.DateTime).Value = t.Id;
                    command.Parameters.Add("@PERFIL_ID", SqlDbType.Int).Value = t.Nome;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    connection.Close();
                    return false;
                }
            }

        }

        /*
         * Essa query utiliza ORM (Object Relational Mapping) Dapper!
         * PARA MAPEAR AS VARIAVEIS COM A QUERY E PUXAR OS DADOS TEM QUE USAR O ALIAS NAS VARIAVEIS, exemplo -> "Select ID id, NOME_USUARIO Nome, etc"
         */

        public IEnumerable<Cliente> List()
        {
            string sql = "SELECT ID id," +
                "DATA_INICIO dateTimeInicio," +
                "DATA_FIM dateTimeFim," +
                "PERFIL_ID PerfilID " +
                "FROM gerenergia.consumo " +
                "ORDER BY PERFIL_ID";

            using (IDbConnection con = connection)
            {
                try
                {
                    con.Open();
                    var resultado = con.QueryMultiple(sql);
                    var consumos = resultado.Read<Cliente>();
                    con.Close();
                    return consumos;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    con.Close();
                    return null;
                }
            }
        }

        
    }
}