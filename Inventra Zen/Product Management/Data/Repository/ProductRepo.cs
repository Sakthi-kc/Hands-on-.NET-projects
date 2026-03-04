using Microsoft.EntityFrameworkCore;
using Product_Management.Entity_Models;

namespace Product_Management.Data.Repository
{
    public class ProductRepo : ZenRepo<ProductEntityModel>, IProductRepo
    {
        public ProductRepo(ProductDBContext context) : base(context)
        {
            
        }

        public async Task<List<ProductEntityModel>> GetActiveProducts()
        {
            return await _dbSet
                .Where(product => product.IsActive)
                .ToListAsync();
        }
    }
}
