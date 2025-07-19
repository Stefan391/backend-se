namespace backend_se.Data.DTO
{
    public class ChatDTO
    {
        public required string username { get; set; }
        public long userId { get; set; }
        public long senderId { get; set; }
        public bool isRead { get; set; }
        public required string message { get; set; }
        public required string sentTime { get; set; }
    }
}
