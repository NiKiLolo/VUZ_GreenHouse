using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workstation.ServiceModel.Ua;

namespace GreenHouseSystem.Services
{
    public class OpcUaSettings
    {
        public string serverUrl { get; set; } = "";
        public string nodeId { get; set; } = "";
    }
}
