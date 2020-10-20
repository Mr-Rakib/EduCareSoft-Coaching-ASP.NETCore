using eds_coaching_api_services.BLL.Interfaces;
using eds_coaching_api_services.DAL.Repositories;
using eds_coaching_api_services.Models;
using eds_coaching_api_services.Utility.Database.Tables;
using eds_coaching_api_services.Utility.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace eds_coaching_api_services.BLL.Services
{
    public class DesignationService : ICRUD<Designation, int, string>
    {
        private readonly DesignationRepository DesignationRepository = new DesignationRepository();
        private readonly TracerService tracerService = new TracerService();

        public string DeleteById(int id, string currentUsername)
        {
            Designation Designation = FindById(id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticate(Designation, staff);

            if (String.IsNullOrEmpty(message))
            {
                if (DesignationRepository.Delete(id))
                {
                    tracerService.Delete(DBTables.Designation.ToString(), staff.Id, Designation.Id, Designation.Institution_id);
                    return null;
                }
                else return Messages.issueInDatabase;
            }
            else return message;
        }

        public List<Designation> FindAll(string currentUsername)
        {
            List<Designation> Designations = DesignationRepository.FindAll();
            Staff logedinStaff = Authorization.GetCurrentUser(currentUsername);
            if (logedinStaff != null)
            {
                if (Authorization.IsAdmin(logedinStaff))
                {
                    Designations = Designations.FindAll(ds => ds.Institution_id == logedinStaff.Login.InstitutionProfile_id);
                }
            }
            return Designations;
        }

        public Designation FindById(int id, string currentUsername)
        {
            Designation Designation = FindAll(currentUsername).Find(d => d.Id == id);
            return Designation;
        }

        public string Save(Designation Designation, string currentUsername)
        {
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticate(Designation, staff);
            if (FindById(Designation.Id, currentUsername) == null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    if (Authorization.IsAdmin(staff))
                    {
                        Designation.Institution_id = staff.Login.InstitutionProfile_id;
                    }
                    return DesignationRepository.Save(Designation) ? null : Messages.issueInDatabase;
                }
                else return message;
            }
            else return Messages.IdExist;
        }

        public string Update(Designation Designation, string currentUsername)
        {
            Designation foundedDesignation = FindById(Designation.Id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            Designation.Institution_id = foundedDesignation.Institution_id;

            string message = IsAuthonticate(Designation, staff);
            if (foundedDesignation != null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    if (IsAuthorizeUser(staff, Designation))
                    {
                        if (DesignationRepository.Update(Designation))
                        {
                            tracerService.Update(DBTables.Designation.ToString(), staff.Id, Designation.Id, Designation.Institution_id);
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

        private bool IsAuthorizeUser(Staff staff, Designation Designation)
        {
            if (Authorization.IsSuperAdmin(staff.Username))
                return true;
            else
            {
                if (Authorization.IsAdmin(staff))
                {
                    if (staff.Login.InstitutionProfile_id == Designation.Institution_id)
                        return true;
                }
                return false;
            }
        }

        private string IsAuthonticate(Designation Designation, Staff staff)
        {
            if (Designation != null)
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
