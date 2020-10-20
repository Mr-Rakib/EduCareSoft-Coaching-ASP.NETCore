using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Models
{
    public class ClassRoutine
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public SubjectManager SubjectManager { get; set; }
        public string Day { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public int RoomNumber { get; set; }
    }
}
