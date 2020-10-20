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
    public class AcademicGradingService : IAcademicGrading
    {
        private readonly AcademicGradingRepository AcademicGradingRepository = new AcademicGradingRepository();
        private readonly GradingSystemService gradingSystemService = new GradingSystemService();
        private readonly TracerService tracerService = new TracerService();

        public string DeleteById(int id, string currentUsername)
        {
            AcademicGrading AcademicGrading = FindById(id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticate(AcademicGrading, staff);

            if (String.IsNullOrEmpty(message))
            {
                if (AcademicGradingRepository.Delete(id))
                {
                    tracerService.Delete(DBTables.AcademicGrading.ToString(), staff.Id, AcademicGrading.Id, AcademicGrading.GradingSystem.InstitutionId);
                    return null;
                }
                else return Messages.issueInDatabase;
            }
            else return message;
        }

        public List<AcademicGrading> FindAll(string currentUsername)
        {
            List<AcademicGrading> AcademicGradings = AcademicGradingRepository.FindAll();
            AcademicGradings.ForEach(gr => gr.GradingSystem =  gradingSystemService.FindById(gr.GradingSystem.Id, currentUsername));

            Staff logedinStaff = Authorization.GetCurrentUser(currentUsername);
            if (logedinStaff != null)
            {
                if (! Authorization.IsSuperAdmin(currentUsername))
                {
                    AcademicGradings = AcademicGradings.FindAll(ds => ds.GradingSystem.InstitutionId == logedinStaff.Login.InstitutionProfile_id);
                }
            }
            return AcademicGradings;
        }

        public List<AcademicGrading> FindByGradingSystemName(string SystemName, string currentUsername)
        {
            List<AcademicGrading> AcademicGrading = FindAll(currentUsername).FindAll(d => d.GradingSystem.SystemName == SystemName);
            return AcademicGrading;
        }

        public AcademicGrading FindById(int id, string currentUsername)
        {
            AcademicGrading AcademicGrading = FindAll(currentUsername).Find(d => d.Id == id);
            return AcademicGrading;
        }

        public string Save(AcademicGrading AcademicGrading, string currentUsername)
        {
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticateGrading(AcademicGrading, staff);
            if (FindById(AcademicGrading.Id, currentUsername) == null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    AcademicGrading.EntryBy_id = staff.Id;
                    return AcademicGradingRepository.Save(AcademicGrading) ? null : Messages.issueInDatabase;
                }
                else return message;
            }
            else return Messages.IdExist;
        }

        private string IsAuthonticateGrading(AcademicGrading academicGrading, Staff staff)
        {
            GradingSystem gradingSystem = gradingSystemService.FindById(academicGrading.GradingSystem.Id, staff.Username);
            string messages = IsAuthonticate(academicGrading, staff);
            if (gradingSystem != null)
            {
                if (String.IsNullOrEmpty(messages))
                {
                    if (IsValidAcademicGrading(academicGrading, staff))
                    {
                        return null;
                    }
                    else return Messages.DuplicateGrade;
                }
                else return Messages.NotFound;
            }
            return messages;
        }

        public string Update(AcademicGrading AcademicGrading, string currentUsername)
        {
            AcademicGrading foundedAcademicGrading = FindById(AcademicGrading.Id, currentUsername);
            Staff staff                     = Authorization.GetCurrentUser(currentUsername);
            AcademicGrading.GradingSystem   = foundedAcademicGrading.GradingSystem;
            string message                  = IsAuthonticateGrading(AcademicGrading, staff);
            if (foundedAcademicGrading != null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    if (AcademicGradingRepository.Update(AcademicGrading))
                    {
                        tracerService.Update(DBTables.AcademicGrading.ToString(), staff.Id, AcademicGrading.Id, AcademicGrading.GradingSystem.InstitutionId);
                        return null;
                    }
                    else return Messages.issueInDatabase;
                }
                else return message;
            }
            else return Messages.NotFound;
        }

        private string IsAuthonticate(AcademicGrading AcademicGrading, Staff staff)
        {
            if (AcademicGrading != null)
            {
                if (staff != null)
                {
                    return null;
                }
                else return Messages.AccessDenied;
            }
            else return Messages.invalidField;
        }

        private bool IsValidAcademicGrading(AcademicGrading academicGrading, Staff staff)
        {
            AcademicGrading FoundedAademicGrading = FindAll(staff.Username).Find(ac => ac.GradingSystem.SystemName == academicGrading.GradingSystem.SystemName &&
                                                                                       ac.GradeName == academicGrading.GradeName &&
                                                                                       ac.GradingSystem.InstitutionId == academicGrading.GradingSystem.InstitutionId);
            return FoundedAademicGrading != null ? true : false;

        }
        
    }
}
