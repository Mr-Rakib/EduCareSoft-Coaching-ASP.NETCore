using System;

namespace eds_coaching_api_services.Models
{
    public class EntryInformation
    {
        public int Id { get; set; }
        public int EntryBy_id { get; set; }
        public DateTime EntryDate { get; set; }
        public int IsActive { get; set; }
    }
}

   
