namespace PropertyManagementApp.Client.Services
{
    public class NotificationService
    {
        public event Action<string, string>? OnNotify;

        public void Success(string message)
        {
            OnNotify?.Invoke(message, "success");
        }

        public void Error(string message)
        {
            OnNotify?.Invoke(message, "danger");
        }

        public void Info(string message)
        {
            OnNotify?.Invoke(message, "info");
        }
    }
}