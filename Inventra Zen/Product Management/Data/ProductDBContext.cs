using Microsoft.EntityFrameworkCore;
using Product_Management.Data.Config;
using Product_Management.Entity_Models;

namespace Product_Management.Data
{
    public class ProductDBContext : DbContext
    {
        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options)
        {
            
        }

        public DbSet<ProductEntityModel>? Products;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityConfig());
        }
    }
}
