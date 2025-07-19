namespace backend_se.Data.Models
{
    public class UserConnectionModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public required string ConnectionId { get; set; }
        public DateTime Connected { get; set; }
        public DateTime? Disconnected { get; set; }
    }
}
