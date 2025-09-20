AlertPublisher.Subscribe(new EmergencyBreakeSystemNotify());
AlertPublisher.Subscribe(new OperatorInterfaceNotify());
EBSMessagePublisher.Subscribe(new OperatorInterfaceNotify());


ISensor<double> temperatureSensor = SensorFabric<double>.CreateSensor("TemperatureSesnor");
ISensor<double> humiditySesnor = SensorFabric<double>.CreateSensor("HumiditySesnor");
ISensor<SoilQualitySensorValue> soilQualitySensor = SensorFabric<SoilQualitySensorValue>.CreateSensor("SoilQualitySensor");



SoilQualitySensorValue value = new SoilQualitySensorValue();
value = soilQualitySensor.Read();

if(value.temp > 30)
{
    AlertPublisher.Notify($"Внимание. Критическое значение температуры почвы: Температура превышена на {value.temp-30} от критической");
    EBSMessagePublisher.Notify($"Температура сейчас {value.temp} | Критическое значение {30}");
}