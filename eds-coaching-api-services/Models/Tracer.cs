using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Models
{
    public class Tracer
    {
        public int Id { get; set; }
        public int Actor_id { get; set; }
        public string ActionName { get; set; }
        public string TableName { get; set; }
        public int ActionApplied_id { get; set; }
        public DateTime ActionTime { get; set; }
        public int Institution_id { get; set; }

    }
}
