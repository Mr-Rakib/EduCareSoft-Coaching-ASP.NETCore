using System;
using System.Collections.Generic;
using System.Text;

namespace eds_coaching_api_services.Models
{
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int IsLoginActive { get; set; }
        public int InstitutionProfile_id { get; set; }
    }
}
