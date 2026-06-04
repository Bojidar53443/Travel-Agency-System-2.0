using System;
using System.Linq;
using Travel_Agency_System_2._0.Enums;
using Travel_Agency_System_2._0.Interfaces;
using Travel_Agency_System_2._0.Models;
using Travel_Agency_System_2._0.Repositories; 
using Travel_Agency_System_2._0.sql_connection;

namespace Travel_Agency_System_2._0.Services
{
    internal class PaymentService
    {
        private readonly TravelAgencyDbContext _context;
        private readonly IBookingRepository _bookingRepo;

        public PaymentService()
        {
            _context = new TravelAgencyDbContext();
            _bookingRepo = new EfBookingRepository(_context);
        }

        public string ProcessPayment(int bookingId, decimal amount, string method)
        {
            try
            {
                var booking = _bookingRepo.GetById(bookingId);
                if (booking == null) return "Грешка: Резервацията не е намерена.";

                var payment = new Payment
                {
                    BookingId = bookingId,
                    Amount = amount,
                    PaymentMethod = method
                };

                _context.Payments.Add(payment);
                _context.SaveChanges();

                booking.Status = BookingStatus.Active;
                _bookingRepo.Update(booking);

                return $"Плащането на стойност {amount} лв. бе успешно регистрирано за Резервация #{bookingId}.";
            }
            catch (Exception)
            {
                return "Грешка при обработка на плащането.";
            }
        }

        public decimal GetRemainingBalance(int bookingId)
        {
            try
            {
                var booking = _bookingRepo.GetById(bookingId);
                if (booking == null) return 0;

                decimal paidAmount = _context.Payments
                    .Where(p => p.BookingId == bookingId)
                    .Sum(p => p.Amount);

                return (booking.PeopleCount * 100) - paidAmount;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}