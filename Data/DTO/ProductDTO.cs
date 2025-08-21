using backend_se.Common.Consts;
using System.ComponentModel.DataAnnotations;

namespace backend_se.Data.DTO
{
    public class ProductDTO
    {
        [Required(ErrorMessage = "Product name is required.")]
        [MaxLength(20, ErrorMessage = "Product name cannot be longer than 20 characters.")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "Product description is required.")]
        [MaxLength(100, ErrorMessage = "Product description cannot be longer than 100 characters.")]
        public required string Description { get; set; }
        [Range(0.00, double.MaxValue, ErrorMessage = "Product price has to be between 0.00 and 10000.00")]
        public decimal Price { get; set; }
        public int Currency { get; set; }
        [EnumDataType(typeof(eProductCondition), ErrorMessage = "Invalid product condition.")]
        public eProductCondition Condition { get; set; }
        public bool Displayed { get; set; }
    }
}
