using System.Collections.Generic;
using Travel_Agency_System_2._0.Models;

namespace Travel_Agency_System_2._0.Interfaces
{
    public interface IBookingRepository
    {
        Booking GetById(int id);
        IReadOnlyList<Booking> GetAll();
        void Save(Booking booking);
        void Update(Booking booking);
        IReadOnlyList<Booking> GetByClient(int clientId);
    }
}