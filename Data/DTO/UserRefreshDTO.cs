namespace backend_se.Data.DTO
{
    public class UserRefreshDTO
    {
        public long userId { get; set; }
        public required string username { get; set; }
        public required string role { get; set; }
    }
}
