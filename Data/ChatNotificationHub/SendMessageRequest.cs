namespace backend_se.Data.ChatNotificationHub
{
    public class SendMessageRequest
    {
        public long userId { get; set; }
        public required string message { get; set; }
    }
}
