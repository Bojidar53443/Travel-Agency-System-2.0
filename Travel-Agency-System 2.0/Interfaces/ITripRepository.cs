using System.Collections.Generic;
using Travel_Agency_System_2._0.Models;

namespace Travel_Agency_System_2._0.Interfaces
{
    public interface ITripRepository
    {
        IReadOnlyList<Trip> GetAll();
        Trip GetById(int id);
        void Save(Trip trip);
        void Update(Trip trip);
        void Delete(Trip trip);
    }
}