using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.DAL.Interface
{
    interface ICRUDRepository<T>
    {
        List<T> FindAll();
        bool Update(T items);
        bool Save(T items);
        bool Delete(int id);
    }
}
