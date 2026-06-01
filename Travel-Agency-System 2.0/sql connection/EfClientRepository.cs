using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Travel_Agency_System_2._0.Interfaces;
using Travel_Agency_System_2._0.Models;
using Travel_Agency_System_2._0.sql_connection;

namespace Travel_Agency_System_2._0.Repositories
{
    public class EfClientRepository : IClientRepository
    {
        private readonly TravelAgencyDbContext context;

        public EfClientRepository(TravelAgencyDbContext context)
        {
            this.context = context;
        }

        public Client GetById(int id)
        {
            var client = context.Clients.FirstOrDefault(c => c.Id == id);

            if (client == null)
                throw new Exception("Client not found.");

            return client;
        }

        public IReadOnlyList<Client> GetAll()
        {
            return context.Clients.ToList();
        }

        public void Save(Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            context.Clients.Add(client);
            context.SaveChanges();
        }
        public void AddClient(Client client)
        {
            context.Clients.Add(client);
            context.SaveChanges(); 
        }
        public void Update(Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            context.Update(client);
            context.SaveChanges();
        }
    }
}