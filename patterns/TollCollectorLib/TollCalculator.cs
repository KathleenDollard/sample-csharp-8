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

                Car c => c.Passengers switch { 
                    0  => carBase + 0.5m,
                    1  => carBase,
                    2  => carBase - 0.50m,
                    _ => carBase - 1.00m,
                },

                Taxi { Fares: 0 } => taxiBase + 1.0m,
                Taxi { Fares: 1 } => taxiBase,
                Taxi { Fares: 2 } => taxiBase - 0.50m,
                Taxi t => taxiBase - 1.00m,

                Bus b when ((double)b.Riders / (double)b.Capacity) < 0.50 => busBase + 2.00m,
                Bus b when ((double)b.Riders / (double)b.Capacity) > 0.90 => busBase - 1.00m,
                Bus b => busBase,

                DeliveryTruck t when t.GrossWeightClass > 5000 => deliveryTruckBase + 5.00m,
                DeliveryTruck t when t.GrossWeightClass < 3000 => deliveryTruckBase - 2.00m,
                DeliveryTruck t => deliveryTruckBase,

                _ => throw new ArgumentException(message: "Not a known vehicle type", paramName: nameof(vehicle))
            };

        public static decimal PeakTimePremium(
            DateTime timeOfToll,
            bool inbound)
                => (IsWeekDay(timeOfToll), GetTimeBand(timeOfToll), inbound) switch
            {
                (true, TimeBand.MorningRush, true) => 2.00m,
                (true, TimeBand.EveningRush, false) => 2.00m,
                (true, TimeBand.Daytime, _) => 1.50m,
                (true, TimeBand.Overnight, _) => 0.75m,
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
            return hour < 6
                ? TimeBand.Overnight
                : hour < 10
                    ? TimeBand.MorningRush
                    : hour < 16
                        ? TimeBand.Daytime
                        : hour < 20
                            ? TimeBand.EveningRush
                            : TimeBand.Overnight;
        }

    }
}
