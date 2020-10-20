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
    public class GradingSystemRepository : ICRUDRepository<GradingSystem>
    {
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public List<GradingSystem> FindAll()
        {
            List<GradingSystem> GradingSystemList = new List<GradingSystem>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new MySqlCommand(Views.ALLGradingSystem, connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GradingSystem GradingSystem = new GradingSystem
                                {
                                    Id              = reader.GetInt32("id"),
                                    SystemName      = reader.GetString("SystemName"),
                                    InstitutionId   = reader.GetInt32("institution_id"),
                                    EntryBy_id      = reader.GetInt32("EntryBy_id")
                                };
                                GradingSystemList.Add(GradingSystem);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return GradingSystemList;
        }

        public bool Update(GradingSystem GradingSystem)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.UpdateGradingSystem, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new MySqlParameter("@Id", GradingSystem.Id));
                        command.Parameters.Add(new MySqlParameter("@SystemName", GradingSystem.SystemName));
                        command.Parameters.Add(new MySqlParameter("@EntryBy_id", GradingSystem.EntryBy_id));
                        command.Parameters.Add(new MySqlParameter("@InstitutionId", GradingSystem.InstitutionId));

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

        public bool Save(GradingSystem GradingSystem)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.SaveGradingSystem, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new MySqlParameter("@SystemName", GradingSystem.SystemName));
                        command.Parameters.Add(new MySqlParameter("@EntryBy_id", GradingSystem.EntryBy_id));
                        command.Parameters.Add(new MySqlParameter("@InstitutionId", GradingSystem.InstitutionId));

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
                    using (command = new MySqlCommand(Procedures.DeleteGradingSystem, connection))
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
