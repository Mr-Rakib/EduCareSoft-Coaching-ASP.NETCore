using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata.Ecma335;
using MySql.Data.MySqlClient;
using eds_coaching_api_services.Utility;
using eds_coaching_api_services.Utility.Dependencies;
using System.Data.SqlClient;

namespace eds_coaching_api_services.Utility.Database
{
    public static class DBConnection
    {
        //-------Singleton Connection Design Patern----------
        private static MySqlConnection MySQLConnection;
        private static SqlConnection MsSQLConnection;
        private static readonly string connectionString = ConfigureAppSettings.Configure.GetSection("Connection")["ConnectionString"];

        public static MySqlConnection GetConnection()
        {
            MySQLConnection = null;
            try
            {
                MySQLConnection = new MySqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return MySQLConnection;
        }

        public static SqlConnection GetSQLConnection()
        {
            MsSQLConnection = null;
            try
            {
                MsSQLConnection = new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return MsSQLConnection;
        }
    }
}
