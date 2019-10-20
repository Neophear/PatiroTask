using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatiroTask.Models
{
    public class Clinic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public IEnumerable<string> Members { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}