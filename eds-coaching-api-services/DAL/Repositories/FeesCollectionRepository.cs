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
    public class FeesCollectionRepository : ICRUDRepository<FeesCollection>
    {
        private MySqlConnection connection;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public List<FeesCollection> FindAll()
        {
            List<FeesCollection> FeesCollectionList = new List<FeesCollection>();
            try
            {
                using (connection = DBConnection.GetConnection())
                {
                    using (command = new MySqlCommand(Views.ALLFeesCollection, connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.Text;
                        using (reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FeesCollection FeesCollection = new FeesCollection
                                {
                                    Id              = reader.GetInt32("id"),
                                    StudentId       = reader.GetInt32("student_id"),
                                    Month           = reader.GetString("month"),
                                    Year            = reader.GetInt32("year"),
                                    Fees            = reader.GetFloat("fees"),
                                    Discount        = reader.GetFloat("discount"),
                                    Remain          = reader.GetFloat("remain"),
                                    InvoiceNumber   = reader.GetString("invoiceNumber"),
                                    Date            = reader.GetDateTime("date"),
                                    EntryById       = reader.GetInt32("EntryBy_id"),
                                    Status          = reader.GetString("status")
                                };
                                FeesCollectionList.Add(FeesCollection);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return FeesCollectionList;
        }

        public bool Update(FeesCollection FeesCollection)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.UpdateFeesCollection, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new MySqlParameter("@id", FeesCollection.Id));
                        SetALLParameter(FeesCollection);

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

        private void SetALLParameter(FeesCollection FeesCollection)
        {
            command.Parameters.Add(new MySqlParameter("@student_id", FeesCollection.StudentId));
            command.Parameters.Add(new MySqlParameter("@month", FeesCollection.Month));
            command.Parameters.Add(new MySqlParameter("@year", FeesCollection.Year));
            command.Parameters.Add(new MySqlParameter("@fees", FeesCollection.Fees));
            command.Parameters.Add(new MySqlParameter("@discount", FeesCollection.Discount));
            command.Parameters.Add(new MySqlParameter("@remain", FeesCollection.Remain));
            command.Parameters.Add(new MySqlParameter("@status", FeesCollection.Status));
            command.Parameters.Add(new MySqlParameter("@invoiceNumber", FeesCollection.InvoiceNumber));
            command.Parameters.Add(new MySqlParameter("@date", FeesCollection.Date));
            command.Parameters.Add(new MySqlParameter("@EntryBy_id", FeesCollection.EntryById));
        }

        public bool Save(FeesCollection FeesCollection)
        {
            int status = 0;
            using (connection = DBConnection.GetConnection())
            {
                try
                {
                    using (command = new MySqlCommand(Procedures.SaveFeesCollection, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SetALLParameter(FeesCollection);

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
                    using (command = new MySqlCommand(Procedures.DeleteFeesCollection, connection))
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
