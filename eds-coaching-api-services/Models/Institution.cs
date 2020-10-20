using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Models
{
    public class Institution
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public ContactInformation ContactInformation { get; set; }
        public int IsActive { get; set; }
    }
}
