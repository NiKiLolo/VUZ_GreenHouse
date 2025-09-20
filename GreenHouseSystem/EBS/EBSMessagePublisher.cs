using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public static class EBSMessagePublisher
{
    private static List<IEBSSubscribers> _subscribers = new
    List<IEBSSubscribers>();
    public static void Subscribe(IEBSSubscribers subscriber)
    {
        _subscribers.Add(subscriber);
    }
    public static void Notify(string message)
    {
        foreach (var sub in _subscribers)
        {
            sub.EBSAlert(message); // Оповещаем всех подписчиков
        }
    }
}