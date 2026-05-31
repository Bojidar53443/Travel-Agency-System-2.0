using System.Collections.Generic;
using Travel_Agency_System_2._0.Models;

namespace Travel_Agency_System_2._0.Interfaces
{
    public interface ITripRepository
    {
        Trip GetById(int id);
        IReadOnlyList<Trip> GetAll();
        void Save(Trip trip);
        void Update(Trip trip);
    }
}