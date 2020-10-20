using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Models
{
    public class PersonalInformation
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Image { get; set; }
        public ContactInformation UserContactInformation { get; set; }
    }
}
