using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Models
{
    public class Batch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Institution_id { get; set; }
    }
}
