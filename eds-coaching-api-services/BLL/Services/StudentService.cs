using eds_coaching_api_services.BLL.Interfaces;
using eds_coaching_api_services.DAL.Repositories;
using eds_coaching_api_services.Models;
using eds_coaching_api_services.Utility.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.BLL.Services
{
    public class StudentService : IUser<Student, int, string>
    {
        private readonly StudentRepository studentRepository = new StudentRepository();
        private readonly InstitutionService institutionService = new InstitutionService();
        private readonly LoginService loginService = new LoginService();
        
        public Student FindById(int id, string currentUser)
        {
            Student student = new Student();
            student = FindAll(currentUser).Find(user => user.Id == id);
            return student;
        }
        
        public Student FindByUsername(string username, string currentUser)
        {
            Student student = new Student();
            student = FindAll(currentUser).Find(user => user.Username == username);
            return student;
        }
        
        public List<Student> FindAll(string currentUser)
        {
            List<Student> students = SetPermissionOfData(currentUser);
            return students;
        }
        
        private List<Student> SetPermissionOfData(string currentUser)
        {
            List<Student> students = studentRepository.FindAll();
            Login login = loginService.FindByUsername(currentUser);
           
            if (login.UserRole == Roles.Admin.ToString())
            {
                students = studentRepository.FindAll().
                      FindAll(student => student.Login.InstitutionProfile_id == login.InstitutionProfile_id).ToList();
            }
            return students;
        }
        
        public string Update(Student student, string currentUser)
        {
            Login login = loginService.FindByUsername(currentUser);
            if (login != null)
            {
                Student foundedStudent = FindById(student.Id, currentUser);
                if (foundedStudent != null)
                {
                    SetAllValueForUpdate(student, foundedStudent, login);
                    return (studentRepository.Update(student)) ? null : Messages.issueInDatabase;
                }
                else return Messages.NotFound;
            }
            else return Messages.AccessDenied;
        }
        
        public string Save(Student student, string currentUser)
        {
            string message;
            Login login = loginService.FindByUsername(currentUser);
            if (login != null)
            {
                SetAllValue(student, login);
                message = ValidStudent(student, currentUser);
                if (message == null)
                {
                    return (studentRepository.Save(student)) ? null : Messages.issueInDatabase;
                }
            }
            else message = Messages.AccessDenied;
            return message;

        }
        
        public string Disable(int id, string currentUser)
        {
            Student student = FindById(id, currentUser);
            if (student != null)
            {
                student.EntryInformation.IsActive = Status.Disable;
                return Update(student, currentUser);
            }
            else return Messages.NotFound;
        }
        
        public string Enable(int id, string currentUser)
        {
            Student student = FindById(id, currentUser);
            if (student != null)
            {
                student.EntryInformation.IsActive = Status.Enable;
                return Update(student, currentUser);
            }
            else return Messages.NotFound;
        }
        
        private void SetAllValue(Student student, Login login)
        {
            student.PersonalInformation
            .Username                   = student.Username;
            student.Login.Username      = student.Username;
            student.Login.Password      = Security.Encrypt(Generator.GeneratePassword());
            student.Login.UserRole      = Roles.Student.ToString();
            student.Login.IsLoginActive = Status.Enable;
            //set after complete the staff CRUD
            student.EntryInformation.EntryBy_id = new StaffService().GetCurrentUserId(login.Username);
            student.EntryInformation.EntryDate  = DateTime.Now;
            student.EntryInformation.IsActive   = Status.Enable;

            if (login.UserRole == Roles.Admin.ToString())
            {
                student.Login.InstitutionProfile_id = login.InstitutionProfile_id;
            }
        }
        
        public string DeleteById(int id, string currentUsername)
        {
            throw new NotImplementedException();
        }
        
        private string ValidStudent(Student student, string currentUser)
        {
            Institution institution = institutionService.FindById(student.Login.InstitutionProfile_id);
            if (institution != null)
            {
                Student foundStudent = FindById(student.Id, currentUser);
                if (foundStudent == null)
                {
                    foundStudent = FindByUsername(student.Username, currentUser);
                    if (foundStudent == null)
                    {
                        return null;
                    }
                    else return Messages.usernameExist;
                }
                else return Messages.IdExist;
            }
            else return Messages.InstitutionNotExist;
        }

        private void SetAllValueForUpdate(Student student, Student foundedStudent, Login login)
        {
            int status = student.EntryInformation.IsActive;
            int institution_id = student.Login.InstitutionProfile_id;

            student.Id                              = foundedStudent.Id;
            student.Username                        = foundedStudent.Username;
            student.PersonalInformation.Username    = student.Username;
            student.PersonalInformation
            .UserContactInformation.Id              = foundedStudent.PersonalInformation.UserContactInformation.Id;
            student.Login                           = foundedStudent.Login;
            student.EntryInformation                = foundedStudent.EntryInformation;
            student.EntryInformation.IsActive       = status;

            if (login.UserRole == Roles.Admin.ToString())
            {
                student.Login.InstitutionProfile_id = login.InstitutionProfile_id;
            }
            else if (login.UserRole == Roles.Superadmin.ToString())
            {
                student.Login.InstitutionProfile_id = institution_id;
            }
        }

    }
}