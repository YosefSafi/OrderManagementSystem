namespace NotificationService.Models
{
    public class Notification
    {
        public Guid OrderId { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Recipient { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
