using Microsoft.Extensions.Logging;
using System;

namespace Store.BusinessLogicLayer.Common.Interfaces
{
    public interface ILogger
    {
        IDisposable BeginScope<TState>(TState state);

        bool IsEnabled(LogLevel logLevel);

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter);
    }
}
