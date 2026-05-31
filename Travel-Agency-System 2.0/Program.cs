using Travel_Agency_System_2._0.Interfaces;
using Travel_Agency_System_2._0.Repositories;
using Travel_Agency_System_2._0.Services;
using Travel_Agency_System_2._0.sql_connection;
using Travel_Agency_System_2._0.UI;
namespace Travel_Agency_System_2._0
{
    internal class Program
    {
        static void Main(string[] args)

        {
            
            using var context = new TravelAgencyDbContext();

            
            IClientRepository clientRepo = new EfClientRepository(context);
            IBookingRepository bookingRepo = new EfBookingRepository(context);
            ITripRepository tripRepo = new EfTripRepository(context);

            
            ClientManager clientManager = new ClientManager(clientRepo);
            TripManager tripManager = new TripManager(tripRepo);
            BookingManager bookingManager = new BookingManager(bookingRepo);

            
            MenuHandler menu = new MenuHandler(clientManager, tripManager, bookingManager);
            bool exit = false;

            while (!exit)
            {
                menu.ShowMainMenu();
                string choice = Console.ReadLine();


                switch (choice)
                {
                    case "1": menu.HandleClientMenu(); break;
                    case "2": menu.HandleTripMenu(); break;
                    case "3": menu.HandleBookingMenu(); break;
                    case "4": menu.HandleVoucherMenu(); break;
                    case "0": exit = true; break;
                    default: Console.WriteLine("Невалидна опция!"); break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nНатиснете клавиш за продължение...");
                    Console.ReadKey();
                }
            }
        }
    }
}
