using GreenHouseSystem.Alerts;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
namespace GreenHouseSystem.Services
{
    public class LoggerAlertNotifier : IAlertNotifier
    {
        private readonly ILogger<LoggerAlertNotifier> _logger;
        public LoggerAlertNotifier(ILogger<LoggerAlertNotifier> logger)
        {
            _logger = logger;
        }
        public Task NotifyAsync(string message, double value)
        {
            // Записываем критическое сообщение в лог
           _logger.LogCritical("УВЕДОМЛЕНИЕ: {Message}. Значение: {Value}",
           message, value);
           return Task.CompletedTask;
        }
    }
}
