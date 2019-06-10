using Common;
using GreenRegistration;
using System;
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
            var logger = new Logger();
            TollSystem.Initialize(logger, true);

            await TollSystem.ChargeTollAsync(
                new Car { Passengers = 2 },
                time: DateTime.Now,
                inbound: true,
                license: "BSF-846-WA");

            //DoTheGreenDemo();

            //await ChargeTollsFromStreamAsync();

            //void DoTheGreenDemo()
            //{
            //    var cycle = new Cycle(riders: 1, 1);
            //    var points = GreenPointSystem.GetPoints(cycle);
            //    logger.SendMessage($"Green! {cycle.Riders}/{cycle.Wheels} Points: {points}", LogLevel.Info);
            //}
        }

        private class Logger : ILogger
        {
            public void SendMessage(string message, LogLevel logLevel)
                => Console.WriteLine(message);
        }
    }
}