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
    public class SubjectManagerRepository : ICRUDRepository<SubjectManager>
    {
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public List<SubjectManager> FindAll()
        {
            List<SubjectManager> SubjectManagerList = new List<SubjectManager>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new MySqlCommand(Views.ALLSubjectManager, connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SubjectManager SubjectManager = new SubjectManager
                                {
                                    Id      = reader.GetInt32("id"),
                                    Fees    = reader.GetFloat("fees"),
                                    Session = reader.GetInt32("session"),
                                
                                Subject = new Subject
                                {
                                    Id = reader.GetInt32("subject_id"),
                                    Name = reader.GetString("SubjectName"),
                                    Code = reader.GetString("SubjectCode"),
                                    Institution_id = reader.GetInt32("institution_id"),
                                },
                                Class = new Class
                                {
                                    Id = reader.GetInt32("class_id"),
                                    Name = reader.GetString("ClassName"),
                                    Institution_id = reader.GetInt32("institution_id"),
                                },
                                Batch = new Batch
                                {
                                    Id = reader.GetInt32("batch_id"),
                                    Name = reader.GetString("BatchName"),
                                    Institution_id = reader.GetInt32("institution_id"),
                                }
                            };
                            SubjectManagerList.Add(SubjectManager);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return SubjectManagerList;
        }

        public bool Update(SubjectManager SubjectManager)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.UpdateSubjectManager, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new MySqlParameter("@id", SubjectManager.Id));
                        SetALLPeremeter(SubjectManager);

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

        public bool Save(SubjectManager SubjectManager)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.SaveSubjectManager, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SetALLPeremeter(SubjectManager);
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
                    using (command = new MySqlCommand(Procedures.DeleteSubjectManager, connection))
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

        private void SetALLPeremeter(SubjectManager SubjectManager)
        {
            command.Parameters.Add(new MySqlParameter("@subject_id", SubjectManager.Subject.Id));
            command.Parameters.Add(new MySqlParameter("@class_id", SubjectManager.Class.Id));
            command.Parameters.Add(new MySqlParameter("@batch_id", SubjectManager.Batch.Id));
            command.Parameters.Add(new MySqlParameter("@fees", SubjectManager.Fees));
            command.Parameters.Add(new MySqlParameter("@session", SubjectManager.Session));
        }

    }
}
