using eds_coaching_api_services.BLL.Interfaces;
using eds_coaching_api_services.DAL.Repositories;
using eds_coaching_api_services.Models;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Text;
using eds_coaching_api_services.Utility.Dependencies;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eds_coaching_api_services.BLL.Services
{
    public class LoginService : ILogin
    {
        private readonly LoginRepository loginRepository = new LoginRepository();

        //For Token Generator / Login Procss
        public List<Login> FindAll()
        {
            List<Login> loginList = loginRepository.FindAll();
            //decrypt all password
            loginList.ForEach(us => us.Password = Security.Decrypt(us.Password));
            return loginList;
        }
        public Login FindByUsername(string username)
        {
            Login login = FindAll().Find(user => user.Username == username);
            return login;
        }
        public Login FindByUsernamePassword(string username, string password)
        {
            Login login = FindAll().Find(user => user.Username == username  && user.Password == password);
            return login;
        }
        public string UpdateLastloginDate(Login login)
        {
            login.LastLoginDate = DateTime.Now;
            return Update(login);
        }
        public string Update(Login login)
        {
            Login foundLogin = FindByUsername(login.Username);
            if(foundLogin != null)
            {
                login.Password = Security.Encrypt(login.Password);
                return loginRepository.Update(login) ? null : Messages.issueInDatabase;
            }
            else return Messages.NotFound;
        }


        //-----------------------------Controller Services ---------------------

        public List<Login> FindAll(string currentUsername)
        {
            List<Login> logins = FindAll();
            Login loginUser = Authorization.GetCurrentLoginUser(currentUsername);
            if(loginUser != null)
            {
                if (Authorization.IsSuperAdmin(currentUsername))
                    logins = logins;

                else if (Authorization.IsAdmin(currentUsername))
                    logins = logins.FindAll(lg => lg.InstitutionProfile_id == loginUser.InstitutionProfile_id);

                else
                    logins = logins.FindAll(lg => lg.Username == loginUser.Username);
            }
            return logins;
        }

        public List<Login> FindAllStaffs(string currentUsername)
        {
            List<Login> StaffsList = FindAll(currentUsername);
            Login LoginUser = Authorization.GetCurrentLoginUser(currentUsername);
            
            StaffsList = StaffsList.Where(st => st.UserRole.ToLower() != Roles.Student.ToString().ToLower()).ToList();

            StaffsList.ForEach(st=> st.Password = null);
            return StaffsList;
        }

        public List<Login> FindAllStudents(string currentUsername)
        {
            List<Login> StaffsList = FindAll(currentUsername);
            Login LoginUser = Authorization.GetCurrentLoginUser(currentUsername);

            StaffsList = StaffsList.Where(st => st.UserRole.ToLower() == Roles.Student.ToString().ToLower()).ToList();

            return StaffsList;
        }

        public string Enable(string username, string currentUsername)
        {
            Login login = FindByUsername(username, currentUsername);

            if (login != null)
            {
                login.IsLoginActive = Status.Enable;
                return Update(login, currentUsername);
            }
            else return Messages.NotFound;
        }

        public string Disable(string username, string currentUsername)
        {
            Login login = FindByUsername(username, currentUsername);

            if (login != null)
            {
                login.IsLoginActive = Status.Disable;
                return Update(login, currentUsername);
            }
            else return Messages.NotFound;
        }
        
        public string ChangePassword(string username, string password, string currentUsername)
        {
            Login login = FindByUsername(username, currentUsername);

            if (login != null)
            {
                login.Password = password;
                return Update(login, currentUsername);
            }
            else return Messages.NotFound;
        }

        public string Update(Login login, string currentUsername)
        {
            Login foundLogin = FindByUsername(login.Username, currentUsername);
            if (foundLogin != null)
            {
                login.Password = Security.Encrypt(login.Password);
                return loginRepository.Update(login) ? null : Messages.issueInDatabase;
            }
            else return Messages.NotFound;
        }

        public Login FindByUsername(string username , string currentUsername)
        {
            Login login = FindAll(currentUsername).Find(user => user.Username == username);
            return login;
        }
        
        private List<Login> FindWithoutSuperAdmin()
        {
            return FindAll().Where(st => st.UserRole.ToLower() != Roles.Superadmin.ToString().ToLower()).ToList();
        }
        
        public Login FindStaffByUsername(string username, string currentUsername)
        {
            Login Login = FindAllStaffs(currentUsername).Find(st => st.Username == username);
            return Login;
        }
        
        public Login FindStudentByUsername(string username, string currentUsername)
        {
            Login Login = FindAllStudents(currentUsername).Find(st => st.Username == username);
            return Login;
        }
    }
}
