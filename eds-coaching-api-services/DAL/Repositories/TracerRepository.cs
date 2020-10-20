using eds_coaching_api_services.BLL.Interfaces;
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
    public class TracerRepository : ICRUDRepository<Tracer>
    {
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Tracer> FindAll()
        {
            List<Tracer> TrackerList = new List<Tracer>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new MySqlCommand(Views.ALLTracer , connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Tracer Tracker = new Tracer
                                {
                                    Id                  = reader.GetInt32("id"),
                                    Actor_id            = reader.GetInt32("actor_id"),
                                    ActionName          = reader.GetString("actionName"),
                                    TableName           = reader.GetString("tableName"),
                                    ActionApplied_id    = reader.GetInt32("actionApplied_id"),
                                    ActionTime          = reader.GetDateTime("actionTime"),
                                    Institution_id      = reader.GetInt32("institution_id")
                                };
                                TrackerList.Add(Tracker);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return TrackerList;
        }

        public bool Save(Tracer Tracker)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.SaveTracker, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new MySqlParameter("@actor_id", Tracker.Actor_id));
                        command.Parameters.Add(new MySqlParameter("@actionName", Tracker.ActionName));
                        command.Parameters.Add(new MySqlParameter("@tableName", Tracker.TableName));
                        command.Parameters.Add(new MySqlParameter("@actionApplied_id", Tracker.ActionApplied_id));
                        command.Parameters.Add(new MySqlParameter("@actionTime", Tracker.ActionTime));
                        command.Parameters.Add(new MySqlParameter("@institution_id", Tracker.Institution_id));

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

        public bool Update(Tracer items)
        {
            throw new NotImplementedException();
        }
    }
}
