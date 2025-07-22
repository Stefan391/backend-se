namespace backend_se.Data.ChatNotificationHub
{
    public class SendMessageResponse
    {
        public long messageId { get; set; }
        public required string username { get; set; }
        public long userId { get; set; }
        public long senderId { get; set; }
        public bool isRead { get; set; }
        public required string message { get; set; }
        public required string sentTime { get; set; }
        public long unreadCount { get; set; }
    }
}
