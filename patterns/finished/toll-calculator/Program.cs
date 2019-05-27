using System;
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
                license: "BSF-846x");
        }

        private class Logger : ILogger
        {
            public void SendMessage(string message, LogLevel logLevel)
            {
                Console.WriteLine(message);
            }
        }
    }
}
