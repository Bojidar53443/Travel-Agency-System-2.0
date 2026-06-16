using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Travel_Agency_System_2._0.Enums;
using Travel_Agency_System_2._0.Models;
using Travel_Agency_System_2._0.sql_connection;

namespace Travel_Agency_System_2._0.Services
{
    internal class ReportService
    {
        private readonly TravelAgencyDbContext _context;

        public ReportService()
        {
            _context = new TravelAgencyDbContext();
        }

        public List<string> GetParticipantsForTrip(int tripId)
        {
            var trip = _context.Trips.AsNoTracking().FirstOrDefault(t => t.Id == tripId);
            if (trip == null) return new List<string> { "Пътуването не е намерено." };

            var paidBookingClientIds = _context.Bookings
                .AsNoTracking()
                .Where(b => b.TripId == tripId)
                .Where(b => _context.Payments.Any(p => p.BookingId == b.Id && p.Amount > 0))
                .Select(b => b.ClientId)
                .Distinct()
                .ToList();

            var participants = _context.Clients
                .AsNoTracking()
                .Where(c => paidBookingClientIds.Contains(c.Id))
                .Select(c => $"- ID: {c.Id} | {c.Name} {c.Surname} ({c.EmailAddress})")
                .ToList();

            if (!participants.Any())
            {
                return new List<string> { "Няма записани (и платени) участници за това пътуване." };
            }

            return participants;
        }

        public List<Trip> GetUpcomingTrips(DateTime start, DateTime end)
        {
            return _context.Trips
                .Where(t => t.StartDate.Date >= start.Date && t.StartDate.Date <= end.Date)
                .ToList();
        }

        public decimal GetRevenueReport(DateTime start, DateTime end)
        {
            var bookingsInPeriod = _context.Bookings
                .Include(b => b.Trip)
                .Include(b => b.ExtraServices)
                .Where(b => b.Status != BookingStatus.Canceled &&
                            b.Trip.StartDate.Date >= start.Date &&
                            b.Trip.StartDate.Date <= end.Date)
                .ToList();

            decimal totalRevenue = 0;

            foreach (var b in bookingsInPeriod)
            {
                decimal basePrice = b.Trip.BasePrice * b.PeopleCount;
                decimal extraPrice = b.ExtraServices != null ? b.ExtraServices.Sum(s => s.Price) : 0;
                totalRevenue += (basePrice + extraPrice);
            }

            return totalRevenue;
        }

        public Dictionary<string, int> GetTopDestinations()
        {
            return _context.Bookings
                .Include(b => b.Trip)
                .Where(b => b.Status != BookingStatus.Canceled && b.Trip != null)
                .AsEnumerable()
                .GroupBy(b => b.Trip.MainDestination)
                .ToDictionary(g => g.Key, g => g.Sum(b => b.PeopleCount))
                .OrderByDescending(p => p.Value)
                .ToDictionary(p => p.Key, p => p.Value);
        }
    }
}