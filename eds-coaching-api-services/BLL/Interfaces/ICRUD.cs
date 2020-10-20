using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.BLL.Interfaces
{
    interface ICRUD<T, I, J>
    {
        List<T> FindAll(J currentUsername);
        T FindById(I id, J currentUsername);
        string Save(T type, J currentUsername);
        string Update(T type, J currentUsername);
        string DeleteById(I id, J currentUsername);
    }
}
