﻿using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using TollCollectorLib.BillingSystem;
using ConsumerVehicleRegistration;
using Common;
using System.Collections.Generic;

namespace TollCollectorLib
{
    public static class TollSystem
    {
        private static readonly ConcurrentQueue<(object, DateTime, bool, string)> s_queue
            = new ConcurrentQueue<(object, DateTime, bool, string)>();
        private static ILogger? s_logger;

        public static void Initialize(ILogger logger) 
            => s_logger = logger;

        public static void AddEntry(object vehicle, DateTime time, bool inbound, string license)
        {
            s_logger?.SendMessage($"{time}: {(inbound ? "Inbound" : "Outbound")} {license} - {vehicle}");
            s_queue.Enqueue((vehicle, time, inbound, license));
        }

        public static async IAsyncEnumerable
                <(object vehicle, DateTime time, bool inbound, string license)>
                GetTollEventsAsync()
        {
            while (true)
            {
                if (s_queue.TryDequeue(out var entry))
                {
                    yield return entry;
                }

                await Task.Delay(500);
            }
        }

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
                Account? account = await Account.LookupAccountAsync(license);
                if (account != null)
                {
                    account.Charge(toll);
                    s_logger?.SendMessage($"Charged: {license} {toll:C}");
                }
                else
                {
                    var state = license[^2..];
                    var plate = license[..^3];
                    var owner = Owner.LookupOwnerAsync(state, plate);
                    var finalToll = toll + 2.00m;
                    s_logger?.SendMessage($"Send bill: {license} {finalToll:C}");
                }

            }
            catch (Exception ex)
            {
                s_logger?.SendMessage(ex.Message, LogLevel.Error);
            }
        }
    }
}
