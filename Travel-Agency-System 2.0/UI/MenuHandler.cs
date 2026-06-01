using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel_Agency_System_2._0.Enums;
using Travel_Agency_System_2._0.Reports;
using Travel_Agency_System_2._0.Services;
namespace Travel_Agency_System_2._0.UI
{
   
    internal class MenuHandler
    {
        
        private readonly ClientManager _clientMgr;
        private readonly TripManager _tripMgr;
        private readonly BookingManager _bookingMgr;

        
        private readonly PaymentService _paymentService;
        private readonly VoucherService _voucherService;
        private readonly ReportService _reportService;

        
        public MenuHandler(ClientManager clientMgr, TripManager tripMgr, BookingManager bookingMgr)
        {
            _clientMgr = clientMgr ?? throw new ArgumentNullException(nameof(clientMgr));
            _tripMgr = tripMgr ?? throw new ArgumentNullException(nameof(tripMgr));
            _bookingMgr = bookingMgr ?? throw new ArgumentNullException(nameof(bookingMgr));

            
            _paymentService = new PaymentService();
            _voucherService = new VoucherService();
            _reportService = new ReportService();
        }
        public void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("================================================");
            Console.WriteLine("   СИСТЕМА ЗА УПРАВЛЕНИЕ НА ТУРИСТИЧЕСКА АГЕНЦИЯ");
            Console.WriteLine("================================================");
            Console.WriteLine("1. Управление на Клиенти (Регистрация/Редакция/История)");
            Console.WriteLine("2. Управление на Пътувания (Създаване/Спирки/Цени)");
            Console.WriteLine("3. Резервации, Услуги и Плащания");
            Console.WriteLine("4. Справки, Отчети и Ваучери");
            Console.WriteLine("0. Изход");
            Console.WriteLine("------------------------------------------------");
            Console.Write("Изберете опция: ");
        }

        public void HandleClientMenu()
        {
            Console.Clear();
            Console.WriteLine("--- УПРАВЛЕНИЕ НА КЛИЕНТИ ---");
            Console.WriteLine("1. Регистрация на нов клиент");
            Console.WriteLine("2. Актуализиране на клиентски данни");
            Console.WriteLine("3. Проследяване история на пътуванията");
            Console.Write("Избор: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Въведете име: "); string fname = Console.ReadLine();
                Console.Write("Въведете фамилия: "); string lname = Console.ReadLine();
                Console.Write("Email: "); string email = Console.ReadLine();
                Console.Write("Телефон: "); string phone = Console.ReadLine();

                _clientMgr.RegisterClient(fname, lname, email, phone);
                Console.WriteLine("\n✅ Клиентът е регистриран успешно!");
            }
            else if (choice == "2")
            {
                Console.Write("Въведете ID на клиента за редактиране: ");
                if (!int.TryParse(Console.ReadLine(), out int id)) return;
                Console.Write("Нов Телефон: "); string phone = Console.ReadLine();
                Console.Write("Нов Email: "); string email = Console.ReadLine();    

                bool success = _clientMgr.UpdateClient(id, phone, email);
                Console.WriteLine(success ? "\n✅ Данните бяха обновени!" : "\n❌ Клиентът не е намерен!");
            }
            else if (choice == "3")
            {
                Console.Write("Въведете ID на клиент: ");
                if (!int.TryParse(Console.ReadLine(), out int id)) return;

                var history = _bookingMgr.GetClientTripHistory(id);
                Console.WriteLine($"\n--- ИСТОРИЯ НА ПЪТУВАНИЯТА ЗА КЛИЕНТ #{id} ---");
                if (!history.Any()) Console.WriteLine("Няма намерени пътувания.");
                foreach (var trip in history)
                {
                    Console.WriteLine($"- [{trip.StartDate.ToShortDateString()}] {trip.MainDestination}");
                }
            }
        }

        public void HandleTripMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("=== УПРАВЛЕНИЕ НА ПЪТУВАНИЯ ===");
                Console.WriteLine("1. Добави нов пътуване");
                Console.WriteLine("2. Прегледай всички пътувания");
                Console.WriteLine("3. Изтрий пътуване");
                Console.WriteLine("4. Добави допълнителни дестинации / спирки");
                Console.WriteLine("5. Дефинирай сезонна цена / тип услуга");
                Console.WriteLine("6. Проверка за свободни места");
                Console.WriteLine("0. Назад");
                Console.WriteLine("-------------------------------");
                Console.Write("Избор: ");

                string tripChoice = Console.ReadLine();

                switch (tripChoice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("--- ДОБАВЯНЕ НА ПЪТУВАНЕ ---");

                        Console.Write("Основна дестинация: ");
                        string dest = Console.ReadLine();

                        Console.Write("Начална дата (гггг-мм-дд): ");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate)) break;

                        Console.Write("Крайна дата (гггг-мм-дд): ");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate)) break;

                        Console.Write("Максимален капацитет: ");
                        if (!int.TryParse(Console.ReadLine(), out int capacity)) break;

                        Console.Write("Базова цена: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal price)) break;

                        Console.Write("Сезон (Low/Mid/High или Лято/Зима): ");
                        string season = Console.ReadLine();

                        Console.Write("Въведете допълнителни дестинации (разделени със запетая, или празно): ");
                        string stopsInput = Console.ReadLine();
                        List<string> stops = !string.IsNullOrWhiteSpace(stopsInput)
                            ? stopsInput.Split(',').Select(s => s.Trim()).ToList()
                            : new List<string>();

                        _tripMgr.CreateTrip(dest, startDate, endDate, capacity, price, season, stops);
                        Console.WriteLine("\n✅ Пътуването е добавено успешно!");
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("--- СПИСЪК С ВСИЧКИ ПЪТУВАНИЯ ---");
                        var trips = _tripMgr.GetAllTrips();

                        if (trips.Count == 0)
                        {
                            Console.WriteLine("Няма регистрирани пътувания.");
                        }
                        else
                        {
                            foreach (var t in trips)
                            {
                                Console.WriteLine($"ID: {t.Id} | Дестинация: {t.MainDestination} | Сезон: {t.Season} | Услуга: {t.ServiceType} | Места: {t.AvailableSeats} | Крайна Цена: {t.Price:F2} евро.");
                            }
                        }
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("--- ИЗТРИВАНЕ НА ПЪТУВАНЕ ---");
                        Console.Write("Въведете ID на пътуването: ");
                        if (int.TryParse(Console.ReadLine(), out int idToDelete))
                        {
                            _tripMgr.DeleteTrip(idToDelete);
                            Console.WriteLine("\n🗑️ Пътуването беше премахнато (ако е съществувало).");
                        }
                        break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("--- ДОБАВЯНЕ НА СПИРКА КЪМ ПЪТУВАНЕ ---");
                        Console.Write("ID на пътуване: ");
                        if (!int.TryParse(Console.ReadLine(), out int tId)) break;
                        Console.Write("Име на спирка/дестинация за добавяне: ");
                        string stop = Console.ReadLine();

                        _tripMgr.AddStopToTrip(tId, stop);
                        Console.WriteLine("✅ Спирката е добавена успешно!");
                        break;

                    case "5":
                        Console.Clear();
                        Console.WriteLine("--- ДЕФИНИРАНЕ НА ПРАВИЛА ЗА ЦЕНА ---");
                        Console.Write("ID на пътуване: ");
                        if (!int.TryParse(Console.ReadLine(), out int tripId)) break;
                        Console.Write("Сезон (Low/Mid/High): ");
                        string seasonRule = Console.ReadLine();
                        Console.Write("Тип услуга (Standard/Premium/AllInclusive): ");
                        string serviceType = Console.ReadLine();
                        Console.Write("Коефициент на цената (напр. 1.2 за +20%): ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal multiplier)) break;

                        _tripMgr.SetPriceRules(tripId, seasonRule, serviceType, multiplier);
                        Console.WriteLine("✅ Правилото за ценообразуване е запазено!");
                        break;

                    case "6":
                        Console.Clear();
                        Console.WriteLine("--- ПРОВЕРКА ЗА СВОБОДНИ МЕСТА ---");
                        Console.Write("ID на пътуване: ");
                        if (!int.TryParse(Console.ReadLine(), out int checkId)) break;

                        int freeSeats = _bookingMgr.GetAvailableSeatsForTrip(checkId);
                        Console.WriteLine($"\nСвободни места за пътуването: {freeSeats}");
                        break;

                    case "0":
                        back = true;
                        break;

                    default:
                        Console.WriteLine("Невалидна опция!");
                        break;
                }

                if (!back)
                {
                    Console.WriteLine("\nНатиснете клавиш за връщане към менюто...");
                    Console.ReadKey();
                }
            }
        }
        public void HandleBookingMenu()
        {
            Console.Clear();
            Console.WriteLine("--- РЕЗЕРВАЦИИ И ПЛАЩАНИЯ ---");
            Console.WriteLine("1. Индивидуална резервация");
            Console.WriteLine("2. Групова резервация (няколко клиенти)");
            Console.WriteLine("3. Добавяне на допълнителни услуги към резервация");
            Console.WriteLine("4. Промяна на статус на резервация / Отмяна (Неустойка)");
            Console.WriteLine("5. Регистриране на плащане");
            Console.Write("Избор: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                
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
                    break;
                    

                case "2":
                    Console.Write("ID на пътуване: "); int tripId = int.Parse(Console.ReadLine());
                    Console.Write("Въведете ID-та на клиентите, разделени със запетая (напр. 1,2,3): ");
                    string clientIdsInput = Console.ReadLine();
                    List<int> clientIds = clientIdsInput.Split(',').Select(int.Parse).ToList();

                    string groupRes = _bookingMgr.MakeGroupBooking(clientIds, tripId);
                    Console.WriteLine($"\n📢 Резултат: {groupRes}");
                    break;

                case "3":
                    Console.Write("ID на резервация: "); int bId = int.Parse(Console.ReadLine());
                    Console.Write("Тип услуга (Екскурзия/Застраховка): "); string serviceName = Console.ReadLine();
                    Console.Write("Цена на услугата: "); decimal sPrice = decimal.Parse(Console.ReadLine());
                    _bookingMgr.AddExtraServiceToBooking(bId, serviceName, sPrice);
                    Console.WriteLine("✅ Допълнителната услуга е добавена към резервацията.");
                    break;

                case "4":
                    Console.Clear();
                    Console.Write("ID на резервация: "); int resId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Изберете нов статус: 1. Active, 2. Cancelled, 3. Completed");
                    string statusChoice = Console.ReadLine();

                    if (statusChoice == "2")
                    {
                        string cancelResult = _bookingMgr.CancelBooking(resId);
                        Console.WriteLine($"\n🛑 {cancelResult}");
                    }
                    else
                    {
                        BookingStatus targetStatus = statusChoice == "3" ? BookingStatus.Completed : BookingStatus.Active;

                        bool statusResult = _bookingMgr.UpdateBookingStatus(resId, targetStatus);
                        if (statusResult)
                            Console.WriteLine($"\n✅ Статусът е променен на: {targetStatus}");
                        else
                            Console.WriteLine("\n❌ Резервацията не е намерена!");
                    }
                    break;

                case "5":
                    Console.Clear();
                    Console.WriteLine("--- РЕГИСТРИРАНЕ НА ПЛАЩАНЕ ---");
                    Console.Write("ID на резервация: ");
                    int payId = int.Parse(Console.ReadLine());
                    Console.Write("Сума за плащане: ");
                    decimal amount = decimal.Parse(Console.ReadLine());
                    Console.Write("Метод (Карта/В брой): ");
                    string method = Console.ReadLine();

                    string status = _paymentService.ProcessPayment(payId, amount, method);
                    Console.WriteLine($"\n📢 {status}");
                    break;
                    
            }
        }

        public void HandleVoucherMenu()
        {
            Console.Clear();
            Console.WriteLine("--- СПРАВКИ, ОТЧЕТИ И ВАУЧЕРИ ---");
            Console.WriteLine("1. Генериране на туристически ваучер");
            Console.WriteLine("2. Списък с участници за конкретно пътуване");
            Console.WriteLine("3. Справка за предстоящи пътувания в период");
            Console.WriteLine("4. Отчет за приходи от пътувания по период");
            Console.WriteLine("5. Статистика за най-търсени дестинации");
            Console.WriteLine("6. Потвърждаване на пътуване (Минимум участници)");
            Console.Write("Избор: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.Write("Въведете ID на резервация: ");
                    int bId = int.Parse(Console.ReadLine());

                    
                    Console.WriteLine("\n" + _voucherService.GenerateVoucher(bId));
                    break;

                case "2":
                    Console.Clear();
                    Console.Write("ID на пътуване: ");
                    int tId = int.Parse(Console.ReadLine());

                    
                    var participants = _reportService.GetParticipantsForTrip(tId);
                    Console.WriteLine($"\n--- СПИСЪК С УЧАСТНИЦИ ЗА ПЪТУВАНЕ #{tId} ---");
                    participants.ForEach(Console.WriteLine);
                    break;

                case "3":
                    Console.Clear();
                    Console.Write("Начална дата (гггг-мм-дд): ");
                    DateTime start = DateTime.Parse(Console.ReadLine());
                    Console.Write("Крайна дата (гггг-мм-дд): ");
                    DateTime end = DateTime.Parse(Console.ReadLine());

                    
                    var upcoming = _reportService.GetUpcomingTrips(start, end);
                    Console.WriteLine("\n--- ПРЕДСТОЯЩИ ПЪТУВАНИЯ ---");
                    foreach (var t in upcoming)
                    {
                        
                        Console.WriteLine($"- [{t.StartDate.ToShortDateString()}] {t.MainDestination} - Оставаат места: {t.AvailableSeats}");
                    }
                    break;

                case "4":
                    Console.Clear();
                    Console.Write("Начална дата за отчет: ");
                    DateTime reportStart = DateTime.Parse(Console.ReadLine());
                    Console.Write("Крайна дата за отчет: ");
                    DateTime reportEnd = DateTime.Parse(Console.ReadLine());

                    
                    decimal revenue = _reportService.GetRevenueReport(reportStart, reportEnd);
                    Console.WriteLine($"\n💰 Общи приходи за периода: {revenue:F2} лв.");
                    break;

                case "5":
                    Console.Clear();
                    Console.WriteLine("\n--- НАЙ-ТЪРСЕНИ ДЕСТИНАЦИИ (ТОП) ---");

                   
                    var stats = _reportService.GetTopDestinations();
                    foreach (var pair in stats)
                    {
                        Console.WriteLine($"📍 {pair.Key}: {pair.Value} резервирани места");
                    }
                    break;

                case "6":
                    Console.Clear();
                    Console.Write("ID на пътуване: ");
                    int tripId = int.Parse(Console.ReadLine());
                    Console.Write("Минимален брой участници за потвърждение: ");
                    int min = int.Parse(Console.ReadLine());

                    
                    bool confirmed = _tripMgr.ConfirmTripStatus(tripId, min);

                    Console.WriteLine();
                    if (confirmed)
                    {
                        Console.WriteLine("✅ Пътуването е ПОТВЪРДЕНО! Достигнат е минимума.");
                    }
                    else
                    {
                        Console.WriteLine("⏳ Пътуването все още не е събрало минимум участници.");
                    }
                    break;
            }
        }


    }
}
