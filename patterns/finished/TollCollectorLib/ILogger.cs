namespace TollCollectorLib
{
    public interface ILogger
    {
        void SendMessage(string message, LogLevel error = 0);
    }
}