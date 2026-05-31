using System.Collections.Generic;
using Travel_Agency_System_2._0.Models;

namespace Travel_Agency_System_2._0.Interfaces
{
    public interface IClientRepository
    {
        Client GetById(int id);
        IReadOnlyList<Client> GetAll();
        void Save(Client client);
        void Update(Client client);
    }
}