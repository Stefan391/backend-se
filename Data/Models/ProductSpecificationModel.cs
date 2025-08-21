namespace backend_se.Data.Models
{
    public class ProductSpecificationModel
    {
        public int Id { get; set; }
        public required string Value { get; set; }
        public bool BoolValue { get; set; }
        public int ProductId { get; set; }
        public int SpecificationId { get; set; }
    }
}
