using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public string InputDevice { get; set; }
        public int EntryBy_id { get; set; }
    }
}
