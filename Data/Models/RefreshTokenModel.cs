namespace backend_se.Data.Models
{
    public class RefreshTokenModel
    {
        public Guid Id { get; set; }
        public required long UserId { get; set; }
        public DateTime Expires { get; set; }
        public bool Revoked { get; set; }
    }
}
