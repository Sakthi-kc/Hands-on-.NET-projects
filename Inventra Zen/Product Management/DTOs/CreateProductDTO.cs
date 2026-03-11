using Product_Management.Validator;
using System.ComponentModel.DataAnnotations;

namespace Product_Management.DTOs
{
    public class CreateProductDTO
    {
        [Required]
        [StringLength(100)]
        public string ProductName { get; set; } = default!;

        [Required]
        [DTOValidatorAttribute(ErrorMessage = "Price must be non-negative.")]
        public decimal Price { get; set; }

        [DTOValidatorAttribute(ErrorMessage = "Stock quantity must be non-negative.")]
        public int StockQuantity { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = default!;
    }
}
