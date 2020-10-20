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
    public class ClassRoutineRepository : ICRUDRepository<ClassRoutine>
    {
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public List<ClassRoutine> FindAll()
        {
            List<ClassRoutine> ClassRoutineList = new List<ClassRoutine>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new MySqlCommand(Views.ALLClassRoutine, connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClassRoutine ClassRoutine = new ClassRoutine
                                {
                                    Id = reader.GetInt32("id"),
                                    StaffId = reader.GetInt32("staff_id"),
                                    SubjectManager = new SubjectManager 
                                    {
                                        Id = reader.GetInt32("subjectManager_id")
                                    },

                                    Day  = reader.GetString("day"),
                                    TimeStart = reader.GetString("timeStart"),
                                    TimeEnd = reader.GetString("timeEnd"),
                                    RoomNumber = reader.GetInt32("roomNumber")

                                };
                                ClassRoutineList.Add(ClassRoutine);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return ClassRoutineList;
        }

        public bool Update(ClassRoutine ClassRoutine)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.UpdateClassRoutine, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new MySqlParameter("@id", ClassRoutine.Id));
                        SetALLValues(ClassRoutine);

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

        private void SetALLValues(ClassRoutine ClassRoutine)
        {
            command.Parameters.Add(new MySqlParameter("@staff_id", ClassRoutine.StaffId));
            command.Parameters.Add(new MySqlParameter("@subjectManager_id", ClassRoutine.SubjectManager.Id));
            command.Parameters.Add(new MySqlParameter("@day", ClassRoutine.Day));
            command.Parameters.Add(new MySqlParameter("@timeStart", ClassRoutine.TimeStart));
            command.Parameters.Add(new MySqlParameter("@timeEnd", ClassRoutine.TimeEnd));
            command.Parameters.Add(new MySqlParameter("@roomNumber", ClassRoutine.RoomNumber));
        }

        public bool Save(ClassRoutine ClassRoutine)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.SaveClassRoutine, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SetALLValues(ClassRoutine);
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
                    using (command = new MySqlCommand(Procedures.DeleteClassRoutine, connection))
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
