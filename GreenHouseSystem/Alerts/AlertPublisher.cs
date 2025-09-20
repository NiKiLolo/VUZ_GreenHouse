using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class AlertPublisher
{
    private static List<IAlertSubscribers> _subscribers = new List<IAlertSubscribers>();
    public static void Subscribe(IAlertSubscribers subscriber)
    {
        _subscribers.Add(subscriber);
    }
    public static void Notify(string message)
    {
        foreach (var sub in _subscribers)
        {
            sub.Alert(message); // Оповещаем всех подписчиков
        }
    }
}