using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GreenHouseSystem.DataInterfaces
{
    public interface IDataProvider
    {
        Task<Double> ReadValueAsync();
    }
}
