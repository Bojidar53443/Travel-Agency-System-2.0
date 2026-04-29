using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Travel_Agency_System_2._0.JsonAddings
{
    public class TravelRepository
    {
        private string filePath = "travels.json";

        public List<Travel> GetAll()
        {
            if (!File.Exists(filePath))
                return new List<Travel>();

            string json = File.ReadAllText(filePath);

            List<Travel> travels = JsonSerializer.Deserialize<List<Travel>>(json);

            if (travels == null)
                return new List<Travel>();

            return travels;
        }

        public void Add(Travel travel)
        {
            List<Travel> travels = GetAll();

            if (travels.Count == 0)
                travel.Id = 1;
            else
                travel.Id = travels.Max(t => t.Id) + 1;

            travels.Add(travel);

            Save(travels);
        }

        private void Save(List<Travel> travels)
        {
            string json = JsonSerializer.Serialize(travels, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(filePath, json);
        }
    }

}
