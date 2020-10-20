using eds_coaching_api_services.DAL.Interface;
using eds_coaching_api_services.Models;
using eds_coaching_api_services.Utility.Database;
using eds_coaching_api_services.Utility;
using eds_coaching_api_services.Utility.Dependencies;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.DAL.Repositories
{
    public class StudentRepository : ICRUDRepository<Student>
    {
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public List<Student> FindAll()
        {
            List<Student> studentList = new List<Student>();
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.GetAllStudent, connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Student student = new Student();
                                GetAllStudentColumns(student);
                                studentList.Add(student);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                }
            }
            return studentList;
        }

        public bool Save(Student student)
        {
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.SaveStudent, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SetAllParameters(student);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                    return false;
                }
            }
            return true;
        }

        public bool Update(Student student)
        {
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.UpdateStudent, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SetAllParametersForUpdate(student);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                    return false;
                }
            }
            return true;
        }
        
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        private void GetAllStudentColumns(Student student)
        {
            //user informations
            student.Id          = reader.GetInt32("id");
            student.Username    = reader.GetString("username");
            //personal informations
            PersonalInformation personalInformation = new PersonalInformation
            {
                Username    = reader.GetString("username"),
                FullName    = reader.GetString("fullName"),
                FathersName = reader.GetString("fathersName"),
                MothersName = ReadNullorString("mothersName"),
                Gender      = reader.GetString("gender"),
                DateOfBirth = DateTime.Parse(reader.GetString("dateOfBirth")),
                Image       = ReadNullorString("image")
            };
            //contact informations
            ContactInformation contactInformation = new ContactInformation
            {
                Id          = reader.GetInt32("userContactInformation_id"),
                Contact1    = ReadNullorString("contact1"),
                Contact2    = ReadNullorString("contact2"),
                Email       = ReadNullorString("email"),
                Address     = reader.GetString("address")
            };

            personalInformation.UserContactInformation = contactInformation;
            student.PersonalInformation = personalInformation;
            //guardian informations
            GuardinaInformation guardinaInformation = new GuardinaInformation
            {
                FathersContact  = reader.GetString("fathersContact"),
                FathersImage    = ReadNullorString("fathersImage"),
                MothersContact  = ReadNullorString("mothersContact"),
                MothersImage    = ReadNullorString("mothersImage"),
                GuardianName    = reader.GetString("guardianName"),
                GuardianContact = reader.GetString("guardianContact"),
                GuardianImage   = ReadNullorString("guardianImage"),
                GuardianAddress = reader.GetString("guardianAddress")
            };
            student.GuardianInformation = guardinaInformation;
            //login informations
            Login login = new Login
            {
                Username                = reader.GetString("username"),
                Password                = reader.GetString("password"),
                UserRole                = reader.GetString("userRole"),
                LastLoginDate           = ReadNullorDateTime("lastLoginDate"),
                IsLoginActive           = reader.GetInt32("isLoginActive"),
                InstitutionProfile_id   = reader.GetInt32("institutionProfile_id")
            };
            student.Login = login;
            //entry informations
            EntryInformation entryInformation = new EntryInformation
            {
                Id          = reader.GetInt32("entryInformation_id"),
                EntryBy_id  = reader.GetInt32("entryBy_id"),
                EntryDate   = reader.GetDateTime("entryDate"),
                IsActive    = reader.GetInt32("isActive")
            };
            student.EntryInformation = entryInformation;
        }

        private string ReadNullorString(string column)
        {
            return (reader.IsDBNull(column)) ? null : reader.GetString(column);
        }
        
        private DateTime? ReadNullorDateTime(string column)
        {
            return (reader.IsDBNull(column)) ? 
                (DateTime?)null : DateTime.Parse(reader.GetString(column));
        }

        private void SetAllParameters(Student student)
        {
            //student infromations
            command.Parameters.Add(new MySqlParameter("@username", student.Username));
            //personal informations
            command.Parameters.Add(new MySqlParameter("@fullName", student.PersonalInformation.FullName));
            command.Parameters.Add(new MySqlParameter("@fathersName", student.PersonalInformation.FathersName));
            command.Parameters.Add(new MySqlParameter("@mothersName", student.PersonalInformation.MothersName));
            command.Parameters.Add(new MySqlParameter("@gender", student.PersonalInformation.Gender));
            command.Parameters.Add(new MySqlParameter("@dateOfBirth", student.PersonalInformation.DateOfBirth));
            command.Parameters.Add(new MySqlParameter("@image", student.PersonalInformation.Image));
            //contact informations
            command.Parameters.Add(new MySqlParameter("@contact1", student.PersonalInformation.UserContactInformation.Contact1));
            command.Parameters.Add(new MySqlParameter("@contact2", student.PersonalInformation.UserContactInformation.Contact2));
            command.Parameters.Add(new MySqlParameter("@email", student.PersonalInformation.UserContactInformation.Email));
            command.Parameters.Add(new MySqlParameter("@address", student.PersonalInformation.UserContactInformation.Address));
            //guardian informations
            command.Parameters.Add(new MySqlParameter("@fathersContact", student.GuardianInformation.FathersContact));
            command.Parameters.Add(new MySqlParameter("@fathersImage", student.GuardianInformation.FathersImage));
            command.Parameters.Add(new MySqlParameter("@mothersContact", student.GuardianInformation.MothersContact));
            command.Parameters.Add(new MySqlParameter("@mothersImage", student.GuardianInformation.MothersImage));
            command.Parameters.Add(new MySqlParameter("@guardianName", student.GuardianInformation.GuardianName));
            command.Parameters.Add(new MySqlParameter("@guardianContact", student.GuardianInformation.GuardianContact));
            command.Parameters.Add(new MySqlParameter("@guardianImage", student.GuardianInformation.GuardianImage));
            command.Parameters.Add(new MySqlParameter("@guardianAddress", student.GuardianInformation.GuardianAddress));
            //login informations 
            command.Parameters.Add(new MySqlParameter("@password", student.Login.Password));
            command.Parameters.Add(new MySqlParameter("@userRole", student.Login.UserRole));
            command.Parameters.Add(new MySqlParameter("@lastLoginDate", student.Login.LastLoginDate));
            command.Parameters.Add(new MySqlParameter("@isLoginActive", student.Login.IsLoginActive));
            command.Parameters.Add(new MySqlParameter("@institutionProfile_id", student.Login.InstitutionProfile_id));

            //entry informations
            command.Parameters.Add(new MySqlParameter("@entryBy_id", student.EntryInformation.EntryBy_id));
            command.Parameters.Add(new MySqlParameter("@entryDate", student.EntryInformation.EntryDate));
            command.Parameters.Add(new MySqlParameter("@isActive", student.EntryInformation.IsActive));
        }

        private void SetAllParametersForUpdate(Student student)
        {
            SetAllParameters(student);
            //additional information 
            command.Parameters.Add(new MySqlParameter("@id", student.Id));
            command.Parameters.Add(new MySqlParameter("@contactInformation_id", student.PersonalInformation.UserContactInformation.Id));
            command.Parameters.Add(new MySqlParameter("@entryInformation_id", student.EntryInformation.Id));
        }

    }
}
