using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using TollCollectorLib.BillingSystem;
using TollCollectorLib.ConsumerVehicleRegistration;
using Common;

namespace TollCollectorLib
{
    public static class TollSystem
    {
        private static readonly ConcurrentQueue<(object, DateTime, bool, string)> s_queue
            = new ConcurrentQueue<(object, DateTime, bool, string)>();
        private static ILogger s_logger;

        public static void Initialize(ILogger logger, bool includeTestData = false) 
        {
            s_logger = logger;
           if (includeTestData)
            { 
                TollSystem.AddEntry(new Car(), DateTime.Now, true, "AAA-BBB-CO");
                TollSystem.AddEntry(new Car(), DateTime.Now, true, "BBB-CCC-AK");
                TollSystem.AddEntry(new Car(), DateTime.Now, true, "CCC-DDD-WI");
                TollSystem.AddEntry(new Car(), DateTime.Now, true, "DDD-EEE-WA");
            }
        }

        public static void AddEntry(object vehicle, DateTime time, bool inbound, string license)
        {
            s_queue.Enqueue((vehicle, time, inbound, license));
        }

        //public static async IAsyncEnumerable
        //        <(object vehicle, DateTime time, bool inBound, string license)>
        //        GetTollEventsAsync()
        //{
        //    while (true)
        //    {
        //        if (s_queue.TryDequeue(out var entry))
        //        {
        //            yield return entry;
        //        }

        //        await Task.Delay(500);
        //    }
        //}

        public static async Task ChargeTollAsync(
            object vehicle,
            DateTime time,
            bool inbound,
            string license)
        {
            try
            {
                var baseToll = TollCalculator.CalculateToll(vehicle);
                var peakPremium = TollCalculator.PeakTimePremium(time, inbound);
                var toll = baseToll * peakPremium;
                Account account = await Account.LookupAccountAsync(license);
                account.Charge(toll);
                s_logger.SendMessage($"Charged: {license} {toll:C}");
            }
            catch (Exception ex)
            {
                s_logger.SendMessage(ex.Message, LogLevel.Error);
            }
        }


        //public static async Task ChargeTollsFromStreamAsync()
        //{
        //    foreach (var t in TollSystem.GetTollEventsAsync())
        //    {
        //        await TollSystem.ChargeTollAsync(t.vehicle, t.time, t.inBound, t.license);
        //    }
        //}   

    }



}
