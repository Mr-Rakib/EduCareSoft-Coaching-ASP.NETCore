using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Models
{
    public class FeesCollection
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public float Fees { get; set; }
        public float Discount { get; set; }
        public float Remain { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime Date { get; set; }
        public int EntryById { get; set; }
        public string Status { get; set; }
    }
}
