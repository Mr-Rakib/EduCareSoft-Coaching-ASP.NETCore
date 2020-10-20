using eds_coaching_api_services.BLL.Interfaces;
using eds_coaching_api_services.DAL.Repositories;
using eds_coaching_api_services.Models;
using eds_coaching_api_services.Utility.Database.Tables;
using eds_coaching_api_services.Utility.Dependencies;
using Org.BouncyCastle.Math.EC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.BLL.Services
{
    public class StudentAdmissionService : IStudentAdmission
    {
        private readonly TracerService tracerService = new TracerService();
        private readonly StudentService studentService = new StudentService();
        private readonly SubjectManagerService subjectManagerService = new SubjectManagerService();
        private readonly StudentAdmissionRepository StudentAdmissionRepository = new StudentAdmissionRepository();

        public string DeleteById(int id, string currentUsername)
        {
            StudentAdmission StudentAdmission = FindById(id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticate(StudentAdmission, staff);

            if (String.IsNullOrEmpty(message))
            {
                if (StudentAdmissionRepository.Delete(id))
                {
                    tracerService.Delete(DBTables.StudentAdmission.ToString(), staff.Id, StudentAdmission.Id, StudentAdmission.SubjectManager.Subject.Institution_id);
                    return null;
                }
                else return Messages.issueInDatabase;
            }
            else return message;
        }
        public List<StudentAdmission> FindAll(string currentUsername)
        {
            List<StudentAdmission> StudentAdmissions = StudentAdmissionRepository.FindAll();
            Staff logedinStaff = Authorization.GetCurrentUser(currentUsername);
            
            if (logedinStaff != null)
            {
                if (Authorization.IsAdmin(logedinStaff))
                {
                    StudentAdmissions = StudentAdmissions.
                        FindAll(sa => logedinStaff.Login.InstitutionProfile_id  == Authorization.GetInstitution(sa.Student_id, currentUsername) && 
                                    sa.SubjectManager.Subject.Institution_id    == logedinStaff.Login.InstitutionProfile_id &&
                                    sa.SubjectManager.Batch.Institution_id      == logedinStaff.Login.InstitutionProfile_id &&
                                    sa.SubjectManager.Class.Institution_id      == logedinStaff.Login.InstitutionProfile_id 
                                );
                }
            }
            return StudentAdmissions;
        }
        public StudentAdmission FindById(int id, string currentUsername)
        {
            StudentAdmission StudentAdmission = FindAll(currentUsername).Find(d => d.Id == id);
            return StudentAdmission;
        }
        public List<StudentAdmission> FindByStudentId(int id, string currentUsername)
        {
            Student student = new StudentService().FindById(id, currentUsername);
            List<StudentAdmission> StudentAdmissionsByStudent = new List<StudentAdmission>();
            if (student != null)
            {
                StudentAdmissionsByStudent = FindAll(currentUsername).FindAll(d => d.Student_id == id);
            }
            else StudentAdmissionsByStudent = null;
            return StudentAdmissionsByStudent;
        }
        public List<StudentAdmission> FindByInstitutionId(int id, string currentUsername)
        {
            Institution institution = new InstitutionService().FindById(id);
            List<StudentAdmission> StudentAdmissionsByInstitution = new List<StudentAdmission>();

            if (institution != null)
            {
                StudentAdmissionsByInstitution = FindAll(currentUsername)
                                                .FindAll(sa => sa.SubjectManager.Subject.Institution_id == id &&
                                                                sa.SubjectManager.Batch.Institution_id == id &&
                                                                sa.SubjectManager.Class.Institution_id == id);
            }
            else StudentAdmissionsByInstitution = null;
            return StudentAdmissionsByInstitution;
        }
        public string Save(StudentAdmission StudentAdmission, string currentUsername)
        {
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticateAdmission(StudentAdmission, staff);

            if (FindById(StudentAdmission.Id, currentUsername) == null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    StudentAdmission.EntryBy_id = staff.Id;
                    SetALLValue(StudentAdmission, currentUsername);
                    return StudentAdmissionRepository.Save(StudentAdmission) ?
                            null : Messages.issueInDatabase;
                }
                else return message;
            }
            else return Messages.IdExist;
        }
        public string Update(StudentAdmission StudentAdmission, string currentUsername)
        {
            StudentAdmission foundedStudentAdmission = FindById(StudentAdmission.Id, currentUsername);
            Staff staff     = Authorization.GetCurrentUser(currentUsername);
            StudentAdmission.Student_id = foundedStudentAdmission.Student_id;
            StudentAdmission.EntryBy_id = foundedStudentAdmission.EntryBy_id;

            string message  = IsAuthonticateAdmission(StudentAdmission, staff);

            if (foundedStudentAdmission != null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    SetALLValue(StudentAdmission, currentUsername);
                    if (StudentAdmissionRepository.Update(StudentAdmission))
                    {
                        tracerService.Update(DBTables.StudentAdmission.ToString(), staff.Id, StudentAdmission.Id, StudentAdmission.SubjectManager.Subject.Institution_id);
                        return null;
                    }
                    else return Messages.issueInDatabase;
                }
                else return message;
            }
            else return Messages.NotFound;
        }

        private string IsAuthonticateAdmission(StudentAdmission studentAdmission, Staff staff)
        {
            string message = IsAuthonticate(studentAdmission, staff);
            Student student = studentService.FindById(studentAdmission.Student_id, staff.Username);
            SubjectManager subjectManager = subjectManagerService.FindById(studentAdmission.SubjectManager.Id, staff.Username);
            if (String.IsNullOrEmpty(message))
            {
                if (student != null)
                {
                    if (student.EntryInformation.IsActive == Status.Enable)
                    {
                        if (subjectManager != null)
                        {
                            if (student.Login.InstitutionProfile_id == subjectManager.Subject.Institution_id)
                            {
                                return null;
                            }
                            else return Messages.InvalidStudentAdmission;
                        }
                        else return Messages.SubjectManagerNotExist;
                    }
                    else return Messages.NotActive;
                }
                else return Messages.StudentNotExist;
            }
            return message;
        }
        private string IsAuthonticate(StudentAdmission StudentAdmission, Staff staff)
        {
            if (StudentAdmission != null)
            {
                if (staff != null)
                {
                    return null;
                }
                else return Messages.AccessDenied;
            }
            else return Messages.invalidField;
        }
        private void SetALLValue(StudentAdmission StudentAdmission, string currentUsername)
        {
            StudentAdmission.SubjectManager = subjectManagerService.FindById(StudentAdmission.SubjectManager.Id, currentUsername);
            StudentAdmission.AdmissionDate  = StudentAdmission.AdmissionDate.ToShortDateString() == null ? DateTime.Now : StudentAdmission.AdmissionDate ;
            StudentAdmission.MonthlyFees    = StudentAdmission.MonthlyFees == 0 ? StudentAdmission.SubjectManager.Fees : StudentAdmission.MonthlyFees;
        }
    }
}
