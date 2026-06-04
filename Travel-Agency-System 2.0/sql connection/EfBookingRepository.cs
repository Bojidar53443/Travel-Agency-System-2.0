using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Travel_Agency_System_2._0.Interfaces;
using Travel_Agency_System_2._0.Models;
using Travel_Agency_System_2._0.sql_connection;


namespace Travel_Agency_System_2._0.Repositories
{
    public class EfBookingRepository : IBookingRepository
    {
        private readonly TravelAgencyDbContext context;

        public EfBookingRepository(TravelAgencyDbContext context)
        {
            this.context = context;
        }

        public Booking GetById(int id)
        {
            var booking = context.Bookings
                .Include(b => b.Client)
                .Include(b => b.Trip)
                .Include(b => b.ExtraServices)
                .FirstOrDefault(b => b.Id == id);

            if (booking == null)
                throw new Exception("Booking not found.");

            return booking;
        }

        public IReadOnlyList<Booking> GetAll()
        {
            return context.Bookings
                .Include(b => b.Client)
                .Include(b => b.Trip)
                .Include(b => b.ExtraServices)
                .ToList();
        }

        public void Save(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking));

            context.Bookings.Add(booking);
            context.SaveChanges();
        }

        public void Update(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking));

            context.Update(booking);
            context.SaveChanges();
        }

        public IReadOnlyList<Booking> GetByClient(int clientId)
        {
            return context.Bookings
                .Include(b => b.Trip)
                .Include(b => b.ExtraServices)
                .Where(b => b.ClientId == clientId)
                .ToList();
        }

        public void Delete(int id)
        {
            var booking = context.Bookings.FirstOrDefault(b => b.Id == id);
            if (booking != null)
            {
                context.Bookings.Remove(booking);
                context.SaveChanges();
            }
        }
    }
}