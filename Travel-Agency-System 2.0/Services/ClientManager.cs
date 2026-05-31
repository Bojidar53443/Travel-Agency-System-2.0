using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel_Agency_System_2._0.Data;
using Travel_Agency_System_2._0.Interfaces;
using Travel_Agency_System_2._0.Models;
namespace Travel_Agency_System_2._0.Services
{
    internal class ClientManager
    {
        private readonly IClientRepository _clientRepo;

        public ClientManager(IClientRepository clientRepo)
        {
            _clientRepo = clientRepo;
        }
        public void RegisterClient(string firstName, string lastName, string email, string phone)
        {
            int nextId = DataContext.Clients.Count + 1;
            var client = new Client
            {
                Id = nextId,
                Name = firstName,
                Surname = lastName,
                EmailAddress = email,
                PhoneNumber = phone
            };
            DataContext.Clients.Add(client);
        }
       


        public bool UpdateClient(int clientId, string newPhone, string newEmail)
        {
            var client = DataContext.Clients.FirstOrDefault(c => c.Id == clientId);
            if (client != null)
            {
                client.PhoneNumber = newPhone;
                client.EmailAddress = newEmail;
                return true;
            }
            return false;
        }
    }
}
