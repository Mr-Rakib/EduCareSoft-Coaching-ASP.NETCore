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
    public class StaffRepository : ICRUDRepository<Staff>
    {
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public List<Staff> FindAll()
        {
            List<Staff> StaffList = new List<Staff>();
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.GetAllStaff, connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Staff Staff = new Staff();
                                GetAllStaffColumns(Staff);
                                StaffList.Add(Staff);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                }
            }
            return StaffList;
        }

        public bool Save(Staff Staff)
        {
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.SaveStaff, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SetAllParameters(Staff);
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

        public bool Update(Staff Staff)
        {
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.UpdateStaff, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SetAllParametersForUpdate(Staff);
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

        private void GetAllStaffColumns(Staff Staff)
        {
            //user informations
            Staff.Id            = reader.GetInt32("id");
            Staff.Username      = reader.GetString("username");
            Staff.NIDNumber     = reader.GetString("NIDNumber");
            Staff.JoiningDate   = reader.GetDateTime("joiningDate");

            Designation designation = new Designation()
            {
                Id              = reader.GetInt32("designation_id"),
                Name            = reader.GetString("designationName"),
                Institution_id  = reader.GetInt32("institution_id")
            };
            Staff.Designation = designation;
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
            Staff.PersonalInformation = personalInformation;
            
            //login informations
            Login login = new Login
            {
                Username                = reader.GetString("username"),
                //Password              = reader.GetString("password"),
                UserRole                = reader.GetString("userRole"),
                LastLoginDate           = ReadNullorDateTime("lastLoginDate"),
                IsLoginActive           = reader.GetInt32("isLoginActive"),
                InstitutionProfile_id   = reader.GetInt32("institutionProfile_id")
            };
            Staff.Login = login;
            //entry informations
            EntryInformation entryInformation = new EntryInformation
            {
                Id          = reader.GetInt32("entryInformation_id"),
                EntryBy_id  = reader.GetInt32("entryBy_id"),
                EntryDate   = reader.GetDateTime("entryDate"),
                IsActive    = reader.GetInt32("isActive")
            };
            Staff.EntryInformation = entryInformation;
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

        private void SetAllParameters(Staff Staff)
        {
            //Staff infromations
            command.Parameters.Add(new MySqlParameter("@username", Staff.Username));
            command.Parameters.Add(new MySqlParameter("@NIDNumber", Staff.NIDNumber));
            command.Parameters.Add(new MySqlParameter("@joiningDate", Staff.JoiningDate));
            //Designation information
            command.Parameters.Add(new MySqlParameter("@designation_id", Staff.Designation.Id));
            //personal informations
            command.Parameters.Add(new MySqlParameter("@fullName", Staff.PersonalInformation.FullName));
            command.Parameters.Add(new MySqlParameter("@fathersName", Staff.PersonalInformation.FathersName));
            command.Parameters.Add(new MySqlParameter("@mothersName", Staff.PersonalInformation.MothersName));
            command.Parameters.Add(new MySqlParameter("@gender", Staff.PersonalInformation.Gender));
            command.Parameters.Add(new MySqlParameter("@dateOfBirth", Staff.PersonalInformation.DateOfBirth));
            command.Parameters.Add(new MySqlParameter("@image", Staff.PersonalInformation.Image));
            //contact informations
            command.Parameters.Add(new MySqlParameter("@contact1", Staff.PersonalInformation.UserContactInformation.Contact1));
            command.Parameters.Add(new MySqlParameter("@contact2", Staff.PersonalInformation.UserContactInformation.Contact2));
            command.Parameters.Add(new MySqlParameter("@email", Staff.PersonalInformation.UserContactInformation.Email));
            command.Parameters.Add(new MySqlParameter("@address", Staff.PersonalInformation.UserContactInformation.Address));
            //login informations 
            command.Parameters.Add(new MySqlParameter("@password", Staff.Login.Password));
            command.Parameters.Add(new MySqlParameter("@userRole", Staff.Login.UserRole));
            command.Parameters.Add(new MySqlParameter("@lastLoginDate", Staff.Login.LastLoginDate));
            command.Parameters.Add(new MySqlParameter("@isLoginActive", Staff.Login.IsLoginActive));
            command.Parameters.Add(new MySqlParameter("@institutionProfile_id", Staff.Login.InstitutionProfile_id));
            //entry informations
            command.Parameters.Add(new MySqlParameter("@entryBy_id", Staff.EntryInformation.EntryBy_id));
            command.Parameters.Add(new MySqlParameter("@entryDate", Staff.EntryInformation.EntryDate));
            command.Parameters.Add(new MySqlParameter("@isActive", Staff.EntryInformation.IsActive));
        }

        private void SetAllParametersForUpdate(Staff Staff)
        {
            SetAllParameters(Staff);
            //additional information 
            command.Parameters.Add(new MySqlParameter("@id", Staff.Id));
            command.Parameters.Add(new MySqlParameter("@contactInformation_id", Staff.PersonalInformation.UserContactInformation.Id));
            command.Parameters.Add(new MySqlParameter("@entryInformation_id", Staff.EntryInformation.Id));
        }

    }
}
