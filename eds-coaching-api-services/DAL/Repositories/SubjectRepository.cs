using eds_coaching_api_services.DAL.Interface;
using eds_coaching_api_services.Models;
using eds_coaching_api_services.Utility.Database;
using eds_coaching_api_services.Utility;
using eds_coaching_api_services.Utility.Database;
using eds_coaching_api_services.Utility.Dependencies;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.DAL.Repositories
{
    public class SubjectRepository : ICRUDRepository<Subject>
    {
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public List<Subject> FindAll()
        {
            List<Subject> SubjectList = new List<Subject>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new MySqlCommand(Views.ALLSubject, connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Subject Subject = new Subject
                                {
                                    Id = reader.GetInt32("id"),
                                    Name = reader.GetString("SubjectName"),
                                    Code = reader.GetString("SubjectCode"),
                                    Institution_id = reader.GetInt32("institution_id"),

                                };
                                SubjectList.Add(Subject);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return SubjectList;
        }

        public bool Update(Subject Subject)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.UpdateSubject, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new MySqlParameter("@id", Subject.Id));
                        command.Parameters.Add(new MySqlParameter("@name", Subject.Name));
                        command.Parameters.Add(new MySqlParameter("@code", Subject.Code));
                        command.Parameters.Add(new MySqlParameter("@institution_id", Subject.Institution_id));

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

        public bool Save(Subject Subject)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.SaveSubject, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new MySqlParameter("@name", Subject.Name));
                        command.Parameters.Add(new MySqlParameter("@code", Subject.Code));
                        command.Parameters.Add(new MySqlParameter("@institution_id", Subject.Institution_id));

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

        public bool Delete(int id)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.DeleteSubject, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new MySqlParameter("@id", id));

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

    }
}
