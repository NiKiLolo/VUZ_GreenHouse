using GreenHouseSystem.Alerts;
using GreenHouseSystem.DataInterfaces;
using GreenHouseSystem.EBS;
using GreenHouseSystem.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
namespace GreenHouseSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Starting..");


            //Создем веб приложением для запуска
           // var builder = WebApplication.CreateBuilder(args);
            //
            var builder = Host.CreateApplicationBuilder(args);

            // 2. Говорим, где брать настройки (appsettings.json)
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            // 3. Настраиваем сервисы (наших "игроков")
            builder.Services.Configure<OpcUaSettings>(builder.Configuration.GetSection("OpcUaSettings"));
            builder.Services.Configure<MonitoringSettings>(builder.Configuration);
            // 4. РЕГИСТРИРУЕМ НАШИ ИСТОЧНИКИ ДАННЫХ
            // Если нужно использовать OPC UA - раскомментируйте следующуюстроку:
            builder.Services.AddTransient<IDataProvider, OpcUaDataProvider>();
            // Если нужно использовать симулятор OPC DA - раскомментируйте следующую строку:
            //builder.Services.AddTransient<IDataProvider, LegacyOpcDaSimulatorService>();
            builder.Services.AddTransient<ThresholdCheckStrategy>();
            builder.Services.AddTransient<SuddenJumpDetectionStrategy>();

            // Система оповещения
            builder.Services.AddTransient<IAlertNotifier, LoggerAlertNotifier>();
            builder.Services.AddTransient<IAlertNotifier, InterfaceAlertNotifier>();
            builder.Services.AddTransient<IAlertNotifier, EmergencyBreakeSystemNotify>();
            builder.Services.AddSingleton<AlertManager>();
            builder.Services.AddSingleton(provider =>
            {
                var alertManager = new AlertManager();
                // Получаем все зарегистрированные реализации IAlertNotifier
                var notifiers = provider.GetServices<IAlertNotifier>();
                foreach (var notifier in notifiers)
                {
                    alertManager.Subscribe(notifier);
                }
                return alertManager;
            });
            //
             // DataProcessor должен быть один
            // Устанавливаем стратегию по умолчанию для DataProcessor
            // Для этого используем фабрику для DataProcessor
         /*   builder.Services.AddSingleton(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<DataProcessor>>();
                var processor = new DataProcessor(logger);
                // По умолчанию используем пороговую проверку
                var defaultStrategy = provider.GetRequiredService<ThresholdCheckStrategy>();
                processor.SetStrategy(defaultStrategy);
                return processor;
            });*/

            builder.Services.AddSingleton<MainService>();
            builder.Services.AddSingleton<MonitoringService>();

            // 11. Обновляем регистрацию DataProcessor
            builder.Services.AddSingleton<DataProcessor>();
            builder.Services.AddSingleton(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<DataProcessor>>();
                return new DataProcessor(logger);
            });

            //Регистррируем демо сценарий в билдере веб приложения
            //
            //Регистррируем сервис в билдере веб приложения
            //builder.Services.AddHostedService<MonitoringService>();
            //Регистррируем консоль интерфейс в билдере веб приложения
            //  
            // 5. Собираем и запускаем хост
            builder.Services.AddHostedService<DemoScenarioService>();
            builder.Services.AddHostedService<ConsoleInterfaceService>();
            var host = builder.Build();
            
            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
                host.StopAsync().GetAwaiter().GetResult();
            };
            try
            {
                var mainService = host.Services.GetRequiredService<MainService>();
                await mainService.StartAsync(CancellationToken.None);
                //Тест в веб режиме вывод данных, позже на основе можно релизнуть интерфейс?(использовать html/css как доп задача неплохая идея)
                //host.UseMiddleware<LegacyOpcDaSimulatorServiceStarter>();
                await host.RunAsync();
            }
            catch (OperationCanceledException)
            {
                // Корректное завершение
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
            }
            finally
            {
                host.Dispose();
            }

        }
    }
}


//Старая наработка мэйна
/*

IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
IConfigurationSection section = config.GetSection("OpcUaSettings");



// Get a configuration section

//писюн писюн писечка писька писька единная россия вперед
// Read simple values
Console.WriteLine($"ServerUrl: {section["ServerUrl"]}");
Console.WriteLine($"NodeId: {section["NodeId"]}");

// Read a collection
//Console.WriteLine("Ports: ");
//IConfigurationSection ports = section.GetSection("Ports");


AlertPublisher.Subscribe(new EmergencyBreakeSystemNotify());
AlertPublisher.Subscribe(new OperatorInterfaceNotify());
EBSMessagePublisher.Subscribe(new OperatorInterfaceNotify());


ISensor<double> temperatureSensor = SensorFabric<double>.CreateSensor("TemperatureSesnor");
ISensor<double> humiditySesnor = SensorFabric<double>.CreateSensor("HumiditySesnor");
ISensor<SoilQualitySensorValue> soilQualitySensor = SensorFabric<SoilQualitySensorValue>.CreateSensor("SoilQualitySensor");



SoilQualitySensorValue value = new SoilQualitySensorValue();
value = soilQualitySensor.Read();

if(value.temp > 30) // Базовый тест для проверки работоспособности  подписок 
{
    AlertPublisher.Notify($"Внимание. Критическое значение температуры почвы: Температура превышена на {value.temp-30} от критической");
    EBSMessagePublisher.Notify($"Температура сейчас {value.temp} | Критическое значение {30}"); // Оповещения в случае срабатывания САО
}*/

// 1. Создаём "построителя" хоста

