using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel_Agency_System_2._0.Data;

namespace Travel_Agency_System_2._0.Services
{
    internal class TripManager
    {
        public void CreateTrip(string destination, DateTime start, DateTime end, int capacity, decimal price)
        {
            int nextId = DataContext.Trips.Count + 1;
            var trip = new Trip
            {
                Id = nextId,
                MainDestination = destination,
                StartDate = start,
                EndDate = end,
                MaxCapacity = capacity,
                BasePrice = price
            };
            DataContext.Trips.Add(trip);
        }


        public void AddStopToTrip(int tripId, string stopName)
        {
            var trip = DataContext.Trips.FirstOrDefault(t => t.Id == tripId);
            if (trip != null)
            {
                trip.AdditionalStops.Add(stopName);
            }
        }


        public int GetAvailableSeats(int tripId)
        {
            var trip = DataContext.Trips.FirstOrDefault(t => t.Id == tripId);
            return trip != null ? trip.AvailableSeats : 0;
        }
    }
}
