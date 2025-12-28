namespace Core.Notifications
{
    public class DomainNotification
    {
        public string? Key { get; private set; }
        public string Message { get; private set; }

        public DomainNotification(string message, string? key = null)
        {            
            Message = message;
            Key = key;
        }
    }
}
