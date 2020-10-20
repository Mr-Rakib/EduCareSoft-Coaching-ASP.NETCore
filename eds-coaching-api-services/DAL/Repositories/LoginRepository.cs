using eds_coaching_api_services.DAL.Interface;
using eds_coaching_api_services.Models;
using eds_coaching_api_services.Utility;
using eds_coaching_api_services.Utility.Database;
using eds_coaching_api_services.Utility.Dependencies;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace eds_coaching_api_services.DAL.Repositories
{
    public class LoginRepository : ICRUDRepository<Login>
    {
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Login> FindAll()
        {
            List<Login> loginList = new List<Login>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new MySqlCommand(Procedures.GetAllLogin, connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Login login = new Login
                                {
                                    Username = reader.GetString("username"),
                                    Password = reader.GetString("password"),
                                    UserRole = reader.GetString("userRole"),
                                    LastLoginDate = ReadNullorDateTime("lastLoginDate"),

                                    IsLoginActive = reader.GetInt32("isLoginActive"),
                                    InstitutionProfile_id = reader.GetInt32("institutionProfile_id")
                                };
                                loginList.Add(login);
                            }
                        }
                    }
                }
            }catch(Exception ex)
            {
                Logger.Log(ex);
            }
            return loginList;
        }

        public bool Save(Login items)
        {
            throw new NotImplementedException();
        }

        public bool Update(Login login)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.UpdateLogin, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new MySqlParameter("@username", login.Username));
                        command.Parameters.Add(new MySqlParameter("@password", login.Password));
                        command.Parameters.Add(new MySqlParameter("@userRole", login.UserRole));
                        command.Parameters.Add(new MySqlParameter("@lastLoginDate", login.LastLoginDate));
                        command.Parameters.Add(new MySqlParameter("@isLoginActive", login.IsLoginActive));
                        command.Parameters.Add(new MySqlParameter("@institutionProfile_id", login.InstitutionProfile_id));

                        connection.Open();
                        status = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                }
            }
            return (status > 0) ? true : false;
        }

        private DateTime? ReadNullorDateTime(string column)
        {
            return (reader.IsDBNull(column)) ?
                (DateTime?)null : DateTime.Parse(reader.GetString(column));
        }
    }
}
