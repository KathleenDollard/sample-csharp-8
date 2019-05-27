namespace TollCollectorLib
{
    
    namespace ConsumerVehicleRegistration
    {
        public class Car
        {
            public int Passengers { get; set; }
        }
    }

    namespace CommercialRegistration
    {
        public class DeliveryTruck
        {
            public int GrossWeightClass { get; set; }
        }
    }

    namespace LiveryRegistration
    {
        public class Taxi
        {
            public int Fares { get; set; }
        }

        public class Bus
        {
            public int Capacity { get; set; }
            public int Riders { get; set; }

            public void Deconstruct(out int capacity, out int riders)
            => (capacity, riders) = (Capacity, Riders);
        }

        public readonly struct Bicycle
        {
            public  Bicycle(int riders, int wheels)
            {
                Riders = riders;
                Wheels = wheels;
            }

            public int Riders { get;   }
            public  int Wheels { get;  }
        }
    }

}