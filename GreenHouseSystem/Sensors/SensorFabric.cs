using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SensorFabric<T>
{
    public static ISensor<T> CreateSensor(string sensorType)
    {
        if (typeof(T) == typeof(double) || typeof(T) == typeof(SoilQualitySensorValue)) // проверка на тип прежде чем создать датчик
        {
            switch (sensorType)
            {
                case "TemperatureSesnor":
                    return (ISensor<T>)new TemperatureSesnor();
                case "HumiditySesnor":
                    return (ISensor<T>)new HumiditySesnor();
                case "SoilQualitySensor":
                    return (ISensor<T>)new SoilQualitySensor();
                default:
                    throw new ArgumentException("Unknown sensor type");
            }
        }
        else
        {
            throw new ArgumentException("Generic type for sensor is cannot be use");
        }
        
    }
}

