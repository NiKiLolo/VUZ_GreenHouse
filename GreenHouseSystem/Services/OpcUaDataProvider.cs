//GHS
using GreenHouseSystem.DataInterfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workstation.ServiceModel.Ua;
using Workstation.ServiceModel.Ua.Channels;


namespace GreenHouseSystem.Services
{
    public class OpcUaDataProvider : IDataProvider
    {
        private readonly ILogger<OpcUaDataProvider> _logger;
        private readonly OpcUaSettings _settings;
            // Конструктор. Сюда система "положит" нужные настройки и логгер
        public OpcUaDataProvider(IOptions<OpcUaSettings> settings, ILogger<OpcUaDataProvider> logger)
        {
            _logger = logger;
            _settings = settings.Value; // Достаём сами настройки из контейнера
            _logger.LogInformation($"OPC UA провайдер создан для сервера:{_settings.serverUrl}");
        }
        public async Task<double> ReadValueAsync()
        {
            try
            {
                // 1. Создаём настройки для подключения
                var applicationDescription = new ApplicationDescription
                {
                    ApplicationName = "AdaptiveControlSystem Client",
                    ApplicationUri = "urn:localhost:AdaptiveControlSystem",
                    ApplicationType = ApplicationType.Client
                };
                // 2. Создаём и запускаем подключение к серверу

                var channel = new ClientSessionChannel( // в файле Павловича -  UaTcpSessionChannel, по результатом поиска сейчас используется ClientSessionChannel
                applicationDescription,
                null, // Без сертификата
                new AnonymousIdentity(), // Анонимный вход
                _settings.serverUrl);
                try
                {
                    await channel.OpenAsync();
                    // 3. Читаем значение с конкретного "датчика" (NodeId)
                    var readRequest = new ReadRequest
                    {
                        NodesToRead = new[] { new ReadValueId { NodeId = _settings.nodeId, AttributeId = AttributeIds.Value } }
                    };
                    var readResponse = await channel.ReadAsync(readRequest);
                    // 4. Если ответ успешный, возвращаем значение
                    if (readResponse.Results[0].StatusCode == Workstation.ServiceModel.Ua.StatusCodes.Good)
                    {
                        double value = (double)readResponse.Results[0].Value;
                        _logger.LogDebug("Прочитано значение из OPC UA: {Value}",value);
                        return value;
                    }
                    else
                    {
                        _logger.LogError("Ошибка чтения из OPC UA. Статус: { StatusCode}", readResponse.Results[0].StatusCode);
                         return double.NaN; // Возвращаем специальное значение "Нечисло" (Not a Number) для обозначения ошибки
                    }
                }
                catch (Exception ex)
                {
                    await channel.AbortAsync();
                    _logger.LogError(ex, "Ошибка подключения или чтения из OPC UA  сервера { ServerUrl}", _settings.serverUrl);
                    return double.NaN;
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Ошибка подключения или чтения из OPC UA  сервера {ServerUrl}", _settings.serverUrl);
                return double.NaN;
            }
        }
    }

}