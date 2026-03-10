using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product_Management.Entity_Models;

namespace Product_Management.Data.Config
{
    public class ProductEntityConfig : IEntityTypeConfiguration<ProductEntityModel>
    {
        public void Configure(EntityTypeBuilder<ProductEntityModel> builder)
        {
            builder.HasKey(rec => rec.ProductId);

            builder.Property(rec => rec.ProductId).UseIdentityColumn();
            builder.Property(rec => rec.ProductName).IsRequired().HasMaxLength(100);
            builder.Property(rec => rec.StockQuantity).IsRequired();
            builder.Property(rec => rec.Price).IsRequired();
            builder.Property(rec => rec.Category).IsRequired().HasMaxLength(50);

            //stored will physically create this column in table
            builder.Property(rec => rec.IsActive).HasComputedColumnSql
                ("CASE When StockQuantity > 0 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", stored: true);

            builder.HasData(
                new List<ProductEntityModel>
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
                }
            );


            //rename table
            builder.ToTable("Products");
        }
    }
}
