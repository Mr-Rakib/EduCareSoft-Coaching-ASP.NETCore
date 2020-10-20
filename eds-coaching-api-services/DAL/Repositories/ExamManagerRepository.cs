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
    public class ExamManagerRepository : ICRUDRepository<ExamManager>
    {
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public List<ExamManager> FindAll()
        {
            List<ExamManager> ExamManagerList = new List<ExamManager>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new MySqlCommand(Views.ALLExamManager, connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;

                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ExamManager ExamManager = new ExamManager
                                {
                                    Id      = reader.GetInt32("id"),
                                    ExamInformation = new ExamInformation()
                                    {
                                        Id = reader.GetInt32("examInformation_id")
                                    },
                                    SubjectManager = new SubjectManager() 
                                    {
                                        Id = reader.GetInt32("subjectManager_id")
                                    },
                                    GradingSystem = new GradingSystem()
                                    {
                                        Id = reader.GetInt32("gradingSystem_id")
                                    },
                                    FullMarks   = reader.GetFloat("fullMarks"),
                                    ExamDate    = reader.GetDateTime("date"),
                                    TimeStart   = reader.GetTimeSpan("timeStart"),
                                    TimeEnd     = reader.GetTimeSpan("timeEnd"),
                                    RoomNumber  = reader.GetString("roomNumber"),
                                    ExamYear    = reader.GetInt32("year"),
                                    EntryBy_id  = reader.GetInt32("EntryBy_id")
                                };
                                ExamManagerList.Add(ExamManager);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return ExamManagerList;
        }

        public bool Update(ExamManager ExamManager)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.UpdateExamManager, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new MySqlParameter("@Id", ExamManager.Id));
                        SetAllParameters(ExamManager);

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

        private void SetAllParameters(ExamManager ExamManager)
        {
            command.Parameters.Add(new MySqlParameter("@ExamInformationId", ExamManager.ExamInformation.Id));
            command.Parameters.Add(new MySqlParameter("@SubjectManagerId" , ExamManager.SubjectManager.Id));
            command.Parameters.Add(new MySqlParameter("@GradingSystemId"  , ExamManager.GradingSystem.Id));
            command.Parameters.Add(new MySqlParameter("@FullMarks"        , ExamManager.FullMarks));
            command.Parameters.Add(new MySqlParameter("@ExamDate"         , ExamManager.ExamDate));
            command.Parameters.Add(new MySqlParameter("@TimeStart"        , ExamManager.TimeStart));
            command.Parameters.Add(new MySqlParameter("@TimeEnd"          , ExamManager.TimeEnd));
            command.Parameters.Add(new MySqlParameter("@RoomNumber"       , ExamManager.RoomNumber));
            command.Parameters.Add(new MySqlParameter("@ExamYear"         , ExamManager.ExamYear));
            command.Parameters.Add(new MySqlParameter("@EntryBy_id"       , ExamManager.EntryBy_id));
        }

        public bool Save(ExamManager ExamManager)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.SaveExamManager, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SetAllParameters(ExamManager);

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
                    using (command = new MySqlCommand(Procedures.DeleteExamManager, connection))
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
