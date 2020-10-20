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
    public class StudentAdmissionRepository : ICRUDRepository<StudentAdmission>
    {
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public List<StudentAdmission> FindAll()
        {
            List<StudentAdmission> StudentAdmissionList = new List<StudentAdmission>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new MySqlCommand(Views.ALLStudentAdmission, connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentAdmission StudentAdmission = new StudentAdmission
                                {
                                    Id = reader.GetInt32("id"),
                                    Student_id = reader.GetInt32("student_id"),
                                    SubjectManager = new SubjectManagerRepository().FindAll().Find(sjm => sjm.Id == reader.GetInt32("subjectManager_id")),
                                    MonthlyFees = reader.GetFloat("monthlyFees"),
                                    AdmissionDate = reader.GetDateTime("admissionDate"),
                                    EntryBy_id = reader.GetInt32("entryBy_id")
                                };
                                StudentAdmissionList.Add(StudentAdmission);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return StudentAdmissionList;
        }

        public bool Update(StudentAdmission StudentAdmission)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.UpdateStudentAdmission, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new MySqlParameter("@id", StudentAdmission.Id));
                        SetALLParameters(StudentAdmission);

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
      
        public bool Save(StudentAdmission StudentAdmission)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.SaveStudentAdmission, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SetALLParameters(StudentAdmission);
                        
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
                    using (command = new MySqlCommand(Procedures.DeleteStudentAdmission, connection))
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

        private void SetALLParameters(StudentAdmission StudentAdmission)
        {
            command.Parameters.Add(new MySqlParameter("@student_id", StudentAdmission.Student_id));
            command.Parameters.Add(new MySqlParameter("@subjectManager_id", StudentAdmission.SubjectManager.Id));
            command.Parameters.Add(new MySqlParameter("@monthlyFees", StudentAdmission.MonthlyFees));
            command.Parameters.Add(new MySqlParameter("@admissionDate", StudentAdmission.AdmissionDate));
            command.Parameters.Add(new MySqlParameter("@entryBy_id", StudentAdmission.EntryBy_id));
        }

    }
}
