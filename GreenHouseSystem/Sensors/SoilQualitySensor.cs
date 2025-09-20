using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Структура определяет тип данных для датчика качества почвы
public struct SoilQualitySensorValue { public double temp { get; } public string soilType { get; } public double fertilizersPercentage { get; } public SoilQualitySensorValue(double temp, string soilType, double fertilizersPercentage) { this.temp = temp; this.soilType = soilType; this.fertilizersPercentage = fertilizersPercentage; } };
public class SoilQualitySensor : ISensor<SoilQualitySensorValue>
{
    public string sensorType => "SoilQualitySensor";
    public SoilQualitySensorValue Read()
    {
        return new SoilQualitySensorValue(31.4 , "Torf", 88.8);
    }
}