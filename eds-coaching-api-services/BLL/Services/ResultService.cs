using eds_coaching_api_services.DAL.Repositories;
using eds_coaching_api_services.Models;
using eds_coaching_api_services.Utility.Database.Tables;
using eds_coaching_api_services.Utility.Dependencies;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.BLL.Services
{
    public class ResultService
    {
        private readonly ResultRepository ResultRepository = new ResultRepository();
        private readonly TracerService tracerService = new TracerService();

        public string DeleteById(int id, string currentUsername)
        {
            Result Result = FindById(id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticate(Result, staff);

            if (String.IsNullOrEmpty(message))
            {
                if (ResultRepository.Delete(id))
                {
                    tracerService.Delete(DBTables.Result.ToString(), staff.Id, Result.Id, Authorization.GetInstitution(Result.StudentId, currentUsername));
                    return null;
                }
                else return Messages.issueInDatabase;
            }
            else return message;
        }

        public List<Result> FindAll(string currentUsername)
        {
            List<Result> Results = ResultRepository.FindAll();
            foreach(var items in Results)
            {
                items.ExamManager = new ExamManagerService().FindById(items.ExamManager.Id, currentUsername);
            }

            Staff logedinStaff = Authorization.GetCurrentUser(currentUsername);
            if (logedinStaff != null)
            {
                if (!Authorization.IsSuperAdmin(currentUsername))
                {
                    Results = Results.FindAll(ds => Authorization.GetInstitution(ds.StudentId, currentUsername) == logedinStaff.Login.InstitutionProfile_id);
                }
                if (Authorization.IsTeacher(currentUsername))
                {
                    Staff staff = Authorization.GetCurrentUser(currentUsername);
                    Results = Results.FindAll(ds => FindStaffsSubjectManager(ds.ExamManager, currentUsername) == ds.ExamManager.SubjectManager.Id);
                }

            }
            return Results;
        }

        private int FindStaffsSubjectManager(ExamManager examManager, string currentUsername)
        {
            Staff Staff = Authorization.GetCurrentUser(currentUsername);
            List<ClassRoutine> ClassRoutines  = new ClassRoutineService().FindByStaffId(Staff.Id, currentUsername);

            if(Staff.EntryInformation.IsActive == Status.Enable)
            {
                var existence = ClassRoutines.Find( sub => sub.SubjectManager.Id == examManager.SubjectManager.Id);
                if(existence != null)
                {
                    return examManager.SubjectManager.Id;
                }
            }
            return 0;
        }

        public Result FindById(int id, string currentUsername)
        {
            Result Result = FindAll(currentUsername).Find(d => d.Id == id);
            return Result;
        }

        public string Save(Result Result, string currentUsername)
        {
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticateResult(Result, staff);
            if (FindById(Result.Id, currentUsername) == null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    Result.Date         = DateTime.Now;
                    Result.EntryBy_id   = staff.Id;
                    return ResultRepository.Save(Result) ? null : Messages.issueInDatabase;
                }
                else return message;
            }
            else return Messages.IdExist;
        }

        public string Update(Result Result, string currentUsername)
        {
            Result foundedResult = FindById(Result.Id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            Result.ExamManager = foundedResult.ExamManager;

            string message = IsAuthonticateResult(Result, staff);
            if (foundedResult != null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    Result.Date = DateTime.Now;
                    Result.EntryBy_id = staff.Id;
                    if (ResultRepository.Update(Result))
                    {
                        tracerService.Update(DBTables.Result.ToString(), staff.Id, Result.Id, Authorization.GetInstitution(Result.StudentId, currentUsername));
                        return null;
                    }
                    else return Messages.issueInDatabase;
                }
                else return message;
            }
            else return Messages.NotFound;
        }
        
        public List<ResultGrade> CalculateGPA(int examInformationId, int studentId, int year, string username)
        {
            List<ResultGrade> ResultGrades = new List<ResultGrade>();
            year = (year > 0) ? year : DateTime.Now.Year;
            if (examInformationId > 0)
            {
                List<Result> AllResults = FindAll(username).FindAll(exam => exam.ExamManager.ExamInformation.Id == examInformationId);
                AllResults = AllResults.FindAll(exam => exam.ExamManager.ExamYear == year);
                AllResults = (studentId > 0) ? AllResults.FindAll( exam => exam.StudentId == studentId) : AllResults;

                List<Result> GetDistinctListofStudent = AllResults.DistinctBy(student => student.StudentId).ToList();
                foreach (var items in GetDistinctListofStudent)
                {
                    ResultGrade ResultGrade = GPACalculation(AllResults, items.StudentId, username);
                    ResultGrades.Add(ResultGrade);
                }

            }return ResultGrades;

        }

        private ResultGrade GPACalculation(List<Result> allResults, int studentId, string currentUsername)
        {
            ResultGrade resultGrade = new ResultGrade();
            allResults = allResults.FindAll(std => std.StudentId == studentId);
            if (allResults.Count > 0)
            {
                resultGrade.StudentId = studentId;
                resultGrade.Results = allResults;
                resultGrade.GPA = CalculateIndividualsGrade(allResults, currentUsername);
            }
            else resultGrade = null;
            return resultGrade;
        }

        private float CalculateIndividualsGrade(List<Result> allResults, string currentUsername)
        {
            float StudentGPA = 0;
            float TotalGPA = 0;
            float TotalSubject = ((allResults.Count > 0) ? allResults.Count : 1);
            if (allResults.Count > 0)
            {

                List<AcademicGrading> academicGradings = new AcademicGradingService().FindByGradingSystemName(allResults[0].ExamManager.GradingSystem.SystemName, currentUsername);
                foreach (var items in allResults)
                {
                    var x = academicGradings.Find(mark => items.Marks >= mark.PercentageFrom && items.Marks <= mark.PercentageTo).Gpa;
                    TotalGPA += x;
                }
            }
            StudentGPA = TotalGPA / TotalSubject;
            return StudentGPA;
        }

        private string IsAuthonticateResult(Result result, Staff staff)
        {
            string message = IsAuthonticate(result, staff);
            if (string.IsNullOrEmpty(message))
            {
                List<Result> Foundedresults = FindAll(staff.Username).FindAll(fr => fr.ExamManager.Id == result.ExamManager.Id && fr.StudentId == result.StudentId);
                if (Foundedresults.Count <= 0)
                    return null;
                else return Messages.DuplicateResult;
            }
            else return message;
        }


        private string IsAuthonticate(Result Result, Staff staff)
        {
            if (Result != null)
            {
                if (staff != null)
                {
                    return null;
                }
                else return Messages.AccessDenied;
            }
            else return Messages.invalidField;
        }

        private void SetUpAllStudentForExam(ExamManager SetExamManager, string currentUsername)
        {
            List<StudentAdmission> NotifyAllExam = new StudentAdmissionService().FindAll(currentUsername).FindAll(st => st.SubjectManager.Id == SetExamManager.SubjectManager.Id);
            NotifyAllExam = NotifyAllExam.Where(std => Authorization.GetCurrentStudent(std.Student_id, currentUsername).EntryInformation.IsActive == Status.Enable).ToList();

        }
    }
}
