using System.Collections.Generic;
using Travel_Agency_System_2._0.Models;

namespace Travel_Agency_System_2._0.Interfaces
{
    public interface IBookingRepository
    {
        IReadOnlyList<Booking> GetAll();
        Booking GetById(int id);
        void Save(Booking booking);
        void Update(Booking booking);
        void Delete(int id);
    }
}