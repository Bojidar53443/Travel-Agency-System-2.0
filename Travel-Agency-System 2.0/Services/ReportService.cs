using System;
using System.Collections.Generic;
using System.Linq;
using Travel_Agency_System_2._0.Data;
using Travel_Agency_System_2._0.Enums;
using Travel_Agency_System_2._0.Models;

namespace Travel_Agency_System_2._0.Services
{
    internal class ReportService
    {
        
        public List<string> GetParticipantsForTrip(int tripId)
        {
            var trip = DataContext.Trips.FirstOrDefault(t => t.Id == tripId);
            if (trip == null) return new List<string> { "Пътуването не е намерено." };

            
            var participants = DataContext.Clients
                .Where(c => trip.RegisteredClientIds.Contains(c.Id))
                .Select(c => $"- ID: {c.Id} | {c.Name} ({c.EmailAddress})")
                .ToList();

            if (!participants.Any())
            {
                return new List<string> { "Няма записани участници за това пътуване." };
            }

            return participants;
        }

        public List<Trip> GetUpcomingTrips(DateTime start, DateTime end)
        {
            return DataContext.Trips
                .Where(t => t.StartDate >= start && t.StartDate <= end)
                .ToList();
        }

        public decimal GetRevenueReport(DateTime start, DateTime end)
        {
            return DataContext.Bookings
                .Where(b => b.Status != BookingStatus.Canceled)
                .Sum(b => {
                    var trip = DataContext.Trips.FirstOrDefault(t => t.Id == b.TripId);
                    if (trip != null && trip.StartDate >= start && trip.StartDate <= end)
                    {
                        decimal basePrice = trip.BasePrice * b.PeopleCount;
                        decimal extraPrice = b.ExtraServices != null ? b.ExtraServices.Sum(s => s.Price) : 0;
                        return basePrice + extraPrice;
                    }
                    return 0;
                });
        }

        public Dictionary<string, int> GetTopDestinations()
        {
            return DataContext.Bookings
                .Where(b => b.Status != BookingStatus.Canceled)
                .GroupBy(b => DataContext.Trips.FirstOrDefault(t => t.Id == b.TripId)?.MainDestination)
                .Where(g => g.Key != null)
                .ToDictionary(g => g.Key, g => g.Sum(b => b.PeopleCount))
                .OrderByDescending(p => p.Value)
                .ToDictionary(p => p.Key, p => p.Value);
        }
    }
}