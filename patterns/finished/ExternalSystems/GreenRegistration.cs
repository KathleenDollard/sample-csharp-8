using System;
using System.Collections.Generic;
using System.Text;

namespace GreenRegistration
{
    public readonly struct Bicycle
    {
        public Bicycle(int riders, int wheels)
        {
            Riders = riders;
            Wheels = wheels;
        }

        public int Riders { get; }
        public int Wheels { get; }
    }
}
