namespace backend_se.Data.ChatNotificationHub
{
    public class ReadMessageRequest
    {
        public long messageId { get; set; }
        public long readerId { get; set; }
    }
}
