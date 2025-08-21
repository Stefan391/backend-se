namespace backend_se.Data.Models
{
    public class ProductCategoryModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int? DisplayIndex { get; set; }
    }
}
