namespace backend_se.Data.Search
{
    public class ProductSearch
    {
        public long? UserId { get; set; }
        public string? Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? CurrencyId { get; set; }
        public short? Condition { get; set; }
        public int? CategoryId { get; set; }
        public bool? Displayed { get; set; }
        public List<SpecificationsSearch> Specifications { get; set; } = new List<SpecificationsSearch>();
    }
    public class SpecificationsSearch
    {
        public int SpecificationId { get; set; }
        public List<string> SpecificationValue { get; set; } = new List<string>();
        public bool BoolValue { get; set; } = false;
        public bool IsBool { get; set; } = false;
    }
}