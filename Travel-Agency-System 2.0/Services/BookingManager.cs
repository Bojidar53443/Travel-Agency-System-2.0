using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel_Agency_System_2._0.Data;

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


        public string CancelBooking(int bookingId)
        {
            var booking = DataContext.Bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking == null) return "Резервацията не е намерена.";

            var trip = DataContext.Trips.First(t => t.Id == booking.TripId);


            if ((trip.StartDate - DateTime.Now).TotalDays < 7)
            {
                booking.Status = BookingStatus.Canceled;
                return "Резервацията е анулирана с 20% неустойка.";
            }

            booking.Status = BookingStatus.Canceled;
            return "Резервацията е анулирана без неустойка.";
        }
    }
}
