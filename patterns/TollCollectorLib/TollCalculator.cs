using System;
using CommercialRegistration;
using ConsumerVehicleRegistration;
using LiveryRegistration;

namespace TollCollectorLib

{
    public static class TollCalculator
    {
        private const decimal carBase = 2.00m;
        private const decimal taxiBase = 3.50m;
        private const decimal busBase = 5.00m;
        private const decimal deliveryTruckBase = 10.00m;

        public static decimal CalculateToll(object vehicle)
         => vehicle switch
         {
             null => throw new ArgumentNullException(nameof(vehicle)),

             Car c => (object)c.Passengers switch
             {
                 0 => 2.00m + 0.5m,
                 1 => 2.0m,
                 2 => 2.0m - 0.5m,
                 _ => 2.00m - 1.0m
             },

             Taxi { Fares: 0 } => taxiBase + 1.0m,
             Taxi { Fares: 1 } => taxiBase,
             Taxi { Fares: 2 } => taxiBase - 0.50m,
             Taxi _ => taxiBase - 1.00m,

             Bus b when ((double)b.Riders / (double)b.Capacity) < 0.50 => busBase + 2.00m,
             Bus b when ((double)b.Riders / (double)b.Capacity) > 0.90 => busBase - 1.00m,
             Bus _ => busBase,

             DeliveryTruck t when t.GrossWeightClass > 5000 => deliveryTruckBase + 5.00m,
             DeliveryTruck t when t.GrossWeightClass < 3000 => deliveryTruckBase - 2.00m,
             DeliveryTruck _ => deliveryTruckBase,

             _ => throw new ArgumentException(message: "Not a known vehicle type", paramName: nameof(vehicle))
         };


        public static decimal PeakTimePremium(
                DateTime timeOfToll,
                bool inbound)
            => (IsWeekDay(timeOfToll), GetTimeBand(timeOfToll), inbound) switch
            {
                (true, TimeBand.Daytime, _) => 1.50m,
                (true, TimeBand.Overnight, _) => 0.75m,
                (true, TimeBand.EveningRush, false) => 2.00m,
                (true, TimeBand.MorningRush, true) => 2.00m,
                (_, _, _) => 1.00m,
            };


        private static bool IsWeekDay(DateTime timeOfToll)
            => timeOfToll.DayOfWeek switch
            {
                DayOfWeek.Saturday => false,
                DayOfWeek.Sunday => false,
                _ => true,
            };

        private enum TimeBand
        {
            MorningRush,
            Daytime,
            EveningRush,
            Overnight
        }

        private static TimeBand GetTimeBand(DateTime timeOfToll)
        {
            var hour = timeOfToll.Hour;
            if (hour < 6)
            {
                return TimeBand.Overnight;
            }
            else if (hour < 10)
            {
                return TimeBand.MorningRush;
            }
            else if (hour < 16)
            {
                return TimeBand.Daytime;
            }
            else if (hour < 20)
            {
                return TimeBand.EveningRush;
            }
            else
            {
                return TimeBand.Overnight;
            }
        }

    }
}
