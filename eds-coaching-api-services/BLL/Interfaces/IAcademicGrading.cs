using eds_coaching_api_services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.BLL.Interfaces
{
    interface IAcademicGrading : ICRUD<AcademicGrading, int, string>
    {
        List<AcademicGrading> FindByGradingSystemName(string SystemName, string currentUsername);
    }
}
