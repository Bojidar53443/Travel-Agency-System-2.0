namespace Travel_Agency_System_2._0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuHandler menu = new MenuHandler();
            bool exit = false;

            while (!exit)
            {
                menu.ShowMainMenu();
                string choice = Console.ReadLine();


                switch (choice)
                {
                    case "1": menu.HandleClientMenu(); break;
                    case "2": /* Можеш да добавиш метод за пътувания */ break;
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
