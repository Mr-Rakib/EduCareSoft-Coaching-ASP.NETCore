using System;
using System.Collections.Generic;
using System.Text;

namespace eds_coaching_api_services.Models
{
    public class TokenProvider
    {
        public string Username { get; set; }
        public string UserRole { get; set; }
        public string Token { get; set; }
        public string Token_type { get; set; }
        public int Expire_in { get; set; }
        public DateTime Issued { get; set; }
        public DateTime Expired { get; set; }
    }
}
