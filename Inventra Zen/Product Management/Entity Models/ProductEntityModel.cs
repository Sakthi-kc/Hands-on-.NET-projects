using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Management.Entity_Models
{
    public class ProductEntityModel
    {
        public int ProductId { get; set; }
        
        public string ProductName { get; set; } = default!;
        
        public decimal Price { get; set; }
        
        public int StockQuantity { get; set; }
        
        public required string Category { get; set; }
        
        public bool IsActive { get; set; }
    }
}
