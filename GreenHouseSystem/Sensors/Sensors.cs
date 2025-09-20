using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface ISensor<T>
{
    string sensorType { get; }
    T Read();
}