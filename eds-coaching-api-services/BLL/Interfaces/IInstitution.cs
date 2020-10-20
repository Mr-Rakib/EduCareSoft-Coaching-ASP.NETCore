using eds_coaching_api_services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.BLL.Interfaces
{
    interface IInstitution
    {
        List<Institution> FindAll();
        Institution FindById(int id);
        string Save(Institution institution);
        bool DeleteById(int id);
        string Disable(int id);
        string Enable (int id);
    }
}
