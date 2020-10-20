using eds_coaching_api_services.DAL.Interface;
using eds_coaching_api_services.Models;
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
    public class ResultRepository : ICRUDRepository<Result>
    {
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public List<Result> FindAll()
        {
            List<Result> ResultList = new List<Result>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new MySqlCommand(Views.ALLResult, connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Result Result = new Result
                                {
                                    Id = reader.GetInt32("id"),
                                    ExamManager = new ExamManager()
                                    {
                                        Id = reader.GetInt32("examManager_id")
                                    },
                                    StudentId = reader.GetInt32("student_id"),
                                    Marks       = reader.GetFloat("marks"),
                                    Date        = reader.GetDateTime("date"),
                                    EntryBy_id  = reader.GetInt32("EntryBy_id") 
                                };
                                ResultList.Add(Result);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return ResultList;
        }

        public bool Update(Result Result)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.UpdateResult, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new MySqlParameter("@Id", Result.Id));
                        SetAllPeremerters(Result);

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

        private void SetAllPeremerters(Result Result)
        {
            command.Parameters.Add(new MySqlParameter("@ExamManagerId", Result.ExamManager.Id));
            command.Parameters.Add(new MySqlParameter("@StudentId", Result.StudentId));
            command.Parameters.Add(new MySqlParameter("@Marks", Result.Marks));
            command.Parameters.Add(new MySqlParameter("@Date", Result.Date));
            command.Parameters.Add(new MySqlParameter("@EntryBy_id", Result.EntryBy_id));
        }

        public bool Save(Result Result)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.SaveResult, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SetAllPeremerters(Result);

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
                    using (command = new MySqlCommand(Procedures.DeleteResult, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new MySqlParameter("@Id", id));

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
