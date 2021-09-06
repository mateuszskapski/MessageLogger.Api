using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageLogger.Api.Extensions
{
    public static class LoggerExtensions
    {
        private static readonly EventId MessageReceived = new(1, "MessageReceived");

        private static readonly Action<ILogger, Guid, string, string, Exception> _logMessage =
            LoggerMessage.Define<Guid, string, string>(LogLevel.Information, MessageReceived, "{Id}, {Date}, {Message}");

        public static void LogReceivedMessage(this ILogger logger, Guid messageId, DateTime date, string message) =>
            _logMessage(logger, messageId, date.ToString("dd/MM/yyyy"), message, default);
    }
}
