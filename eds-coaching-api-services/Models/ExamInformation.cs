using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Models
{
    public class ExamInformation
    {
        public int Id { get; set; }
        public string ExamName { get; set; }
        public int InstitutionId { get; set; }
        public int EntryBy_id { get; set; }
    }
}
