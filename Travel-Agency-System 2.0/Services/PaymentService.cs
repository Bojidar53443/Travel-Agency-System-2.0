using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel_Agency_System_2._0.Data;
using Travel_Agency_System_2._0.Models;
using Travel_Agency_System_2._0.Enums;
namespace Travel_Agency_System_2._0.Services
{
    internal class PaymentService
    {
        public string ProcessPayment(int bookingId, decimal amount, string method)
        {

            var booking = DataContext.Bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking == null) return "Грешка: Резервацията не е  намерена.";


            if (amount < booking.FinalPrice)
            {
                return $"Грешка: Сумата е недостатъчна. Дължима сума: {booking.FinalPrice} лв.";
            }


            var payment = new Payment
            {
                Id = DataContext.Payments.Count + 1,
                BookingId = bookingId,
                Amount = amount,
                Date = DateTime.Now,
                PaymentMethod = method
            };

            DataContext.Payments.Add(payment);


            booking.Status = BookingStatus.Active;

            return $"Плащането на стойност {amount} лв. бе успешно регистрирано за Резервация #{bookingId}.";
        }


        public decimal GetRemainingBalance(int bookingId)
        {
            var booking = DataContext.Bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking == null) return 0;

            decimal paidAmount = DataContext.Payments
                .Where(p => p.BookingId == bookingId)
                .Sum(p => p.Amount);

            return booking.FinalPrice - paidAmount;
        }
    }
}
