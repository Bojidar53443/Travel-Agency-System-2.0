using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var client = new Client
            {
                Name = firstName,
                Surname = lastName,
                EmailAddress = email,
                PhoneNumber = phone
            };

            _clientRepo.Save(client);
        }

        public bool UpdateClient(int clientId, string newPhone, string newEmail)
        {
            try
            {
                var client = _clientRepo.GetById(clientId);
                
                client.PhoneNumber = newPhone;
                client.EmailAddress = newEmail;

                _clientRepo.Update(client);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}