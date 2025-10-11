using GreenHouseSystem.Alerts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenHouseSystem.Services
{
    public class AlertManager
    {
        private readonly List<IAlertNotifier> _notifiers = new List<IAlertNotifier>();
        // Метод для подписки на уведомления
        public void Subscribe(IAlertNotifier notifier)
        {
            if (!_notifiers.Contains(notifier))
            {
                _notifiers.Add(notifier);
            }
        }
        // Метод для отписки от уведомлений
        public void Unsubscribe(IAlertNotifier notifier)
        {
            _notifiers.Remove(notifier);
        }
        // Главный метод для оповещения всех подписчиков
        public async Task NotifyAllAsync(string message, double value)
        {
            // Создаём задачи для всех уведомлений
            var notificationTasks = _notifiers.Select(notifier => notifier.NotifyAsync(message, value));
            // Запускаем все уведомления параллельно и ждём их завершения
            await Task.WhenAll(notificationTasks);
        }
    }
}