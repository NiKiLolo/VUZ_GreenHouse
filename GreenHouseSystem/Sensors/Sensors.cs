using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseSystem.Sensors
{
    public interface ISensor<T> // Используешься шаблон так как некоторые датчики возвращаются разные типы данных
    {
        string sensorType { get; }
        T Read();
    }
}