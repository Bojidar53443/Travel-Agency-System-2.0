using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Travel_Agency_System_2._0.Models;
using Travel_Agency_System_2._0.sql_connection;

namespace Travel_Agency_System_2._0.Reports
{
    internal class ReportGenerator
    {
        private readonly TravelAgencyDbContext _context;

        public ReportGenerator()
        {
            _context = new TravelAgencyDbContext();
        }

        public List<string> GetParticipantList(int tripId)
        {
            var trip = _context.Trips.FirstOrDefault(t => t.Id == tripId);
            if (trip == null) return new List<string>();

            return _context.Clients
                .Where(c => trip.RegisteredClientIds.Contains(c.Id))
                .Select(c => $"{c.Name} {c.Surname}")
                .ToList();
        }

        public decimal GetTotalRevenue(DateTime start, DateTime end)
        {
            return _context.Payments
                .Where(p => p.Date.Date >= start.Date && p.Date.Date <= end.Date)
                .Sum(p => p.Amount);
        }

        public string GetMostPopularDestination()
        {
            var mostPopular = _context.Bookings
                .GroupBy(b => b.TripId)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            if (mostPopular == null) return "Няма данни.";

            var trip = _context.Trips.FirstOrDefault(t => t.Id == mostPopular.Key);
            return trip != null ? trip.MainDestination : "Няма данни.";
        }
    }
}