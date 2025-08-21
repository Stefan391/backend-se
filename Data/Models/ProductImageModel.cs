namespace backend_se.Data.Models
{
    public class ProductImageModel
    {
        public int Id { get; set; }
        public required string ImageUrl { get; set; }
        public int? DisplayIndex { get; set; }
    }
}
