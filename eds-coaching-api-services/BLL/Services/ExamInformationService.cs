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
    public class ExamInformationService : ICRUD<ExamInformation, int, string>
    {
        private readonly ExamInformationRepository ExamInformationRepository = new ExamInformationRepository();
        private readonly TracerService tracerService = new TracerService();

        public string DeleteById(int id, string currentUsername)
        {
            ExamInformation ExamInformation = FindById(id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticate(ExamInformation, staff);

            if (String.IsNullOrEmpty(message))
            {
                if (ExamInformationRepository.Delete(id))
                {
                    tracerService.Delete(DBTables.ExamInformation.ToString(), staff.Id, ExamInformation.Id, ExamInformation.InstitutionId);
                    return null;
                }
                else return Messages.issueInDatabase;
            }
            else return message;
        }

        public List<ExamInformation> FindAll(string currentUsername)
        {
            List<ExamInformation> ExamInformations = ExamInformationRepository.FindAll();
            Staff logedinStaff = Authorization.GetCurrentUser(currentUsername);
            if (logedinStaff != null)
            {
                if (Authorization.IsAdmin(logedinStaff))
                {
                    ExamInformations = ExamInformations.FindAll(ds => ds.InstitutionId == logedinStaff.Login.InstitutionProfile_id);
                }
            }
            return ExamInformations;
        }

        public ExamInformation FindById(int id, string currentUsername)
        {
            ExamInformation ExamInformation = FindAll(currentUsername).Find(d => d.Id == id);
            return ExamInformation;
        }

        public string Save(ExamInformation ExamInformation, string currentUsername)
        {
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticate(ExamInformation, staff);
            if (FindById(ExamInformation.Id, currentUsername) == null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    if (Authorization.IsAdmin(staff))
                    {
                        ExamInformation.InstitutionId = staff.Login.InstitutionProfile_id;
                    }
                    ExamInformation.EntryBy_id = staff.Id;

                    return ExamInformationRepository.Save(ExamInformation) ? null : Messages.issueInDatabase;
                }
                else return message;
            }
            else return Messages.IdExist;
        }

        public string Update(ExamInformation ExamInformation, string currentUsername)
        {
            ExamInformation foundedExamInformation = FindById(ExamInformation.Id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            ExamInformation.InstitutionId   = foundedExamInformation.InstitutionId;
            ExamInformation.EntryBy_id      = foundedExamInformation.EntryBy_id;

            string message = IsAuthonticate(ExamInformation, staff);
            if (foundedExamInformation != null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    if (IsAuthorizeUser(staff, ExamInformation))
                    {
                        if (ExamInformationRepository.Update(ExamInformation))
                        {
                            tracerService.Update(DBTables.ExamInformation.ToString(), staff.Id, ExamInformation.Id, ExamInformation.InstitutionId);
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

        private bool IsAuthorizeUser(Staff staff, ExamInformation ExamInformation)
        {
            if (Authorization.IsSuperAdmin(staff.Username))
                return true;
            else
            {
                if (Authorization.IsAdmin(staff))
                {
                    if (staff.Login.InstitutionProfile_id == ExamInformation.InstitutionId)
                        return true;
                }
                return false;
            }
        }

        private string IsAuthonticate(ExamInformation ExamInformation, Staff staff)
        {
            if (ExamInformation != null)
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
