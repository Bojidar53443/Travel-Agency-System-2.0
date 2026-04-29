using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel_Agency_System_2._0.Data;

namespace Travel_Agency_System_2._0.Services
{
    internal class ClientManager
    {
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
