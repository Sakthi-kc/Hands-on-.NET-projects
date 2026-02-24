using Product_Management.Entity_Models;

namespace Product_Management.Repository
{
    public class ProductData
    {
        public static List<ProductEntityModel> Products = new List<ProductEntityModel>
        {
            new ProductEntityModel
            {
                ProductId = 1,
                ProductName = "Wireless mouse",
                Price = 599.5M,
                StockQuantity = 10,
                Category = "Electronics",
                IsActive = true
            },
            new ProductEntityModel
            {
                ProductId = 2,
                ProductName = "USB Charger",
                Price = 899.0M,
                StockQuantity = 7,
                Category = "Electronics",
                IsActive = true
            },
            new ProductEntityModel
            {
                ProductId = 3,
                ProductName = "SmartWatch",
                Price = 999,
                StockQuantity = 2,
                Category = "Wearable",
                IsActive = true
            },
            new ProductEntityModel
            {
                ProductId = 4,
                ProductName = "Work table",
                Price = 1499.25M,
                StockQuantity = 30,
                Category = "Home & Office",
                IsActive = true
            },
            new ProductEntityModel
            {
                ProductId = 5,
                ProductName = "Desk lamp",
                Price = 99,
                StockQuantity = 0,
                Category = "Home & Office",
                IsActive = false
            }
        };
    }
}
