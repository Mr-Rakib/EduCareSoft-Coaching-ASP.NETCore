using eds_coaching_api_services.BLL.Interfaces;
using eds_coaching_api_services.DAL;
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
    public class ClassService : ICRUD<Class, int, string>
    {
        private readonly ClassRepository ClassRepository = new ClassRepository();
        private readonly TracerService tracerService = new TracerService();

        public string DeleteById(int id, string currentUsername)
        {
            Class Class = FindById(id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticate(Class, staff);

            if (String.IsNullOrEmpty(message))
            {
                if (ClassRepository.Delete(id))
                {
                    tracerService.Delete(DBTables.Class.ToString(), staff.Id, Class.Id, Class.Institution_id);
                    return null;
                }
                else return Messages.issueInDatabase;
            }
            else return message;
        }

        public List<Class> FindAll(string currentUsername)
        {
            List<Class> Classs = ClassRepository.FindAll();
            Staff logedinStaff = Authorization.GetCurrentUser(currentUsername);
            if (logedinStaff != null)
            {
                if (Authorization.IsAdmin(logedinStaff))
                {
                    Classs = Classs.FindAll(ds => ds.Institution_id == logedinStaff.Login.InstitutionProfile_id);
                }
            }
            return Classs;
        }

        public Class FindById(int id, string currentUsername)
        {
            Class Class = FindAll(currentUsername).Find(d => d.Id == id);
            return Class;
        }

        public string Save(Class Class, string currentUsername)
        {
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticate(Class, staff);
            if (FindById(Class.Id, currentUsername) == null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    if (Authorization.IsAdmin(staff))
                    {
                        Class.Institution_id = staff.Login.InstitutionProfile_id;
                    }
                    return ClassRepository.Save(Class) ? null : Messages.issueInDatabase;
                }
                else return message;
            }
            else return Messages.IdExist;
        }

        public string Update(Class Class, string currentUsername)
        {
            Class foundedClass = FindById(Class.Id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            Class.Institution_id = foundedClass.Institution_id;

            string message = IsAuthonticate(Class, staff);
            if (foundedClass != null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    if (IsAuthorizeUser(staff, Class))
                    {
                        if (ClassRepository.Update(Class))
                        {
                            tracerService.Update(DBTables.Class.ToString(), staff.Id, Class.Id, Class.Institution_id);
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

        private bool IsAuthorizeUser(Staff staff, Class Class)
        {
            if (Authorization.IsSuperAdmin(staff.Username))
                return true;
            else
            {
                if (Authorization.IsAdmin(staff))
                {
                    if (staff.Login.InstitutionProfile_id == Class.Institution_id)
                        return true;
                }
                return false;
            }
        }

        private string IsAuthonticate(Class Class, Staff staff)
        {
            if (Class != null)
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
