using eds_coaching_api_services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.BLL.Interfaces
{
    interface IUser<T, I, J> : ICRUD<T, I, J>
    {
        T FindByUsername(J Username, J currentUser);
        string Disable(I id, J currentUser);
        string Enable(I id, J currentUser);
    }
}
