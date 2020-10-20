using eds_coaching_api_services.BLL.Interfaces;
using eds_coaching_api_services.DAL.Repositories;
using eds_coaching_api_services.Models;
using eds_coaching_api_services.Utility.Database.Tables;
using eds_coaching_api_services.Utility.Dependencies;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.BLL.Services
{
    public class ClassRoutineService : IClassRoutine
    {
        private readonly StudentAdmissionService studentAdmissionService = new StudentAdmissionService();
        private readonly ClassRoutineRepository ClassRoutineRepository = new ClassRoutineRepository();
        private readonly SubjectManagerService subjectManagerService = new SubjectManagerService();
        private readonly TracerService tracerService = new TracerService();
        private readonly StaffService staffService = new StaffService();

        public string DeleteById(int id, string currentUsername)
        {
            ClassRoutine ClassRoutine = FindById(id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticate(ClassRoutine, staff);

            if (String.IsNullOrEmpty(message))
            {
                if (ClassRoutineRepository.Delete(id))
                {
                    tracerService.Delete(DBTables.ClassRoutine.ToString(), staff.Id, ClassRoutine.Id, ClassRoutine.SubjectManager.Subject.Institution_id);
                    return null;
                }
                else return Messages.issueInDatabase;
            }
            else return message;
        }
        
        public List<ClassRoutine> FindAll(string currentUsername)
        {
            List<ClassRoutine> ClassRoutines = ClassRoutineRepository.FindAll();
            ClassRoutines.ForEach(clr => clr.SubjectManager = subjectManagerService.FindById(clr.SubjectManager.Id, currentUsername));

            Login loginUser = Authorization.GetCurrentLoginUser(currentUsername);
            if (loginUser != null)
            {
                if (! Authorization.IsSuperAdmin(currentUsername))
                {
                    ClassRoutines = ClassRoutines.FindAll(clrs => clrs.SubjectManager.Subject.Institution_id == loginUser.InstitutionProfile_id);
                }
            }
            return ClassRoutines;
        }
        
        public ClassRoutine FindById(int id, string currentUsername)
        {
            ClassRoutine ClassRoutine = FindAll(currentUsername).Find(d => d.Id == id);
            return ClassRoutine;
        }
        
        public string Save(ClassRoutine ClassRoutine, string currentUsername)
        {
            SetALLValue(ClassRoutine,currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticateClassRoutine(ClassRoutine, staff, currentUsername);

            if (FindById(ClassRoutine.Id, currentUsername) == null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    return ClassRoutineRepository.Save(ClassRoutine) ? null : Messages.issueInDatabase;
                }
                else return message;
            }
            else return Messages.IdExist;
        }
        
        public string Update(ClassRoutine ClassRoutine, string currentUsername)
        {
            SetALLValue(ClassRoutine, currentUsername);
            ClassRoutine foundedClassRoutine = FindById(ClassRoutine.Id, currentUsername);
            Staff staff = Authorization.GetCurrentUser(currentUsername);
            string message = IsAuthonticateClassRoutine(ClassRoutine, staff,currentUsername);

            if (foundedClassRoutine != null)
            {
                if (String.IsNullOrEmpty(message))
                {
                    if (ClassRoutineRepository.Update(ClassRoutine))
                    {
                        tracerService.Update(DBTables.ClassRoutine.ToString(), staff.Id, ClassRoutine.Id, ClassRoutine.SubjectManager.Subject.Institution_id);
                        return null;
                    }
                    else return Messages.issueInDatabase;
                }
                else return message;
            }
            else return Messages.NotFound;
        }
        
        public List<ClassRoutine> FindByStaffId(int id, string currentUsername)
        {
            List<ClassRoutine> ClassRoutines = FindAllCurrentSession(currentUsername);
            ClassRoutines = ClassRoutines.FindAll(clr => clr.StaffId == id);
            
            if (! Authorization.IsStudent(currentUsername))
            {
                if (Authorization.IsTeacher(currentUsername))
                {
                    Staff staff = new StaffRepository().FindAll().Find(st => st.Username == currentUsername);
                    if (staff.Id != id)
                        return null;
                }
            }else return null;

            return ClassRoutines;
        }
        
        public List<ClassRoutine> FindByDay(string day, string currentUsername)
        {
            List<ClassRoutine> ClassRoutines = FindAll(currentUsername).FindAll(clr => clr.Day.ToLower() == day.ToLower());
            return ClassRoutines;
        }
        
        public List<ClassRoutine> FindByClass(int id, string currentUsername)
        {
            List<ClassRoutine> ClassRoutines = FindAll(currentUsername).FindAll(clr => clr.SubjectManager.Class.Id == id);
            return ClassRoutines;
        }

        public List<ClassRoutine> FindByStudentId(int id, string currentUsername)
        {
            List<ClassRoutine> ClassRoutines = FindAllCurrentSession(currentUsername);
            ClassRoutines = ClassRoutines.Where(clrs => FindStudentId(id, clrs.SubjectManager.Id, currentUsername) == id).ToList();

            if(Authorization.IsStudent(currentUsername))
            {
                Student student = new StudentRepository().FindAll().Find(st => st.Username == currentUsername);
                if (student.Id != id)
                    return null;
            }
            return ClassRoutines;
        }

        public List<ClassRoutine> FindByInstitutionId(int id, string currentUsername)
        {
            List<ClassRoutine> ClassRoutines = FindAll(currentUsername).FindAll(clr => clr.SubjectManager.Subject.Institution_id == id);
            return ClassRoutines;
        }

        private int FindStudentId(int studentId, int subjectManagerId, string currentUsername)
        {
            List<StudentAdmission> studentAdmissions = studentAdmissionService.FindAll(currentUsername).FindAll(stda => stda.SubjectManager.Id == subjectManagerId);
            StudentAdmission studentAdmission =  studentAdmissions.Find(std => std.Student_id == studentId);

            return (studentAdmission == null) ? 0 : studentAdmission.Student_id;
        }
        private string IsAuthonticateClassRoutine(ClassRoutine classRoutine, Staff staff, string currentUsername)
        {
            string message = IsAuthonticate(classRoutine, staff);
            Staff teacher = staffService.FindById(classRoutine.StaffId, currentUsername);
            SubjectManager subjectManager = subjectManagerService.FindById(classRoutine.SubjectManager.Id, currentUsername);

            if (String.IsNullOrEmpty(message))
            {
                if (teacher != null)
                {
                    if (subjectManager != null)
                    {
                        if (!IsConflictRoutine(classRoutine, currentUsername))
                        {
                            if( IsSameInstitution(classRoutine, currentUsername))
                            {
                                return null;
                            }
                            return Messages.AccessDenied;
                        }
                        else return Messages.Conflict;
                    }
                    else return Messages.SubjectManagerNotExist;
                }
                else return Messages.StaffNotExist;
            }
            else return message;
        }
        private string IsAuthonticate(ClassRoutine ClassRoutine, Staff staff)
        {
            if (ClassRoutine != null)
            {
                if (staff != null)
                {
                    return null;
                }
                else return Messages.AccessDenied;
            }
            else return Messages.invalidField;
        }
        private bool IsSameInstitution(ClassRoutine classRoutine, string currentUsername)
        {
            Staff teacher = staffService.FindById(classRoutine.StaffId, currentUsername);
            return (teacher.Login.InstitutionProfile_id == classRoutine.SubjectManager.Subject.Institution_id) 
            ? true : false;
        }
        private bool IsConflictRoutine(ClassRoutine classRoutine, string currentUsername)
        {
            List<ClassRoutine> classRoutines = FindAll(currentUsername).FindAll(clr => clr.StaffId == classRoutine.StaffId);
            classRoutines = classRoutines.Where(clr => clr.Day == classRoutine.Day && 
                                                clr.TimeStart == classRoutine.TimeStart && 
                                                clr.TimeEnd == classRoutine.TimeEnd && 
                                                clr.SubjectManager.Session == classRoutine.SubjectManager.Session
                                                ).ToList();
            
            return (classRoutines == null) ? true : false;
        }
        private void SetALLValue(ClassRoutine ClassRoutine, string currentUsername)
        {
            ClassRoutine.SubjectManager = subjectManagerService.FindById(ClassRoutine.SubjectManager.Id, currentUsername);
        }
        private List<ClassRoutine> FindAllCurrentSession(string currentUsername)
        {
            List<ClassRoutine> classRoutines = FindAll(currentUsername).FindAll(clr => clr.SubjectManager.Session == DateTime.Now.Year);
            return classRoutines;

        }

    }
}
