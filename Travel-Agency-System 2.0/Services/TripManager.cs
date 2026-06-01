using System;
using System.Collections.Generic;
using System.Linq;
using Travel_Agency_System_2._0.Interfaces;
using Travel_Agency_System_2._0.Models;

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

        public void CreateTrip(string destination, DateTime start, DateTime end, int capacity, decimal price)
        {
            var trip = new Trip
            {
                MainDestination = destination,
                StartDate = start,
                EndDate = end,
                MaxCapacity = capacity,
                BasePrice = price
            };

            _tripRepo.Save(trip);
        }

        public IReadOnlyList<Trip> GetTripsByPeriod(DateTime startDate, DateTime endDate)
        {
            return _tripRepo.GetAll()
                .Where(t => t.StartDate.Date >= startDate.Date && t.EndDate.Date <= endDate.Date)
                .ToList();
        }

        public void DeleteTrip(int id)
        {
            var trip = _tripRepo.GetById(id);
            if (trip != null)
            {
                _tripRepo.Delete(trip);
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

        public void SetPriceRules(int tripId, string season, string serviceType, decimal multiplier)
        {
            var trip = _tripRepo.GetById(tripId);
            if (trip != null)
            {
                trip.BasePrice *= multiplier;
                _tripRepo.Update(trip);
            }
        }

        public bool ConfirmTripStatus(int tripId, int minParticipants)
        {
            var trip = _tripRepo.GetById(tripId);
            if (trip == null) return false;

            return trip.RegisteredClientIds.Count >= minParticipants;
        }
    }
}