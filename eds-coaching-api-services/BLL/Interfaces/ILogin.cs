using eds_coaching_api_services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.BLL.Interfaces
{
    interface ILogin
    {
        //for token generator
        List<Login> FindAll();
        Login FindByUsername(string username);
        Login FindByUsernamePassword(string username, string password);
        string UpdateLastloginDate(Login login);

        List<Login> FindAll(string currentUsername);
        List<Login> FindAllStaffs(string currentUsername);
        List<Login> FindAllStudents(string currentUsername);

        Login FindStaffByUsername(string username, string currentUsername);
        Login FindStudentByUsername(string username, string currentUsername);

        string ChangePassword(string username, string password, string currentUsername);

        string Update(Login login, string currentUsername);
        string Enable(string username, string currentUsername);
        string Disable(string username, string currentUsername);
    }
}
