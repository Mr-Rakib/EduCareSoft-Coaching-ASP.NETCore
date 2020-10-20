using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public PersonalInformation PersonalInformation { get; set; }
        public GuardinaInformation GuardianInformation {get; set;}
        public Login Login { get; set; }
        public EntryInformation EntryInformation { get; set; }
    }
}
