using System;
using System.Collections.Generic;

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
        public int AvailableSeats { get; set; }
        public decimal BasePrice { get; set; }
        public List<int> RegisteredClientIds { get; set; } = new List<int>();


        public string Season { get; set; }
        public string ServiceType { get; set; } 
        public decimal Price { get; set; } 

        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}