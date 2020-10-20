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
    public class GradingSystemService : ICRUD<GradingSystem, int , string>
    {
        private readonly GradingSystemRepository GradingSystemRepository = new GradingSystemRepository();
        private readonly TracerService tracerService = new TracerService();

        public string DeleteById(int id, string currentUsername)
        {
            GradingSystem GradingSystem = FindById(id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticate(GradingSystem, staff);

            if (String.IsNullOrEmpty(message))
            {
                if (GradingSystemRepository.Delete(id))
                {
                    List<AcademicGrading> academicGradings = new AcademicGradingService().FindAll(currentUsername).FindAll(ac => ac.GradingSystem.Id == GradingSystem.Id);
                    if(academicGradings != null)
                    {
                        academicGradings.ForEach(ac => tracerService.Delete(DBTables.AcademicGrading.ToString(), staff.Id, ac.Id, GradingSystem.InstitutionId));
                    }
                    tracerService.Delete(DBTables.GradingSystem.ToString(), staff.Id, GradingSystem.Id, GradingSystem.InstitutionId);
                    return null;
                }
                else return Messages.issueInDatabase;
            }
            else return message;
        }

        public List<GradingSystem> FindAll(string currentUsername)
        {
            List<GradingSystem> GradingSystems = GradingSystemRepository.FindAll();
            Staff logedinStaff = Authorization.GetCurrentUser(currentUsername);
            if (logedinStaff != null)
            {
                if (! Authorization.IsSuperAdmin(logedinStaff.Username))
                {
                    GradingSystems = GradingSystems.FindAll(ds => ds.InstitutionId == logedinStaff.Login.InstitutionProfile_id);
                }
            }
            return GradingSystems;
        }

        public GradingSystem FindById(int id, string currentUsername)
        {
            GradingSystem GradingSystem = FindAll(currentUsername).Find(d => d.Id == id);
            return GradingSystem;
        }

        public string Save(GradingSystem GradingSystem, string currentUsername)
        {
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticate(GradingSystem, staff);
            if (FindById(GradingSystem.Id, currentUsername) == null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    GradingSystem.InstitutionId = staff.Login.InstitutionProfile_id;
                    if (Authorization.IsAdmin(staff))
                    {
                        GradingSystem.InstitutionId = staff.Login.InstitutionProfile_id;
                    }
                    return GradingSystemRepository.Save(GradingSystem) ? null : Messages.issueInDatabase;
                }
                else return message;
            }
            else return Messages.IdExist;
        }

        public string Update(GradingSystem GradingSystem, string currentUsername)
        {
            GradingSystem foundedGradingSystem = FindById(GradingSystem.Id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticate(GradingSystem, staff);
            if (foundedGradingSystem != null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    if (Authorization.IsAdmin(staff))
                    {
                        if (staff.Login.InstitutionProfile_id == foundedGradingSystem.InstitutionId)
                            GradingSystem.InstitutionId = staff.Login.InstitutionProfile_id;
                        else return Messages.AccessDenied;
                    }
                    if (GradingSystemRepository.Update(GradingSystem))
                    {
                        tracerService.Update(DBTables.GradingSystem.ToString(), staff.Id, GradingSystem.Id, GradingSystem.InstitutionId);
                        return null;
                    }
                    else return Messages.issueInDatabase;
                }
                else return message;
            }
            else return Messages.NotFound;
        }

        public string IsAuthonticate(GradingSystem GradingSystem, Staff staff)
        {
            if (GradingSystem != null)
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
