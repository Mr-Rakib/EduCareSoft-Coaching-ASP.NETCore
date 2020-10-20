using eds_coaching_api_services.BLL.Interfaces;
using eds_coaching_api_services.DAL.Repositories;
using eds_coaching_api_services.Models;
using eds_coaching_api_services.Utility.Database.Tables;
using eds_coaching_api_services.Utility.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.BLL.Services
{
    public class BatchService : ICRUD<Batch, int, string>
    {
        private readonly BatchRepository BatchRepository = new BatchRepository();
        private readonly TracerService tracerService = new TracerService();

        public string DeleteById(int id, string currentUsername)
        {
            Batch Batch = FindById(id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticate(Batch, staff);

            if (String.IsNullOrEmpty(message))
            {
                if (BatchRepository.Delete(id))
                {
                    tracerService.Delete(DBTables.Batch.ToString(), staff.Id, Batch.Id, Batch.Institution_id);
                    return null;
                }
                else return Messages.issueInDatabase;
            }
            else return message;
        }

        public List<Batch> FindAll(string currentUsername)
        {
            List<Batch> Batchs = BatchRepository.FindAll();
            Staff logedinStaff = Authorization.GetCurrentUser(currentUsername);
            if (logedinStaff != null)
            {
                if (Authorization.IsAdmin(logedinStaff))
                {
                    Batchs = Batchs.FindAll(ds => ds.Institution_id == logedinStaff.Login.InstitutionProfile_id);
                }
            }
            return Batchs;
        }

        public Batch FindById(int id, string currentUsername)
        {
            Batch Batch = FindAll(currentUsername).Find(d => d.Id == id);
            return Batch;
        }

        public string Save(Batch Batch, string currentUsername)
        {
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticate(Batch, staff);
            if (FindById(Batch.Id, currentUsername) == null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    if (Authorization.IsAdmin(staff))
                    {
                        Batch.Institution_id = staff.Login.InstitutionProfile_id;
                    }
                    return BatchRepository.Save(Batch) ? null : Messages.issueInDatabase;
                }
                else return message;
            }
            else return Messages.IdExist;
        }

        public string Update(Batch Batch, string currentUsername)
        {
            Batch foundedBatch = FindById(Batch.Id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            Batch.Institution_id = foundedBatch.Institution_id;

            string message = IsAuthonticate(Batch, staff);
            if (foundedBatch != null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    if (IsAuthorizeUser(staff, Batch))
                    {
                        if (BatchRepository.Update(Batch))
                        {
                            tracerService.Update(DBTables.Batch.ToString(), staff.Id, Batch.Id, Batch.Institution_id);
                            return null;
                        }
                        else return Messages.issueInDatabase;
                    }
                    else return Messages.AccessDenied;
                }
                else return message;
            }
            else return Messages.NotFound;
        }

        private bool IsAuthorizeUser(Staff staff, Batch Batch)
        {
            if (Authorization.IsSuperAdmin(staff.Username))
                return true;
            else
            {
                if (Authorization.IsAdmin(staff))
                {
                    if (staff.Login.InstitutionProfile_id == Batch.Institution_id)
                        return true;
                }
                return false;
            }
        }

        private string IsAuthonticate(Batch Batch, Staff staff)
        {
            if (Batch != null)
            {
                if (staff != null)
                {
                    return null;
                }
                else return Messages.AccessDenied;
            }
            else return Messages.invalidField;
        }

    }
}
