namespace backend_se.Data.DTO
{
    public class ChatHistoryDTO
    {
        public long messageId { get; set; }
        public required string username { get; set; }
        public long userId { get; set; }
        public long senderId { get; set; }
        public long unreadCount { get; set; }
        public bool isRead { get; set; }
        public required string message { get; set; }
        public required string sentTime { get; set; }
        public bool isTyping { get; set; } = false;
        public bool isLastMessage { get; set; }
    }

    public class ChatHistoryRequest
    {
        public long? messageId { get; set; }
    }
    public class ChatHistoryResponse
    {
        public long messageId { get; set; }
        public required string username { get; set; }
        public long userId { get; set; }
        public long senderId { get; set; }
        public long unreadCount { get; set; }
        public bool isRead { get; set; }
        public required string message { get; set; }
        public required string sentTime { get; set; }
        public bool isTyping { get; set; } = false;
        public bool isLastMessage { get; set; }
    }

    public class MessageHistoryDTO
    {
        public long messageId { get; set; }
        public long userId { get; set; }
        public long senderId { get; set; }
        public required string message { get; set; }
        public required string sentTime { get; set; }
        public bool isRead { get; set; }
    }

    public class MessageHistoryRequest
    {
        public long userId { get; set; }
        public long? lastMessageId { get; set; } = null;
    }

    public class MessageHistoryResponse
    {
        public List<MessageHistoryDTO> list { get; set; } = new List<MessageHistoryDTO>();
        public bool isLastMessage { get; set; }
    }
}
