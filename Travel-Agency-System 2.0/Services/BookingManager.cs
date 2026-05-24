using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel_Agency_System_2._0.Data;
using Travel_Agency_System_2._0.Enums;
using Travel_Agency_System_2._0.Models;

namespace Travel_Agency_System_2._0.Services
{
    internal class BookingManager
    {
        
        public string MakeBooking(int clientId, int tripId, int peopleCount)
        {
            var trip = DataContext.Trips.FirstOrDefault(t => t.Id == tripId);
            if (trip == null) return "Пътуването не е намерено.";

            if (trip.AvailableSeats < peopleCount)
                return $"Няма достатъчно места! Свободни: {trip.AvailableSeats}";

            var booking = new Booking
            {
                Id = DataContext.Bookings.Count + 1,
                ClientId = clientId,
                TripId = tripId,
                PeopleCount = peopleCount,
                Status = BookingStatus.Active
            };

            DataContext.Bookings.Add(booking);

            for (int i = 0; i < peopleCount; i++)
            {
                trip.RegisteredClientIds.Add(clientId);
            }

            return "Резервацията е успешна!";
        }

        
        public string MakeGroupBooking(List<int> clientIds, int tripId)
        {
            var trip = DataContext.Trips.FirstOrDefault(t => t.Id == tripId);
            if (trip == null) return "Пътуването не е намерено.";

            int peopleCount = clientIds.Count;
            if (trip.AvailableSeats < peopleCount)
                return $"Няма достатъчно места за цялата група! Свободни: {trip.AvailableSeats}";

            
            var booking = new Booking
            {
                Id = DataContext.Bookings.Count + 1,
                ClientId = clientIds.First(),
                TripId = tripId,
                PeopleCount = peopleCount,
                Status = BookingStatus.Active
            };

            

            DataContext.Bookings.Add(booking);

            
            foreach (var id in clientIds)
            {
                trip.RegisteredClientIds.Add(id);
            }

            return $"Груповата резервация е успешна! Генерирано ID: {booking.Id}";
        }


        public string AddExtraServiceToBooking(int bookingId, string serviceName, decimal price, string description = "")
        {
            var booking = DataContext.Bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking == null) return "Резервацията не е намерена.";

            if (booking.Status == BookingStatus.Canceled)
                return "Не може да добавяте услуги към анулирана резервация.";

            if (booking.ExtraServices == null)
            {
                booking.ExtraServices = new List<ExtraService>();
            }

            
            int nextId = booking.ExtraServices.Count + 1;

            
            booking.ExtraServices.Add(new ExtraService
            {
                Id = nextId,
                Name = serviceName,
                Price = price,
                Description = description,
                RelatedTripId = booking.TripId 
            });

            return $"Успешно добавена услуга: {serviceName} ({price:F2} лв.)";
        }

        
        public decimal CalculateTotalAmount(int bookingId)
        {
            var booking = DataContext.Bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking == null) return 0;

            var trip = DataContext.Trips.FirstOrDefault(t => t.Id == booking.TripId);
            if (trip == null) return 0;

            
            decimal totalAmount = trip.BasePrice * booking.PeopleCount;

            

            
            if (booking.ExtraServices != null)
            {
                totalAmount += booking.ExtraServices.Sum(s => s.Price);
            }

            return totalAmount;
        }


        public string CancelBooking(int bookingId)
        {
            var booking = DataContext.Bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking == null) return "Резервацията не е намерена.";

            if (booking.Status == BookingStatus.Canceled)
                return "Резервацията вече е анулирана.";

            var trip = DataContext.Trips.FirstOrDefault(t => t.Id == booking.TripId);
            if (trip == null) return "Пътуването не е намерено.";

            booking.Status = BookingStatus.Canceled;

            if (trip.RegisteredClientIds != null)
            {
                for (int i = 0; i < booking.PeopleCount; i++)
                {
                    trip.RegisteredClientIds.Remove(booking.ClientId);
                }
            }

            decimal totalCost = CalculateTotalAmount(bookingId);

            
            if ((trip.StartDate - DateTime.Now).TotalDays < 7)
            {
                decimal penalty = totalCost * 0.20m;
                return $"Резервацията е анулирана с 20% неустойка. Дължима глоба: {penalty:F2} лв.";
            }

            return "Резервацията е анулирана успешно без неустойка.";
        }


        public bool UpdateBookingStatus(int bookingId, BookingStatus newStatus)
        {
            var booking = DataContext.Bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking == null) return false;

            booking.Status = newStatus;

            
            if (newStatus == BookingStatus.Canceled)
            {
                var trip = DataContext.Trips.FirstOrDefault(t => t.Id == booking.TripId);
                if (trip != null && trip.RegisteredClientIds != null)
                {
                    for (int i = 0; i < booking.PeopleCount; i++)
                    {
                        trip.RegisteredClientIds.Remove(booking.ClientId);
                    }
                }
            }

            return true;
        }

        
        public List<Trip> GetClientTripHistory(int clientId)
        {
            
            var tripIds = DataContext.Bookings
                .Where(b => b.ClientId == clientId && b.Status != BookingStatus.Canceled)
                .Select(b => b.TripId)
                .Distinct()
                .ToList();

            
            return DataContext.Trips
                .Where(t => tripIds.Contains(t.Id))
                .ToList();
        }

        
        public int GetAvailableSeatsForTrip(int tripId)
        {
            var trip = DataContext.Trips.FirstOrDefault(t => t.Id == tripId);
            if (trip == null) return 0;

            return trip.AvailableSeats;
        }
    }
}