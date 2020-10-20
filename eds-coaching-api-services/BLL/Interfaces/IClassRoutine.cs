using eds_coaching_api_services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.BLL.Interfaces
{
    interface IClassRoutine : ICRUD<ClassRoutine, int, string>
    {
        List<ClassRoutine> FindByClass(int id, string currentUsername);
        List<ClassRoutine> FindByDay(string day, string currentUsername);
        List<ClassRoutine> FindByStaffId(int id, string currentUsername);
        List<ClassRoutine> FindByStudentId(int id, string currentUsername);
        List<ClassRoutine> FindByInstitutionId(int id, string currentUsername);
    }
}
