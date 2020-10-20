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
    public class ExamInformationRepository : ICRUDRepository<ExamInformation>
    {
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public List<ExamInformation> FindAll()
        {
            List<ExamInformation> ExamInformationList = new List<ExamInformation>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new MySqlCommand(Views.ALLExamInformation, connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ExamInformation ExamInformation = new ExamInformation
                                {
                                    Id              = reader.GetInt32("id"),
                                    ExamName        = reader.GetString("ExamName"),
                                    InstitutionId   = reader.GetInt32("institution_id"),
                                    EntryBy_id      = reader.GetInt32("EntryBy_id")
                                };
                                ExamInformationList.Add(ExamInformation);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return ExamInformationList;
        }

        public bool Update(ExamInformation ExamInformation)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.UpdateExamInformation, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new MySqlParameter("@id", ExamInformation.Id));
                        command.Parameters.Add(new MySqlParameter("@ExamName", ExamInformation.ExamName));
                        command.Parameters.Add(new MySqlParameter("@InstitutionId", ExamInformation.InstitutionId));
                        command.Parameters.Add(new MySqlParameter("@EntryBy_id", ExamInformation.EntryBy_id));

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

        public bool Save(ExamInformation ExamInformation)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.SaveExamInformation, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new MySqlParameter("@ExamName", ExamInformation.ExamName));
                        command.Parameters.Add(new MySqlParameter("@InstitutionId", ExamInformation.InstitutionId));
                        command.Parameters.Add(new MySqlParameter("@EntryBy_id", ExamInformation.EntryBy_id));

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
                    using (command = new MySqlCommand(Procedures.DeleteExamInformation, connection))
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
