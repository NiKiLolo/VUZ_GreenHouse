using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class HumiditySesnor : ISensor<double>
{
    public string sensorType => "HumiditySesnor";
    public double Read()
    {
        return 25.4;
    }
}
