using System;
using System.Threading.Tasks;
using TollCollectorLib.CommercialRegistration;
using TollCollectorLib.ConsumerVehicleRegistration;
using TollCollectorLib.LiveryRegistration;

namespace TollCollectorLib

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
                license: "BSF-846X-WA");

            //await MoreStuffAsync();
       
        }

        private class Logger : ILogger
        {
            public void SendMessage(string message, LogLevel logLevel)
            {
                Console.WriteLine(message);
            }
        }

        private static async Task MoreStuffAsync()
        {
            await AddEntriesAsync();
            await foreach (var t in TollSystem.GetVehiclesAsync())
            {
                await TollSystem.ChargeTollAsync(t.vehicle, t.time, t.inBound, t.license);
            }
        }

        private static async Task AddEntriesAsync()
        {
            TollSystem.AddEntry(new Car(), DateTime.Now, true, "AAA-BBB-CO");
            TollSystem.AddEntry(new Car(), DateTime.Now, true, "BBB-CCC-AK");
            TollSystem.AddEntry(new Car(), DateTime.Now, true, "CCC-DDD-WI");
            TollSystem.AddEntry(new Car(), DateTime.Now, true, "DDD-EEE-WA");
        }
    }
}
