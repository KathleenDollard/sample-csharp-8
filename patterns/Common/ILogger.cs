using System;

namespace Common

{
    public interface ILogger
    {
        void SendMessage(string message, LogLevel error = 0);
        void SendError(Exception ex) 
            => SendMessage(ex.ToString(), LogLevel.Error);
    }
}