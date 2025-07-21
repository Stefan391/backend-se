namespace backend_se.Data.ChatNotificationHub
{
    public class StartTypingRequest
    {
        public long senderId { get; set; }
        public long receiverId { get; set; }
    }
}
