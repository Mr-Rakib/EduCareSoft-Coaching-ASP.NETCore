using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Models
{
    public class ExamManager
    {
        public int Id { get; set; }
        public ExamInformation ExamInformation { get; set; }
        public SubjectManager SubjectManager { get; set; }
        public GradingSystem GradingSystem { get; set; }
        public float FullMarks { get; set; }
        public DateTime ExamDate{ get; set; }
        public TimeSpan TimeStart{ get; set; }
        public TimeSpan TimeEnd{ get; set; }
        public string RoomNumber{ get; set; }
        public int ExamYear { get; set; }
        public int EntryBy_id { get; set; }
    }
}
