using eds_coaching_api_services.Utility.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Utility.Database.Type
{
    public static class MySQL
    {
        public static MySqlConnection connection;
        public static MySqlCommand command;
        public static MySqlDataReader reader;

        public static MySqlConnection GetConection()
        {
            connection = DBConnection.GetConnection();
            return connection;
        }
        public static MySqlCommand GetCommand(string Procedures, MySqlConnection Connection)
        {
            command = new MySqlCommand(Procedures, Connection);
            return command;
        }

        public static MySqlDataReader GetReader(MySqlCommand Command)
        {
            reader = Command.ExecuteReader();
            return reader;
        }
    }
}
