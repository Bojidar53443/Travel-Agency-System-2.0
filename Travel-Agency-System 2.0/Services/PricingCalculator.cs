using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel_Agency_System_2._0.Data;

namespace Travel_Agency_System_2._0.Services
{
    internal class PricingCalculator
    {
        public decimal CalculateTotalPrice(int tripId, Season season, ServiceType serviceType, int peopleCount, bool hasInsurance)
        {
            var trip = DataContext.Trips.FirstOrDefault(t => t.Id == tripId);
            if (trip == null) return 0;

            decimal currentPrice = trip.BasePrice;


            currentPrice *= GetSeasonMultiplier(season);


            currentPrice += GetServiceExtraCost(serviceType);


            if (hasInsurance)
            {
                currentPrice += 50.00m;
            }


            return currentPrice * peopleCount;
        }

        private decimal GetSeasonMultiplier(Season season)
        {
            return season switch
            {
                Season.High => 1.50m,
                Season.Mid => 1.20m,
                Season.Low => 0.90m,
                _ => 1.0m
            };
        }

        private decimal GetServiceExtraCost(ServiceType type)
        {
            return type switch
            {
                ServiceType.AllInclusive => 200.00m,
                ServiceType.VIP => 500.00m,
                ServiceType.Standard => 50.00m,
                _ => 0m
            };
        }
    }
}
