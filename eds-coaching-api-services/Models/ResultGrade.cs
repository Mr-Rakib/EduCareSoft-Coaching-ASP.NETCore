using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Models
{
    public class ResultGrade
    {
        public int StudentId { get; set; }
        public List<Result> Results { get; set; }
        public float GPA { get; set; }
    }
}
