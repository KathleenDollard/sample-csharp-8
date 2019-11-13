namespace GreenRegistration
{
    public struct Cycle
    {
        public readonly int x;

        public Cycle(int riders, int wheels)
        {
            Riders = riders;
            Wheels = wheels;
            x = 42;
        }

        public void Deconstruct(out int riders, out int wheels) =>
                (riders, wheels) = (Riders, Wheels);

        public  int Riders { get; }
        public int Wheels { get; }
    }
}
