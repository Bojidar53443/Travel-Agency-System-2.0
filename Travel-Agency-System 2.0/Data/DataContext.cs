using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_Agency_System_2._0.Data
{
    internal class DataContext
    {

        public static List<Client> Clients = new List<Client>();
        public static List<Trip> Trips = new List<Trip>();
        public static List<Booking> Bookings = new List<Booking>();
        public static List<Payment> Payments = new List<Payment>();
        public static List<Guide> Guides = new List<Guide>();


        private static int _clientIdCounter = 1;


        public static Client AddClient(string firstName, string lastName, string email, string phone)
        {
            var client = new Client
            {
                Id = _clientIdCounter++,
                Name = firstName,
                Surname = lastName,
                EmailAddress = email,
                PhoneNumber = phone
            };

            Clients.Add(client);
            return client;
        }
    }
}
