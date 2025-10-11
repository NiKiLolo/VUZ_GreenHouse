using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseSystem.Services
{
    public class MonitoringSettings
    {
        public double criticalHumidityThreshold { get; set; }
        public int monitoringIntervalMs { get; set; }

    }
}
