using System.Text.Json.Serialization;

namespace Core.Notifications
{
    public class DomainNotification
    {
        [JsonPropertyName("key")]
        public string? Key { get; private set; }

        [JsonPropertyName("message")]
        public string Message { get; private set; }

        public DomainNotification(string message, string? key = null)
        {            
            Message = message;
            Key = key;
        }
    }
}
