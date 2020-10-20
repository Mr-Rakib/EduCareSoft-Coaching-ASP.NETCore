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
    public class SubjectManagerService : ICRUD<SubjectManager, int, string>
    {
        private readonly SubjectManagerRepository SubjectManagerRepository = new SubjectManagerRepository();
        private readonly SubjectService SubjectManager = new SubjectService();
        private readonly TracerService tracerService = new TracerService();
        private readonly ClassService classService = new ClassService();
        private readonly BatchService batchService = new BatchService();

        public string DeleteById(int id, string currentUsername)
        {
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            SubjectManager SubjectManager = FindById(id, currentUsername);
            string message = IsAuthonticate(SubjectManager, currentUsername);

            if (String.IsNullOrEmpty(message))
            {
                if (SubjectManagerRepository.Delete(id))
                {
                    tracerService.Delete(DBTables.SubjectManager.ToString(), staff.Id, SubjectManager.Id, SubjectManager.Subject.Institution_id);
                    return null;
                }
                else return Messages.issueInDatabase;
            }
            else return message;
        }
        
        public List<SubjectManager> FindAll(string currentUsername)
        {
            List<SubjectManager> SubjectManagers = SubjectManagerRepository.FindAll();
            Staff logedinStaff = Authorization.GetCurrentUser(currentUsername);
            if (logedinStaff != null)
            {
                if (Authorization.IsAdmin(logedinStaff))
                {
                    SubjectManagers = SubjectManagers.FindAll
                        (ds => 
                            ds.Subject.Institution_id == logedinStaff.Login.InstitutionProfile_id &&
                            ds.Class.Institution_id   == logedinStaff.Login.InstitutionProfile_id &&
                            ds.Batch.Institution_id   == logedinStaff.Login.InstitutionProfile_id 
                        );
                }
            }
            return SubjectManagers;
        }
        
        public SubjectManager FindById(int id, string currentUsername)
        {
            SubjectManager SubjectManager = FindAll(currentUsername).Find(d => d.Id == id);
            return SubjectManager;
        }
        
        public string Save(SubjectManager SubjectManager, string currentUsername)
        {
            SetAllValue(SubjectManager, currentUsername);
            string message = IsAuthonticateSubjcetManager(SubjectManager, currentUsername);
            if (FindById(SubjectManager.Id, currentUsername) == null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    return SubjectManagerRepository.Save(SubjectManager) ? null : Messages.issueInDatabase;
                }
                else return message;
            }
            else return Messages.IdExist;
        }
        
        public string Update(SubjectManager SubjectManager, string currentUsername)
        {
            SetAllValue(SubjectManager, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            SubjectManager foundedSubjectManager = FindById(SubjectManager.Id, currentUsername);
            string message = IsAuthonticate(SubjectManager, currentUsername);
            if (foundedSubjectManager != null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    if (SubjectManagerRepository.Update(SubjectManager))
                    {
                        tracerService.Update(DBTables.SubjectManager.ToString(), staff.Id, SubjectManager.Id, SubjectManager.Subject.Institution_id);
                        return null;
                    }
                    else return Messages.issueInDatabase;
                }
                else return message;
            }
            else return Messages.NotFound;
        }

        private string IsAuthonticate(SubjectManager SubjectManager, string currentUsername)
        {
            if (SubjectManager != null)
            {
                Staff staff = Authorization.GetCurrentUser(currentUsername);
                if (staff != null)
                {
                    return null;
                }
                else return Messages.AccessDenied;
            }
            else return Messages.invalidField;
        }
        private string IsAuthonticateSubjcetManager(SubjectManager SubjectManager, string currentUsername)
        {
            string message = IsAuthonticate(SubjectManager,currentUsername);
            if (String.IsNullOrEmpty(message))
            {
                if (IsValidSubjectManager(SubjectManager))
                {
                    return null;
                }
                else return Messages.InvalidSubjectManager;
            }
            else return message;
        }
        private bool IsValidSubjectManager(SubjectManager subjectManager)
        {
            return (IsNullSubjectManage(subjectManager))
            ?  false :
            (
                subjectManager.Subject.Institution_id == subjectManager.Class.Institution_id &&
                subjectManager.Class.Institution_id == subjectManager.Class.Institution_id &&
                subjectManager.Batch.Institution_id == subjectManager.Subject.Institution_id
            ) 
            ? true : false ;
        }
        private bool IsNullSubjectManage(SubjectManager SubjectManager)
        {
            return (SubjectManager.Subject == null || SubjectManager.Class == null || SubjectManager.Batch == null)
                ? true
                : false;
        }
        private void SetAllValue(SubjectManager subjectManager, string currentUsername)
        {
            subjectManager.Subject  = SubjectManager.FindById(subjectManager.Subject.Id, currentUsername);
            subjectManager.Class    = classService.FindById(subjectManager.Class.Id, currentUsername);
            subjectManager.Batch    = batchService.FindById(subjectManager.Batch.Id, currentUsername);
            subjectManager.Session  = subjectManager.Session == 0 ? DateTime.Now.Year : subjectManager.Session;
        }
    }
}
