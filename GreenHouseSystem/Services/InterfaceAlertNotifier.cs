using GreenHouseSystem.Alerts;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GreenHouseSystem.Services
{
    public class InterfaceAlertNotifier : IAlertNotifier
    {
        private readonly ILogger<InterfaceAlertNotifier> _logger;
        public InterfaceAlertNotifier(ILogger<InterfaceAlertNotifier> logger)
        {
            _logger = logger;
        }
        public async Task NotifyAsync(string message, double value)
        {
            // Имитация отправки интерфейсу (задержка 0.2-0.5 секунда)
            int delayMs = new Random().Next(200, 500);
            await Task.Delay(delayMs);
            _logger.LogWarning("TO INTERFACE ОТПРАВЛЕН: {Message}. Значение:{ Value}. (Задержка: {Delay} мс)", message, value, delayMs);

        }
    }
}
