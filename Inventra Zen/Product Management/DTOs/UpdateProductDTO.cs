using System.ComponentModel.DataAnnotations;

namespace Product_Management.DTOs
{
    public class UpdateProductDTO
    {
        [Range(0, double.MaxValue)]
        public decimal? Price { get; set; }

        [Range(0, int.MaxValue)]
        public int? StockQuantity { get; set; }
    }
}
