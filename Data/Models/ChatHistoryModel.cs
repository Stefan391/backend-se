namespace backend_se.Data.Models
{
    public class ChatHistoryModel
    {
        public long Id { get; set; }
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
        public required string Message { get; set; }
        public DateTime SentTime { get; set; }
        public DateTime? ReadTime { get; set; }
    }
}