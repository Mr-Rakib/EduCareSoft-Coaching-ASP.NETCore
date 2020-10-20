using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Models
{
    public class Result
    {
        public int Id { get; set; }
        public ExamManager ExamManager { get; set; }
        public int StudentId { get; set; }
        public float Marks { get; set; }
        public DateTime Date { get; set; }
        public int EntryBy_id { get; set; }
    }
}
