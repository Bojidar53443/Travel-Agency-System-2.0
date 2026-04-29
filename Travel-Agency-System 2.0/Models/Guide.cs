using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_Agency_System_2._0.Models
{
    internal class Guide
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public List<int> AssignedTripIds { get; set; } = new List<int>();
    }
}
