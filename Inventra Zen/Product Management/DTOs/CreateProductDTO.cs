using System.ComponentModel.DataAnnotations;

namespace Product_Management.DTOs
{
    public class CreateProductDTO
    {
        [Required]
        [StringLength(100)]
        public string ProductName { get; set; } = default!;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = default!;
    }
}
