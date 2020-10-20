using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eds_coaching_api_services.Models
{
    public class SubjectManager
    {
        public int Id { get; set; }
        public Subject Subject { get; set; }
        public Class Class { get; set; }
        public Batch Batch { get; set; }
        public float Fees { get; set; }
        public int Session { get; set; }
    }
}
