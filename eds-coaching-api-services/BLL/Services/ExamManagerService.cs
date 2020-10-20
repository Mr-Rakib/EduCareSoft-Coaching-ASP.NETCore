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
    public class ExamManagerService : IExamManager
    {
        //Load Data Access Layer
        private readonly ExamManagerRepository ExamManagerRepository = new ExamManagerRepository();
        private readonly TracerService tracerService = new TracerService();
        //Load Dependency Services
        private readonly ExamInformationService examInformationService = new ExamInformationService();
        private readonly ResultService examService = new ResultService();
        private readonly SubjectManagerService subjectManagerService = new SubjectManagerService();
        private readonly GradingSystemService gradingSystemService = new GradingSystemService();

        public string DeleteById(int id, string currentUsername)
        {
            ExamManager ExamManager = FindById(id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticate(ExamManager, staff);

            if (String.IsNullOrEmpty(message))
            {
                if (ExamManagerRepository.Delete(id))
                {
                    tracerService.Delete(DBTables.ExamManager.ToString(), staff.Id, ExamManager.Id, ExamManager.ExamInformation.InstitutionId);
                    return null;
                }
                else return Messages.issueInDatabase;
            }
            else return message;
        }

        public List<ExamManager> FindAll(string currentUsername)
        {
            List<ExamManager> ExamManagers = SetAllInformation(currentUsername);
            Login Login = Authorization.GetCurrentLoginUser(currentUsername);
            if (Login != null)
            {
                if (!Authorization.IsSuperAdmin(Login.Username))
                {
                    ExamManagers = ExamManagers.FindAll(ds => ds.ExamInformation.InstitutionId == Login.InstitutionProfile_id);
                }
            }
            return ExamManagers;
        }

        private List<ExamManager> SetAllInformation(string currentUsername)
        {
            List<ExamManager> examManagers = ExamManagerRepository.FindAll();
            foreach(var item in examManagers)
            {
                item.ExamInformation    = examInformationService.FindById(item.ExamInformation.Id, currentUsername);
                item.GradingSystem      = gradingSystemService.FindById(item.GradingSystem.Id, currentUsername);
                item.SubjectManager     = subjectManagerService.FindById(item.SubjectManager.Id, currentUsername);
            }
            return examManagers;
        }

        public ExamManager FindById(int id, string currentUsername)
        {
            ExamManager ExamManager = FindAll(currentUsername).Find(d => d.Id == id);
            return ExamManager;
        }

        public string Save(ExamManager ExamManager, string currentUsername)
        {
            ExamManager.ExamYear = ExamManager.ExamYear = ExamManager.ExamDate.Year;
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthorizeExamManager(ExamManager, staff);
            if (FindById(ExamManager.Id, currentUsername) == null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    if (!IsDuplicateExamManager(FindAll(currentUsername), ExamManager))
                    {
                        ExamManager.EntryBy_id = staff.Id;
                        if (ExamManagerRepository.Save(ExamManager)) 
                        {
                            SetUpAllStudentForExam(ExamManager, currentUsername);
                            return null;
                        }
                        else return Messages.issueInDatabase;
                    }
                    else return Messages.DuplicateExamManager;
                }
                else return message;
            }
            else return Messages.IdExist;
        }

        private void SetUpAllStudentForExam(ExamManager SetExamManager, string currentUsername)
        {
            List<StudentAdmission> NotifyAllExam = new StudentAdmissionService().FindAll(currentUsername).FindAll(st => st.SubjectManager.Id == SetExamManager.SubjectManager.Id);
            NotifyAllExam = NotifyAllExam.Where(std => Authorization.GetCurrentStudent(std.Student_id, currentUsername).EntryInformation.IsActive == Status.Enable).ToList();
            foreach (var items in NotifyAllExam)
            {
                Result result = new Result()
                {
                    ExamManager = FindAll(currentUsername).Find(ex => ex.SubjectManager.Id == SetExamManager.SubjectManager.Id && ex.ExamInformation.Id == SetExamManager.ExamInformation.Id && ex.ExamYear == SetExamManager.ExamYear),
                    StudentId = items.Student_id,
                    Marks       = 0,
                    Date        = DateTime.Now,
                    EntryBy_id  = Authorization.GetCurrentUser(currentUsername).Id
                };
                examService.Save(result, currentUsername);
            }
        }

        public string Update(ExamManager ExamManager, string currentUsername)
        {
            ExamManager foundedExamManager = FindById(ExamManager.Id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            ExamManager.EntryBy_id = foundedExamManager.EntryBy_id;
            ExamManager.ExamYear = ExamManager.ExamDate.Year;

            string message = IsAuthorizeExamManager(ExamManager, staff);
            if (foundedExamManager != null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    if (IsAuthorizeUser(staff, ExamManager))
                    {
                        if (ExamManagerRepository.Update(ExamManager))
                        {
                            tracerService.Update(DBTables.ExamManager.ToString(), staff.Id, ExamManager.Id, ExamManager.ExamInformation.InstitutionId);
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

        private bool IsDuplicateExamManager(List<ExamManager> List, ExamManager ExamManager)
        {
            List<ExamManager> ExamManagers = List.Where(exam => exam.ExamInformation.Id == ExamManager.ExamInformation.Id && exam.ExamYear == ExamManager.ExamYear).ToList();
            ExamManagers = ExamManagers.Where(exams => exams.SubjectManager.Id == ExamManager.SubjectManager.Id).ToList();
            return (ExamManagers.Count > 0) ? true : false;
        }

        private string IsAuthorizeExamManager(ExamManager ExamManager, Staff staff)
        {
            ExamManager.ExamInformation = examInformationService.FindById(ExamManager.ExamInformation.Id, staff.Username);
            ExamManager.SubjectManager = subjectManagerService.FindById(ExamManager.SubjectManager.Id, staff.Username);
            ExamManager.GradingSystem = gradingSystemService.FindById(ExamManager.GradingSystem.Id, staff.Username);
            string message = IsValidExamManager(ExamManager, staff); 
                
            if (String.IsNullOrEmpty(message))
            {
                return (IsAuthorizeUser(staff, ExamManager)) ? null : Messages.AccessDenied;
            }
            else return message;
        }

        private string IsValidExamManager(ExamManager ExamManager, Staff staff)
        {
            string message = IsAuthonticate(ExamManager, staff);
            if (string.IsNullOrEmpty(message))
            {
                if (ExamManager.ExamInformation != null)
                {
                    if (ExamManager.SubjectManager != null)
                    {
                        if (ExamManager.GradingSystem != null)
                        {
                            if (ExamManager.ExamInformation.InstitutionId == ExamManager.SubjectManager.Subject.Institution_id && ExamManager.ExamInformation.InstitutionId == ExamManager.GradingSystem.InstitutionId)
                            {
                                return null;
                            }
                            else return Messages.InvalidExamManagerInformation;
                        }
                        else return Messages.GragingSystemNotExist;
                    }
                    else return Messages.SubjectManagerNotExist;
                }
                else return Messages.ExamInformationNotExit;
            }
            else return message;
        }

        private bool IsAuthorizeUser(Staff staff, ExamManager ExamManager)
        {
            if (Authorization.IsSuperAdmin(staff.Username))
                return true;
            else
            {
                if (Authorization.IsAdmin(staff))
                {
                    if (staff.Login.InstitutionProfile_id == ExamManager.ExamInformation.InstitutionId)
                        return true;
                }
                return false;
            }
        }

        private string IsAuthonticate(ExamManager ExamManager, Staff staff)
        {
            if (ExamManager != null)
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
