using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Travel_Agency_System_2._0.Interfaces;
using Travel_Agency_System_2._0.Models;
using Travel_Agency_System_2._0.sql_connection;

namespace Travel_Agency_System_2._0.Services
{
    internal class TripManager
    {
        private readonly ITripRepository _tripRepo;

        public TripManager(ITripRepository tripRepo)
        {
            _tripRepo = tripRepo;
        }

        public IReadOnlyList<Trip> GetAllTrips()
        {
            return _tripRepo.GetAll();
        }

        public void CreateTrip(string destination, DateTime start, DateTime end, int capacity, decimal price, string season, string serviceType)
        {
            var trip = new Trip
            {
                MainDestination = destination,
                StartDate = start,
                EndDate = end,
                MaxCapacity = capacity,
                AvailableSeats = capacity,
                BasePrice = price,
                Price = price,
                Season = season,
                ServiceType = serviceType,
                AdditionalStops = new List<string>()
            };

            _tripRepo.Save(trip);
        }

        public void SetPriceRules(int tripId, string season, string serviceType, decimal multiplier)
        {
            var trip = _tripRepo.GetById(tripId);
            if (trip != null)
            {
                trip.Season = season;
                trip.ServiceType = serviceType;
                trip.Price = trip.BasePrice * multiplier;

                _tripRepo.Update(trip);
            }
        }



        public string DeleteTrip(int tripId)
        {
            using (var db = new TravelAgencyDbContext())
            {
                
                var relatedBookings = db.Bookings.Where(b => b.TripId == tripId).ToList();
                if (relatedBookings.Any())
                {
                    db.Bookings.RemoveRange(relatedBookings);
                }

               
                var trip = db.Trips.FirstOrDefault(t => t.Id == tripId);
                if (trip == null) return "Пътуването не е намерено.";

                db.Trips.Remove(trip);
                db.SaveChanges(); 

                return "Пътуването и всички негови резервации бяха изтрити успешно!";
            }
        }

        public void AddStopToTrip(int tripId, string stopName)
        {
            var trip = _tripRepo.GetById(tripId);
            if (trip != null)
            {
                trip.AdditionalStops.Add(stopName);
                _tripRepo.Update(trip);
            }
        }

        public int GetAvailableSeats(int tripId)
        {
            var trip = _tripRepo.GetById(tripId);
            return trip != null ? trip.AvailableSeats : 0;
        }

        public IReadOnlyList<Trip> GetTripsByPeriod(DateTime startDate, DateTime endDate)
        {
            return _tripRepo.GetAll()
                .Where(t => t.StartDate.Date >= startDate.Date && t.EndDate.Date <= endDate.Date)
                .ToList();
        }


        public bool ConfirmTripStatus(int tripId, int minParticipants)
        {
            using (var db = new TravelAgencyDbContext())
            {
                var trip = db.Trips.AsNoTracking().FirstOrDefault(t => t.Id == tripId);
                if (trip == null) return false;

                var bookings = db.Bookings
                    .AsNoTracking()
                    .Where(b => b.TripId == tripId)
                    .ToList();

                int totalPaidPeople = 0;

                foreach (var booking in bookings)
                {
                    bool hasPaid = db.Payments
                        .AsNoTracking()
                        .Any(p => p.BookingId == booking.Id && p.Amount > 0);

                    if (hasPaid)
                    {
                        totalPaidPeople += booking.PeopleCount;
                    }
                }

                return totalPaidPeople >= minParticipants;
            }
        }
    }
}