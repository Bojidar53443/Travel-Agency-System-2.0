using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Travel_Agency_System_2._0.Enums;
using Travel_Agency_System_2._0.Interfaces;
using Travel_Agency_System_2._0.Models;
using Travel_Agency_System_2._0.sql_connection;

namespace Travel_Agency_System_2._0.Services
{
    internal class BookingManager
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly ITripRepository _tripRepo;
        private readonly TravelAgencyDbContext _context;

        public BookingManager(IBookingRepository bookingRepo, ITripRepository tripRepo)
        {
            _bookingRepo = bookingRepo;
            _tripRepo = tripRepo;
            _context = new TravelAgencyDbContext();
        }

        public string MakeBooking(int clientId, int tripId, int peopleCount)
        {
            var trip = _tripRepo.GetById(tripId);
            if (trip == null) return "Пътуването не е намерено.";

            if (trip.AvailableSeats < peopleCount)
                return $"Няма достатъчно места! Свободни: {trip.AvailableSeats}";

            var booking = new Booking
            {
                ClientId = clientId,
                TripId = tripId,
                PeopleCount = peopleCount,
                Status = BookingStatus.Active
            };

            _bookingRepo.Save(booking);

            trip.AvailableSeats -= peopleCount;
            _tripRepo.Update(trip);

            return $"Резервацията беше създадена успешно с ID: {booking.Id}";
        }

        public string MakeGroupBooking(List<int> clientIds, int tripId)
        {
            var trip = _tripRepo.GetById(tripId);
            if (trip == null) return "Пътуването не е намерено.";

            if (trip.AvailableSeats < clientIds.Count)
                return $"Няма достатъчно места за групата! Свободни: {trip.AvailableSeats}";

            foreach (var clientId in clientIds)
            {
                var booking = new Booking
                {
                    ClientId = clientId,
                    TripId = tripId,
                    PeopleCount = 1,
                    Status = BookingStatus.Active
                };
                _bookingRepo.Save(booking);
            }

            trip.AvailableSeats -= clientIds.Count;
            _tripRepo.Update(trip);

            return $"Успешно бяха създадени {clientIds.Count} резервации за групата!";
        }

        public string AddExtraServiceToBooking(int bookingId, string serviceName, decimal price)
        {
            var booking = _context.Bookings
                .Include(b => b.ExtraServices)
                .FirstOrDefault(b => b.Id == bookingId);

            if (booking == null) return "Грешка: Резервацията не е намерена!";

            var newService = new ExtraService
            {
                Name = serviceName,
                Price = price,
                Description = "Допълнителна услуга"
            }; ;

            booking.ExtraServices.Add(newService);
            _context.SaveChanges();

            return $"Успешно добавихте услуга '{serviceName}' на стойност {price} лв. към Резервация #{bookingId}!";
        }

        public string UpdateBookingStatus(int bookingId, BookingStatus newStatus)
        {
            var booking = _bookingRepo.GetById(bookingId);
            if (booking == null) return "Резервацията не е намерена.";

            booking.Status = newStatus;
            _bookingRepo.Update(booking);

            return $"Статусът на резервация #{bookingId} беше променен на {newStatus}.";
        }

        public string CancelBooking(int bookingId)
        {
            var booking = _bookingRepo.GetById(bookingId);
            if (booking == null) return "Резервацията не е намерена.";

            if (booking.Status == BookingStatus.Canceled)
                return "Резервацията вече е анулирана.";

            booking.Status = BookingStatus.Canceled;
            _bookingRepo.Update(booking);

            var trip = _tripRepo.GetById(booking.TripId);
            if (trip != null)
            {
                trip.AvailableSeats += booking.PeopleCount;
                _tripRepo.Update(trip);
            }

            return $"Резервация #{bookingId} беше анулирана успешно.";
        }

        public List<Booking> GetClientTripHistory(int clientId)
        {
            return _context.Bookings
                .Include(b => b.Trip)
                .Include(b => b.ExtraServices)
                .Where(b => b.ClientId == clientId)
                .ToList();
        }
        public List<Client> GetClientsByTrip(int tripId)
        {
            using (var db = new TravelAgencyDbContext())
            {
                return db.Bookings
                    .Include(b => b.Client) 
                    .Where(b => b.TripId == tripId)
                    .Select(b => b.Client)
                    .Where(c => c != null)
                    .Distinct()
                    .ToList();
            }
        }
    }
}