using eds_coaching_api_services.Utility.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Utility.Database.Type
{
    public class MsSQL
    {
        public static SqlConnection connection;
        public static SqlCommand command;
        public static SqlDataReader reader;

        public static SqlConnection GetConection()
        {
            connection = DBConnection.GetSQLConnection();
            return connection;
        }
        public static SqlCommand GetCommand(string Procedures, SqlConnection Connection)
        {
            command = new SqlCommand(Procedures, Connection);
            return command;
        }

        public static SqlDataReader GetReader(SqlCommand Command)
        {
            reader = Command.ExecuteReader();
            return reader;
        }
    }
}
