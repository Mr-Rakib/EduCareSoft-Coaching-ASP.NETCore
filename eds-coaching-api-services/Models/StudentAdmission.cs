using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Models
{
    public class StudentAdmission
    {
        public int Id { get; set; }
        public int Student_id { get; set; }
        public SubjectManager SubjectManager { get; set; }
        public float MonthlyFees { get; set; }
        public DateTime AdmissionDate { get; set; }
        public int EntryBy_id{ get; set; }

    }
}
