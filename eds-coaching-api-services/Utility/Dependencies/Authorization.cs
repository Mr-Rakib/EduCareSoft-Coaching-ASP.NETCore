using eds_coaching_api_services.BLL.Services;
using eds_coaching_api_services.DAL.Repositories;
using eds_coaching_api_services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Utility.Dependencies
{
    public static class Authorization
    {
        private static readonly StaffService staffService = new StaffService();
        private static readonly LoginService loginService = new LoginService();
        private static readonly StudentService StudentService= new StudentService();

        public static bool IsSuperAdmin(string username)
        {
            Login Login = GetCurrentLoginUser(username);
            return string.Equals(Login.UserRole, Roles.Superadmin.ToString(), StringComparison.OrdinalIgnoreCase) ?
            true : false;
        }

        public static bool IsAdmin(Staff staff)
        {
            return String.Equals(staff.Login.UserRole, Roles.Admin.ToString(), StringComparison.OrdinalIgnoreCase) ?
            true : false;
        }

        public static bool IsAdmin(Login Login)
        {
            return String.Equals(Login.UserRole, Roles.Admin.ToString(), StringComparison.OrdinalIgnoreCase) ?
            true : false;
        }

        public static bool IsAdmin(string username)
        {
            Login Login = GetCurrentLoginUser(username);
            return string.Equals(Login.UserRole, Roles.Admin.ToString(), StringComparison.OrdinalIgnoreCase) ?
            true : false;
        }

        public static bool IsTeacher(Login Login)
        {
            return String.Equals(Login.UserRole, Roles.Teacher.ToString(), StringComparison.OrdinalIgnoreCase) ?
            true : false;
        }

        public static bool IsTeacher(string username)
        {
            Login Login = GetCurrentLoginUser(username);
            return string.Equals(Login.UserRole, Roles.Teacher.ToString(), StringComparison.OrdinalIgnoreCase) ?
            true : false;
        }


        public static bool IsStudent(string username)
        {
            Login Login = GetCurrentLoginUser(username);
            return String.Equals(Login.UserRole, Roles.Student.ToString(), StringComparison.OrdinalIgnoreCase) ?
            true : false;
        }

        public static Staff GetCurrentUser(string currentUsername)
        {
            return staffService.FindByUsername(currentUsername, currentUsername);
        }

              
        public static Student GetCurrentStudent(string username)
        {
            Student student = new Student();
            student = new StudentRepository().FindAll().Find(st => st.Username == username);
            return student;
        }

        public static Login GetCurrentLoginUser(string currentUsername)
        {
            Login login = new Login();
            login = loginService.FindByUsername(currentUsername);
            return login ;
        }

        public static int GetInstitution(int studentId, string currentUsername)
        {
            Student student = StudentService.FindById(studentId, currentUsername);
            return student == null ? 0 : student.Login.InstitutionProfile_id;
        }

        internal static bool IsActiveStudent(int studentId, string username)
        {
            Student student = StudentService.FindById(studentId, username);
            if (student != null)
            {
                return student.EntryInformation.IsActive == Status.Enable ? true : false;
            }return false;
        }

        internal static Student GetCurrentStudent(int student_id, string currentUsername)
        {
            Student student = StudentService.FindById(student_id, currentUsername);
            if (student != null)
            {
                return student;
            }
            return null; ;
        }
    }
}
