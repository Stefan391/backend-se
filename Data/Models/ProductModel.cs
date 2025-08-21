namespace backend_se.Data.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime Created { get; set; }
        public decimal Price { get; set; }
        public int CurrencyId { get; set; }
        public short Condition { get; set; }
        public bool Displayed { get; set; }
        public int? BoughtBy { get; set; }
    }
}
