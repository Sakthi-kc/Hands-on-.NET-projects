namespace Product_Management.DTOs
{
    public class ProductDTO
    {
        public string ProductName { get; set; } = default!;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public string Category { get; set; } = default!;
    }
}
