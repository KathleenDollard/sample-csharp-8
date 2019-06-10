using System;
using TollCollectorLib.CommercialRegistration;
using TollCollectorLib.ConsumerVehicleRegistration;
using TollCollectorLib.LiveryRegistration;

namespace TollCollectorLib

{
    public static class TollCalculator
    {
        public static decimal CalculateToll(object vehicle)
        {
            if (vehicle is null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }
            switch (vehicle)
            {
                case Car c when c.Passengers == 0:
                    return 2.00m + 0.5m;
                case Car c when c.Passengers == 1:
                    return 2.00m;
                case Car c when c.Passengers == 2:
                    return 2.00m - 0.50m;
                case Car c:
                    return 2.00m - 1.00m;

                case Taxi t when t.Fares == 0:
                    return 3.50m + 1.0m;
                case Taxi t when t.Fares == 1:
                    return 3.50m;
                case Taxi t when t.Fares == 2:
                    return 3.50m - 0.50m;
                case Taxi t:
                    return 3.50m - 1.00m;

                case Bus b when ((double)b.Riders / (double)b.Capacity) < 0.50:
                    return 5.00m + 2.00m;
                case Bus b when ((double)b.Riders / (double)b.Capacity) > 0.90:
                    return 5.00m - 1.00m;
                case Bus b:
                    return 5.00m;

                case DeliveryTruck t when t.GrossWeightClass > 5000:
                    return 10.00m + 5.00m;
                case DeliveryTruck t when t.GrossWeightClass < 3000:
                    return 10.00m - 2.00m;
                case DeliveryTruck t:
                    return 10.00m;

                default:
                    throw new ArgumentException(message: "Not a known vehicle type", paramName: nameof(vehicle));
            };
        }

        public static decimal PeakTimePremium(DateTime timeOfToll, bool inbound)
        {
            if (IsWeekDay(timeOfToll))
            {
                if (GetTimeBand(timeOfToll) == TimeBand.MorningRush)
                {
                    return inbound ? 2.00m : 1.00m;
                }
                if (GetTimeBand(timeOfToll) == TimeBand.Daytime)
                {
                    return 1.50m;
                }
                if (GetTimeBand(timeOfToll) == TimeBand.EveningRush)
                {
                    return !inbound ? 2.00m : 1.00m;
                }
                if (GetTimeBand(timeOfToll) == TimeBand.Overnight)
                {
                    return 0.75m;
                }
            }
            return 1.00m;

        }

        private static bool IsWeekDay(DateTime timeOfToll)
        {
            switch (timeOfToll.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return true;
                case DayOfWeek.Tuesday:
                    return true;
                case DayOfWeek.Wednesday:
                    return true;
                case DayOfWeek.Thursday:
                    return true;
                case DayOfWeek.Friday:
                    return true;
                case DayOfWeek.Saturday:
                    return false;
                case DayOfWeek.Sunday:
                    return false;
                default:
                    return true;
            };
        }

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
