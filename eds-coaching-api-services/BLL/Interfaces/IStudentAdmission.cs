using eds_coaching_api_services.Models;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.BLL.Interfaces
{
    interface IStudentAdmission : ICRUD<StudentAdmission, int, string>
    {
        List<StudentAdmission> FindByStudentId(int id, string currentUsername);
        List<StudentAdmission> FindByInstitutionId(int id, string currentUsername);
    }
}
