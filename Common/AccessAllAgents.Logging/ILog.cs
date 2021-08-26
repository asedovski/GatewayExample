using System;

namespace AccessAllAgents.Logging
{
    public interface ILog
    {
        void Debug(string message);
        void Error(string message);
        void Error(string message, Exception exception);
        void Fatal(string message);
        void Fatal(string message, Exception exception);
        void Info(string message, Exception exception);
        void Info(string message);
        void Warn(string message);
        void Warn(string message, Exception exception);
    }
}