using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Models
{
    public class AcademicGrading
    {
        public int Id { get; set; }
        public GradingSystem GradingSystem { get; set; }
        public string GradeName { get; set; }
        public float PercentageFrom { get; set; }
        public float PercentageTo { get; set; }
        public float Gpa { get; set; }
        public int EntryBy_id { get; set; }
    }
}
