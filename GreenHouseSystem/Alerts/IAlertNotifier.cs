namespace GreenHouseSystem.Alerts
{
    public interface IAlertNotifier
    {
        Task NotifyAsync(string message, double value);
    }
}
