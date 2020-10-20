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
    public class InstitutionRepository : ICRUDRepository<Institution>
    {
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public List<Institution> FindAll()
        {
            List<Institution> institutionList = new List<Institution>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new MySqlCommand(Procedures.GetAllInstitution, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Institution institution = new Institution();
                                GetAllInstitutionColumns(institution);
                                institutionList.Add(institution);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return institutionList;
        }

        public bool Save(Institution institution)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.SaveInstitution, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SetAllParameters(institution);
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

        public bool Update(Institution institution)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.UpdateInstitution, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add(new MySqlParameter("@id", institution.Id));
                        SetAllParameters(institution);
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

        private void SetAllParameters(Institution institution)
        {
            command.Parameters.Add(new MySqlParameter("@name", institution.Name));
            command.Parameters.Add(new MySqlParameter("@logo", institution.Logo));
            command.Parameters.Add(new MySqlParameter("@registrationNumber", institution.RegistrationNumber));
            command.Parameters.Add(new MySqlParameter("@registrationDate", institution.RegistrationDate));
            command.Parameters.Add(new MySqlParameter("@contactInformation_id", institution.ContactInformation.Id));

            command.Parameters.Add(new MySqlParameter("@contact1", institution.ContactInformation.Contact1));
            command.Parameters.Add(new MySqlParameter("@contact2", institution.ContactInformation.Contact2));
            command.Parameters.Add(new MySqlParameter("@email", institution.ContactInformation.Email));
            command.Parameters.Add(new MySqlParameter("@address", institution.ContactInformation.Address));
            
            command.Parameters.Add(new MySqlParameter("@isActive", institution.IsActive));
        }
        private void GetAllInstitutionColumns(Institution institution)
        {
            institution.Id                  = reader.GetInt32("id");
            institution.Name                = reader.GetString("name");
            institution.Logo                = ReadNullorString("logo");
            institution.RegistrationNumber  = ReadNullorString("registrationNumber");
            institution.RegistrationDate    = reader.GetDateTime("registrationDate");

            ContactInformation contactInformation = new ContactInformation
            {
                Id = reader.GetInt32("userContactInformation_id"),
                Contact1 = ReadNullorString("contact1"),
                Contact2 = ReadNullorString("contact2"),
                Email = ReadNullorString("email"),
                Address = reader.GetString("address")
            };

            institution.ContactInformation  = contactInformation;
            institution.IsActive            = reader.GetInt32("isActive");
        }

        private string ReadNullorString(string column)
        {
            return (reader.IsDBNull(column)) ? null : reader.GetString(column);
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
