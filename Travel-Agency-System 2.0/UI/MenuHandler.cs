using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel_Agency_System_2._0.Reports;
using Travel_Agency_System_2._0.Services;
namespace Travel_Agency_System_2._0.UI
{
    internal class MenuHandler
    {

        private readonly ClientManager _clientMgr = new ClientManager();
        private readonly TripManager _tripMgr = new TripManager();
        private readonly BookingManager _bookingMgr = new BookingManager();
        private readonly PaymentService _paymentService = new PaymentService();
        private readonly VoucherService _voucherService = new VoucherService();

        public void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("================================================");
            Console.WriteLine("   СИСТЕМА ЗА УПРАВЛЕНИЕ НА ТУРИСТИЧЕСКА АГЕНЦИЯ");
            Console.WriteLine("================================================");
            Console.WriteLine("1. Управление на Клиенти (Регистрация/Редакция)");
            Console.WriteLine("2. Управление на Пътувания (Създаване/Спирки)");
            Console.WriteLine("3. Резервации и Плащания");
            Console.WriteLine("4. Справки и Ваучери");
            Console.WriteLine("0. Изход");
            Console.WriteLine("------------------------------------------------");
            Console.Write("Изберете опция: ");
        }

        public void HandleClientMenu()
        {
            Console.Clear();
            Console.WriteLine("--- УПРАВЛЕНИЕ НА КЛИЕНТИ ---");
            Console.Write("Въведете име: "); string fname = Console.ReadLine();
            Console.Write("Въведете фамилия: "); string lname = Console.ReadLine();
            Console.Write("Email: "); string email = Console.ReadLine();
            Console.Write("Телефон: "); string phone = Console.ReadLine();

            _clientMgr.RegisterClient(fname, lname, email, phone);
            Console.WriteLine("\n✅ Клиентът е регистриран успешно!");
        }

        public void HandleBookingMenu()
        {
            Console.Clear();
            Console.WriteLine("--- НОВА РЕЗЕРВАЦИЯ ---");
            Console.Write("ID на клиент: ");
            if (!int.TryParse(Console.ReadLine(), out int cId)) return;

            Console.Write("ID на пътуване: ");
            if (!int.TryParse(Console.ReadLine(), out int tId)) return;

            Console.Write("Брой хора: ");
            if (!int.TryParse(Console.ReadLine(), out int count)) return;

            string result = _bookingMgr.MakeBooking(cId, tId, count);
            Console.WriteLine($"\n📢 Резултат: {result}");
        }

        public void HandlePaymentMenu()
        {
            Console.Clear();
            Console.WriteLine("--- РЕГИСТРИРАНЕ НА ПЛАЩАНЕ ---");
            Console.Write("ID на резервация: ");
            int bId = int.Parse(Console.ReadLine());
            Console.Write("Сума за плащане: ");
            decimal amount = decimal.Parse(Console.ReadLine());
            Console.Write("Метод (Карта/В брой): ");
            string method = Console.ReadLine();

            string status = _paymentService.ProcessPayment(bId, amount, method);
            Console.WriteLine($"\n📢 {status}");
        }

        public void HandleVoucherMenu()
        {
            Console.Clear();
            Console.WriteLine("--- ГЕНЕРИРАНЕ НА ВАУЧЕР ---");
            Console.Write("Въведете ID на резервация: ");
            int bId = int.Parse(Console.ReadLine());

            string voucher = _voucherService.GenerateVoucher(bId);
            Console.WriteLine("\n" + voucher);
        }
        public void HandleTripMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("=== Меню за пътувания ===");
                Console.WriteLine("1. Добави пътуване");
                Console.WriteLine("2. Прегледай всички пътувания");
                Console.WriteLine("3. Редактирай пътуване");
                Console.WriteLine("4. Изтрий пътуване");
                Console.WriteLine("0. Назад");
                Console.Write("Избор: ");

                string tripChoice = Console.ReadLine();

                switch (tripChoice)
                {
                    case "1": /* логика за добавяне на пътуване */ break;
                    case "2": /* показване на списък */ break;
                    case "3": /* редактиране */ break;
                    case "4": /* изтриване */ break;
                    case "0": back = true; break;
                    default: Console.WriteLine("Невалидна опция!"); break;
                }

                if (!back)
                {
                    Console.WriteLine("\nНатиснете клавиш за продължение...");
                    Console.ReadKey();
                }
            }
        }
    }
}
