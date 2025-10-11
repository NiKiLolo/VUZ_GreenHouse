using GreenHouseSystem.DataInterfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GreenHouseSystem.Services
{
    public class ThresholdCheckStrategy : IDataProcessingStrategy
    {
        private readonly ILogger<ThresholdCheckStrategy> _logger;
        private readonly double _criticalThreshold;
        public ThresholdCheckStrategy(ILogger<ThresholdCheckStrategy> logger, IOptions<MonitoringSettings> settings)
        {
            _logger = logger;
            // Получаем пороговое значение из конфигурации
            _criticalThreshold = settings.Value.criticalHumidityThreshold;
        }
        public Task<bool> ProcessDataAsync(double data)
        {
            // Простая проверка: если данные выше порога -> аномалия
            bool isAnomaly = data > _criticalThreshold;
            if (isAnomaly)
                _logger.LogWarning("Сработала пороговая проверка! Значение {Data} превысило порог {Threshold}", data, _criticalThreshold);
            return Task.FromResult(isAnomaly);
        }
            
    }
}
