using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Models
{
    public class LoginCredential
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Grant_type { get; set; }
    }
}
