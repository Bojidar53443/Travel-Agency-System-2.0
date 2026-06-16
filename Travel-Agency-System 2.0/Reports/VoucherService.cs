using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Travel_Agency_System_2._0.Models;
using Travel_Agency_System_2._0.sql_connection;

namespace Travel_Agency_System_2._0.Reports
{
    internal class VoucherService
    {
        private readonly TravelAgencyDbContext _context;

        public VoucherService()
        {
            _context = new TravelAgencyDbContext();
        }

        public string GenerateVoucher(int bookingId)
        {
            var booking = _context.Bookings
                .AsNoTracking()
                .FirstOrDefault(b => b.Id == bookingId);

            if (booking == null) return "Грешка: Резервацията не е намерена!";

            var client = _context.Clients
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == booking.ClientId);

            var trip = _context.Trips
                .AsNoTracking()
                .FirstOrDefault(t => t.Id == booking.TripId);

            if (client == null || trip == null) return "Грешка: Непълни данни за ваучера!";

            decimal paidAmount = _context.Payments
                .AsNoTracking()
                .Where(p => p.BookingId == bookingId)
                .Sum(p => (decimal?)p.Amount) ?? 0m;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("**************************************************");
            sb.AppendLine("           ТУРИСТИЧЕСКИ ВАУЧЕР (OFFICIAL)         ");
            sb.AppendLine("**************************************************");
            sb.AppendLine($" Номер на ваучер: {booking.Id:D5}");
            sb.AppendLine($" Дата на издаване: {DateTime.Now:dd/MM/yyyy}");
            sb.AppendLine("--------------------------------------------------");
            sb.AppendLine($" КЛИЕНТ: {client.Name} {client.Surname}");
            sb.AppendLine($" ТЕЛЕФОН: {client.PhoneNumber}");
            sb.AppendLine("--------------------------------------------------");
            sb.AppendLine($" ДЕСТИНАЦИЯ: {trip.MainDestination}");
            sb.AppendLine($" ПЕРИОД: {trip.StartDate:dd/MM/yyyy} - {trip.EndDate:dd/MM/yyyy}");
            sb.AppendLine($" БРОЙ УЧАСТНИЦИ: {booking.PeopleCount}");

            if (trip.AdditionalStops != null && trip.AdditionalStops.Any())
            {
                sb.AppendLine($" МАРШРУТ: {string.Join(" -> ", trip.AdditionalStops)}");
            }

            sb.AppendLine("--------------------------------------------------");
            sb.AppendLine($" СТАТУС: {booking.Status}");
            sb.AppendLine($" ПЛАТЕНА СУМА: {paidAmount:F2} лв.");
            sb.AppendLine("--------------------------------------------------");
            sb.AppendLine("   Благодарим Ви, че избрахте нашата агенция!     ");
            sb.AppendLine("**************************************************");

            return sb.ToString();
        }
    }
}