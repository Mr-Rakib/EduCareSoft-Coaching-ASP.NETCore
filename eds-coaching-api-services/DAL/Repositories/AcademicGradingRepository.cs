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
    public class AcademicGradingRepository : ICRUDRepository<AcademicGrading>
    {
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public List<AcademicGrading> FindAll()
        {
            List<AcademicGrading> AcademicGradingList = new List<AcademicGrading>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new MySqlCommand(Views.ALLAcademicGrading, connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AcademicGrading AcademicGrading = new AcademicGrading
                                {
                                    Id                  = reader.GetInt32("id"),
                                    GradingSystem       = new GradingSystem()
                                    {
                                        Id              = reader.GetInt32("GradingSystem_id"),
                                        SystemName      = reader.GetString("SystemName"),
                                        InstitutionId   = reader.GetInt32("institution_id")
                                    },
                                    GradeName           = reader.GetString("GradeName"),

                                    PercentageFrom      = reader.GetFloat("percentageFrom"),
                                    PercentageTo        = reader.GetFloat("percentageTo"),
                                    Gpa                 = reader.GetFloat("gpa"),
                                    EntryBy_id          = reader.GetInt32("EntryBy_id")
                                };
                                AcademicGradingList.Add(AcademicGrading);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return AcademicGradingList;
        }

        internal bool DeleteByGradingSystem(int id)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.DeleteAcademicGradingByGradingSystem, connection))
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

        public bool Update(AcademicGrading AcademicGrading)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.UpdateAcademicGrading, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new MySqlParameter("@Id", AcademicGrading.Id));
                        SetALLData(AcademicGrading);

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

        public bool Save(AcademicGrading AcademicGrading)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.SaveAcademicGrading, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SetALLData(AcademicGrading);

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
                    using (command = new MySqlCommand(Procedures.DeleteAcademicGrading, connection))
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
                return (status > 0) ? true : false;
            }
        }
        
        private void SetALLData(AcademicGrading AcademicGrading)
        {
            command.Parameters.Add(new MySqlParameter("@GradingSystemId", AcademicGrading.GradingSystem.Id));
            command.Parameters.Add(new MySqlParameter("@GradeName", AcademicGrading.GradeName));
            command.Parameters.Add(new MySqlParameter("@PercentageFrom", AcademicGrading.PercentageFrom));
            command.Parameters.Add(new MySqlParameter("@PercentageTo", AcademicGrading.PercentageTo));
            command.Parameters.Add(new MySqlParameter("@Gpa", AcademicGrading.Gpa));
            command.Parameters.Add(new MySqlParameter("@EntryBy_id", AcademicGrading.EntryBy_id));
        }
    }
}
