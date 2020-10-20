using eds_coaching_api_services.BLL.Interfaces;
using eds_coaching_api_services.DAL.Repositories;
using eds_coaching_api_services.Models;
using eds_coaching_api_services.Utility.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace eds_coaching_api_services.BLL.Services
{
    public class StaffService : IUser<Staff, int, string>
    {
        private readonly StaffRepository staffRepository = new StaffRepository();
        private readonly InstitutionService institutionService = new InstitutionService();
        private readonly LoginService loginService = new LoginService();

        
        public string Disable(int id, string currentUser)
        {
            Staff staff = FindById(id, currentUser);
            if (staff != null)
            {
                staff.EntryInformation.IsActive = Status.Disable;
                return Update(staff, currentUser);
            }
            else return Messages.NotFound;
        }
        
        public string Enable(int id, string currentUser)
        {
            Staff staff = FindById(id, currentUser);
            if (staff != null)
            {
                staff.EntryInformation.IsActive = Status.Enable;
                return Update(staff, currentUser);
            }
            else return Messages.NotFound;
        }
        
        public List<Staff> FindAll(string currentUser)
        {
            List<Staff> staffsList = staffRepository.FindAll();
            Login login = loginService.FindByUsername(currentUser);
            if(login != null)
            {
                if(login.UserRole.ToLower() == Roles.Admin.ToString().ToLower())
                {
                    staffsList = staffsList.FindAll(sl =>sl.Login.InstitutionProfile_id == login.InstitutionProfile_id );
                }
            }
            return staffsList;
        }
        
        public Staff FindById(int id, string currentUser)
        {
            Staff staff = FindAll(currentUser).Find(st => st.Id == id);
            return staff;
        }
        
        public Staff FindByUsername(string Username, string currentUser)
        {
            Staff staff = FindAll(currentUser).Find(st => st.Username == Username);
            return staff;
        }
        
        public string Save(Staff staff, string currentUser)
        {
            string message;
            Login login = loginService.FindByUsername(currentUser);
            if (login != null)
            {
                if (staff.Login.Password != null)
                {
                    SetAllValue(staff, login);
                    message = ValidStaff(staff, currentUser);
                    if (message == null)
                    {
                        return (staffRepository.Save(staff)) ? null : Messages.issueInDatabase;
                    }
                }
                else message = Messages.InvalidPasswordField;
            }
            else message = Messages.AccessDenied;
            return message;
        }
        
        public int GetCurrentUserId(string username)
        {
            return FindByUsername(username, username).Id;
        }
        
        public string Update(Staff staff, string currentUser)
        {
            Login login = loginService.FindByUsername(currentUser);
            if (login != null)
            {
                Staff foundedStaff = FindById(staff.Id, currentUser);
                if (foundedStaff != null)
                {
                    SetAllValueForUpdate(staff, foundedStaff, login);
                    return (staffRepository.Update(staff)) ? null : Messages.issueInDatabase;
                }
                else return Messages.NotFound;
            }
            else return Messages.AccessDenied;
        }
        
        public string DeleteById(int id, string currentUsername)
        {
            throw new NotImplementedException();
        }

        private string ValidStaff(Staff staff, string currentUser)
        {
            Institution institution = institutionService.FindById(staff.Login.InstitutionProfile_id);
            if (institution != null)
            {
                Staff foundStaff = FindById(staff.Id, currentUser);
                if (foundStaff == null)
                {
                    foundStaff = FindByUsername(staff.Username, currentUser);
                    if (foundStaff == null)
                    {
                        return null;
                    }
                    else return Messages.usernameExist;
                }
                else return Messages.IdExist;
            }
            else return Messages.InstitutionNotExist;
        }
        private void SetAllValue(Staff staff, Login login)
        {
            staff.PersonalInformation.Username  = staff.Username;
            staff.Login.Username                = staff.Username;
            staff.Login.Password                = Security.Encrypt(staff.Login.Password);
            staff.Login.IsLoginActive           = Status.Enable;
            staff.EntryInformation.EntryBy_id   = GetCurrentUserId(login.Username);
            staff.EntryInformation.EntryDate    = DateTime.Now;
            staff.EntryInformation.IsActive     = Status.Enable;

            if (login.UserRole == Roles.Admin.ToString())
            {
                staff.Login.InstitutionProfile_id = login.InstitutionProfile_id;
            }
        }
        private void SetAllValueForUpdate(Staff staff, Staff foundedStaff, Login login)
        {
            int status = staff.EntryInformation.IsActive;
            string userRole     = staff.Login.UserRole;
            int institution_id  = staff.Login.InstitutionProfile_id;
            
            staff.Id                                            = foundedStaff.Id;
            staff.Username                                      = foundedStaff.Username;
            staff.PersonalInformation.Username                  = staff.Username;
            staff.PersonalInformation.UserContactInformation.Id = foundedStaff.PersonalInformation.UserContactInformation.Id;
            staff.Login                                         = foundedStaff.Login;
            staff.Login.UserRole                                = userRole ?? foundedStaff.Login.UserRole;
            staff.EntryInformation                              = foundedStaff.EntryInformation;
            staff.EntryInformation.IsActive                     = status;
            
            if (login.UserRole == Roles.Admin.ToString())
            {
                staff.Login.InstitutionProfile_id = login.InstitutionProfile_id;
            }
            else if (login.UserRole == Roles.Superadmin.ToString())
            {
                staff.Login.InstitutionProfile_id = institution_id;
            }
        }

    }
}
