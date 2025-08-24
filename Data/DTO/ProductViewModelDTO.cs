namespace backend_se.Data.DTO
{
    public class ProductViewModelDTO
    {
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public long UserId { get; set; }
        public required string Username { get; set; }
        public DateTime Created { get; set; }
        public decimal Price { get; set; }
        public int CurrencyId { get; set; }
        public required string CurrencyName { get; set; }
        public required string CityName { get; set; }
        public short Condition { get; set; }
        public required string ConditionName { get; set; }
        public bool Displayed { get; set; }
        public List<ProductSpecificationView> Specifications { get; set; } = new List<ProductSpecificationView>();
        public List<string> Images { get; set; } = new List<string>();
    }

    public class ProductSpecificationView
    {
        public required string Name { get; set; }
        public required string Value { get; set; }
    }
}
