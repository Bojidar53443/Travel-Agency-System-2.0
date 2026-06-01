using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Travel_Agency_System_2._0.Interfaces;
using Travel_Agency_System_2._0.Models;
using Travel_Agency_System_2._0.sql_connection;

namespace Travel_Agency_System_2._0.Repositories
{
    public class EfTripRepository : ITripRepository
    {
        private readonly TravelAgencyDbContext context;

        public EfTripRepository(TravelAgencyDbContext context)
        {
            this.context = context;
        }

        
        public Trip GetById(int id)
        {
            var trip = context.Trips.FirstOrDefault(t => t.Id == id);

            if (trip == null)
                throw new Exception("Trip not found.");

            return trip;
        }

        public IReadOnlyList<Trip> GetAll()
        {
            return context.Trips.ToList();
        }

        public void Save(Trip trip)
        {
            if (trip == null)
                throw new ArgumentNullException(nameof(trip));

            context.Trips.Add(trip);
            context.SaveChanges();

        }
        public void AddTrip(Trip trip)
        {
            context.Trips.Add(trip);
            context.SaveChanges(); 
        }

        public void Update(Trip trip)
        {
            if (trip == null)
                throw new ArgumentNullException(nameof(trip));

            context.Update(trip);
            context.SaveChanges();
        }
    }
}