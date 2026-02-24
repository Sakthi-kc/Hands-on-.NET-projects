using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product_Management.Entity_Models;

namespace Product_Management.Data.Config
{
    public class ProductEntityConfig : IEntityTypeConfiguration<ProductEntityModel>
    {
        public void Configure(EntityTypeBuilder<ProductEntityModel> builder)
        {
            builder.HasKey("ProductId");

            builder.Property(rec => rec.ProductId).UseIdentityColumn();
            builder.Property(rec => rec.ProductName).IsRequired().HasMaxLength(100);
            builder.Property(rec => rec.StockQuantity).IsRequired();
            builder.Property(rec => rec.Price).IsRequired();
            builder.Property(rec => rec.Category).IsRequired();
            builder.Property(rec => rec.StockQuantity).IsRequired();

            builder.Property(rec => rec.IsActive).HasComputedColumnSql
                ("CASE When StockQuantity > 0 THEN 1 ELSE 0 END");
        }
    }
}
