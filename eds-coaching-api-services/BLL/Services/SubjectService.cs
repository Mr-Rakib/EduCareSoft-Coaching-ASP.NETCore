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
    public class SubjectService : ICRUD<Subject, int , string>
    {
        private readonly SubjectRepository SubjectRepository = new SubjectRepository();
        private readonly TracerService tracerService = new TracerService();

        public string DeleteById(int id, string currentUsername)
        {
            Subject Subject = FindById(id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticate(Subject, staff);

            if (String.IsNullOrEmpty(message))
            {
                if (SubjectRepository.Delete(id))
                {
                    tracerService.Delete(DBTables.Subject.ToString(), staff.Id, Subject.Id, Subject.Institution_id);
                    return null;
                }
                else return Messages.issueInDatabase;
            }
            else return message;
        }

        public List<Subject> FindAll(string currentUsername)
        {
            List<Subject> Subjects = SubjectRepository.FindAll();
            Staff logedinStaff = Authorization.GetCurrentUser(currentUsername);
            if (logedinStaff != null)
            {
                if (Authorization.IsAdmin(logedinStaff))
                {
                    Subjects = Subjects.FindAll(ds => ds.Institution_id == logedinStaff.Login.InstitutionProfile_id);
                }
            }
            return Subjects;
        }

        public Subject FindById(int id, string currentUsername)
        {
            Subject Subject = FindAll(currentUsername).Find(d => d.Id == id);
            return Subject;
        }

        public string Save(Subject Subject, string currentUsername)
        {
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticate(Subject, staff);
            if (FindById(Subject.Id, currentUsername) == null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    if (Authorization.IsAdmin(staff))
                    {
                        Subject.Institution_id = staff.Login.InstitutionProfile_id;
                    }
                    return SubjectRepository.Save(Subject) ? null : Messages.issueInDatabase;
                }
                else return message;
            }
            else return Messages.IdExist;
        }

        public string Update(Subject Subject, string currentUsername)
        {
            Subject foundedSubject = FindById(Subject.Id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            Subject.Institution_id = foundedSubject.Institution_id;

            string message = IsAuthonticate(Subject, staff);
            if (foundedSubject != null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    if (IsAuthorizeUser(staff, Subject))
                    {
                        if (SubjectRepository.Update(Subject))
                        {
                            tracerService.Update(DBTables.Subject.ToString(), staff.Id, Subject.Id, Subject.Institution_id);
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

        private bool IsAuthorizeUser(Staff staff, Subject Subject)
        {
            if (Authorization.IsSuperAdmin(staff.Username))
                return true;
            else
            {
                if (Authorization.IsAdmin(staff))
                {
                    if (staff.Login.InstitutionProfile_id == Subject.Institution_id)
                        return true;
                }
                return false;
            }
        }

        private string IsAuthonticate(Subject Subject, Staff staff)
        {
            if (Subject != null)
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