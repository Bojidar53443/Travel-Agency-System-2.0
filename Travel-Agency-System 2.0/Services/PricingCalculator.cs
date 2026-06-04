using System;
using System.Linq;
using Travel_Agency_System_2._0.Enums;
using Travel_Agency_System_2._0.Interfaces;
using Travel_Agency_System_2._0.Models;

namespace Travel_Agency_System_2._0.Services
{
    internal class PricingCalculator
    {
        private readonly ITripRepository _tripRepo;

        public PricingCalculator(ITripRepository tripRepo)
        {
            _tripRepo = tripRepo;
        }

        public decimal CalculateTotalPrice(int tripId, Season season, ServiceType serviceType, int peopleCount, bool hasInsurance)
        {
            var trip = _tripRepo.GetById(tripId);
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