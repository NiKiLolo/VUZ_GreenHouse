using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class OperatorInterfaceNotify : IAlertSubscribers, IEBSSubscribers 
{
    public void Alert(string message)
    {
        Console.WriteLine($"Интерфейс: Зафиксирована проблема: {message}");
    }
    public void EBSAlert (string message) 
    { 
        Console.WriteLine($"Интерфейс: Зафиксировано срабатывание САО. причина {message}"); 
    }
}
