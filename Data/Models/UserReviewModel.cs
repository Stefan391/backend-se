namespace backend_se.Data.Models
{
    public class UserReviewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ReviewerId { get; set; }
        public short ReviewStatus { get; set; }
        public bool GoodCommunication { get; set; }
        public bool GoodDescriptions { get; set; }
        public required string Description { get; set; }
    }
}