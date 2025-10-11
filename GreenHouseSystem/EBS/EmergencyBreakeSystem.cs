using GreenHouseSystem.Alerts;
using GreenHouseSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GreenHouseSystem.EBS
{
    public class EmergencyBreakeSystemNotify : IAlertNotifier
    {
        private readonly ILogger<EmergencyBreakeSystemNotify> _logger;
        public EmergencyBreakeSystemNotify(ILogger<EmergencyBreakeSystemNotify> logger)
        {
            _logger = logger;
        }
        public async Task NotifyAsync(string message, double value)
        {
            _logger.LogWarning("Система Аварийной Останвоки: {Message}. Значение:{ Value}", message, value);

        }
    }
}