using System.ComponentModel.DataAnnotations;

namespace Product_Management.DTOs
{
    public class CreateProductDTO
    {
        [Required]
        [StringLength(100)]
        public string ProductName { get; set; } = default!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        [Required]
        public string Category { get; set; } = default!;
    }
}
