using Product_Management.Validator;
using System.ComponentModel.DataAnnotations;

namespace Product_Management.DTOs
{
    public class UpdateProductDTO
    {
        public required string ProductName { get; set; }

        [DTOValidatorAttribute(ErrorMessage = "Price must be non-negative.")]
        public decimal? Price { get; set; }

        [DTOValidatorAttribute(ErrorMessage = "Stock quantity must be non-negative.")]
        public int? StockQuantity { get; set; }

        public string? Category { get; set; }
    }
}
