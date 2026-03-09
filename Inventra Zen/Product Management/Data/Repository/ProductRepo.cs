using Microsoft.EntityFrameworkCore;
using Product_Management.Entity_Models;

namespace Product_Management.Data.Repository
{
    public class ProductRepo : ZenRepo<ProductEntityModel>, IProductRepo
    {
        public ProductRepo(ProductDBContext context) : base(context)
        {
            
        }

        public async Task<List<ProductEntityModel>?> GetActiveProducts()
        {
            return await _dbSet
                .Where(x => x.IsActive)
                .ToListAsync();
        }

        public async Task<List<ProductEntityModel>?> GetProductsByCategory(string categoryName)
        {
            return await _dbSet
                .Where(x => string.Compare(categoryName, x.Category, StringComparison.OrdinalIgnoreCase) == 0)
                .ToListAsync();
        }

    }
}
