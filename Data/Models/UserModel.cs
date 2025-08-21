namespace backend_se.Data.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public short Role { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int CityId { get; set; }
    }
}