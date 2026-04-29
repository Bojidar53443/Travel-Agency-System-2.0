using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_Agency_System_2._0.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string MainDestination { get; set; }
        public List<string> AdditionalStops { get; set; } = new List<string>();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxCapacity { get; set; }
        public decimal BasePrice { get; set; }
        public List<int> RegisteredClientIds { get; set; } = new List<int>();


        public int AvailableSeats => MaxCapacity - RegisteredClientIds.Count;
    }
}
