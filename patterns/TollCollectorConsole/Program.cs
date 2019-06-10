using Common;
using System;
using System.Threading.Tasks;
using TollCollectorLib;
using TollCollectorLib.CommercialRegistration;
using TollCollectorLib.ConsumerVehicleRegistration;
using TollCollectorLib.LiveryRegistration;

namespace TollCollectorConsole
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            TollSystem.Initialize(new Logger());

            await TollSystem.ChargeTollAsync(
                new Car { Passengers = 2 },
                time: DateTime.Now,
                inbound: true,
                license: "BSF-846-WA");

            //await ChargeTollsFromStreamAsync();

        }

        private class Logger : ILogger
        {
            public void SendMessage(string message, LogLevel logLevel)
                => Console.WriteLine(message);
        }

        //private static async Task ChargeTollsFromStreamAsync()
        //{
        //    foreach (var t in TollSystem.GetTollEventsAsync())
        //    {
        //        await TollSystem.ChargeTollAsync(t.vehicle, t.time, t.inBound, t.license);
        //    }
        //}   
    }
}