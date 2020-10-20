using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Models
{
    public class GradingSystem
    {
        public int Id { get; set; }
        public string SystemName { get; set; }
        public int InstitutionId { get; set; }
        public int EntryBy_id { get; set; }
    }
}
