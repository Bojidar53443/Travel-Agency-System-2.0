using System;
using System.Collections.Generic;
using System.Linq;
using Travel_Agency_System_2._0.Data;
using Travel_Agency_System_2._0.Models;

namespace Travel_Agency_System_2._0.Services
{
    internal class TripManager
    {
        public void CreateTrip(string destination, DateTime start, DateTime end, int capacity, decimal price)
        {
            int nextId = DataContext.Trips.Count > 0 ? DataContext.Trips.Max(t => t.Id) + 1 : 1;
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

        public List<Trip> GetAllTrips()
        {
            return DataContext.Trips;
        }

        public void DeleteTrip(int id)
        {
            var trip = DataContext.Trips.FirstOrDefault(t => t.Id == id);
            if (trip != null)
            {
                DataContext.Trips.Remove(trip);
            }
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
        public void SetPriceRules(int tripId, string season, string serviceType, decimal multiplier)
        {
            
            var trip = DataContext.Trips.FirstOrDefault(t => t.Id == tripId);

            if (trip != null)
            {
                
                trip.BasePrice *= multiplier;

                
            }
        }
        public bool ConfirmTripStatus(int tripId, int minParticipants)
        {
            
            var trip = DataContext.Trips.FirstOrDefault(t => t.Id == tripId);
            if (trip == null) return false;

            
            {
                return trip.RegisteredClientIds.Count >= minParticipants;
            }

            return false;
        }
    }
}