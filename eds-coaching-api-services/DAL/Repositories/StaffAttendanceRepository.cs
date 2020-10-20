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
    public class StaffAttendanceRepository : ICRUDRepository<Attendance>
    {
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public List<Attendance> FindAll()
        {
            List<Attendance> AttendanceList = new List<Attendance>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new MySqlCommand(Views.ALLStaffAttendance, connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Attendance Attendance = new Attendance
                                {
                                    Id = reader.GetInt32("id"),
                                    User_id = reader.GetInt32("Staff_id"),
                                    TimeIn = reader.GetDateTime("timeIn"),
                                    TimeOut = ReadNullorDateTime("timeOut"),
                                    InputDevice = reader.GetString("inputDevice"),
                                    EntryBy_id = reader.GetInt32("entryBy_id"),

                                };
                                AttendanceList.Add(Attendance);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return AttendanceList;
        }



        public bool Update(Attendance Attendance)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.UpdateStaffAttendance, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new MySqlParameter("@id", Attendance.Id));
                        command.Parameters.Add(new MySqlParameter("@user_id", Attendance.User_id));
                        command.Parameters.Add(new MySqlParameter("@timeIn", Attendance.TimeIn));
                        command.Parameters.Add(new MySqlParameter("@timeOut", Attendance.TimeOut));
                        command.Parameters.Add(new MySqlParameter("@inputdevice", Attendance.InputDevice));
                        command.Parameters.Add(new MySqlParameter("@entryBy_id", Attendance.EntryBy_id));

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

        public bool Save(Attendance Attendance)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.SaveStaffAttendance, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new MySqlParameter("@user_id", Attendance.User_id));
                        command.Parameters.Add(new MySqlParameter("@timeIn", Attendance.TimeIn));
                        command.Parameters.Add(new MySqlParameter("@timeOut", Attendance.TimeOut));
                        command.Parameters.Add(new MySqlParameter("@inputdevice", Attendance.InputDevice));
                        command.Parameters.Add(new MySqlParameter("@entryBy_id", Attendance.EntryBy_id));

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
                    using (command = new MySqlCommand(Procedures.DeleteStaffAttendance, connection))
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

        private DateTime? ReadNullorDateTime(string column)
        {
            return (reader.IsDBNull(column)) ?
                (DateTime?)null : DateTime.Parse(reader.GetString(column));
        }
    }
}
