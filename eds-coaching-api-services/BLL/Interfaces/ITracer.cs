using eds_coaching_api_services.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.BLL.Interfaces
{
    interface ITracer 
    {
        List<Tracer> FindAll(string currentUsername);
        List<Tracer> FindByActorID(int id, string currentUsername);
        List<Tracer> FindByInstitution(int id, int userId, string currentUsername);

        bool Update(string table, int staffID, int id, int institutionID);
        bool Delete(string table, int staffID, int id, int institutionID);
    }
}
