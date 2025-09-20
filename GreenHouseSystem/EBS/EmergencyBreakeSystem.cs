using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EmergencyBreakeSystemNotify : IAlertSubscribers
{
    public void Alert(string message)
    {
        Console.WriteLine("Система Аварийной Остановки: Активирована аварийная остановка");
    }
}
public interface IEBSSubscribers // Отдельная подписка для Интерфейса, так как САО и Интерфейс связаны
{
    void EBSAlert(string message);
}