using eds_coaching_api_services.Models;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.BLL.Interfaces
{
    interface IAttendance : ICRUD<Attendance,int, string>
    {
        List<Attendance> FindByUserId(int id, string currentUsername);
        List<Attendance> FindByDate(DateTime date, string currentUsername);
    }
}
